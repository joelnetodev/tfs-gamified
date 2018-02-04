using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomInfra.Injector.Simple.IoC;
using TfsGamified.Configuration;
using TfsGamified.Entities;
using TfsGamified.Entities.Dto;
using TfsGamified.Repositories.Interfaces;
using TfsGamified.Services.Interfaces;

namespace TfsGamified.Services.Classes
{
    public class ProjetoUsuarioAtividadeService : IProjetoUsuarioAtividadeService
    {
        private readonly IWorkItemService _workItemService;
        private readonly IWorkItemCoreWereRepository _coreWereRepository;
        private readonly IWorkItemCustomLatestRepository _customLatestRepository;
        private readonly IConstantService _constantService;
        private readonly IFieldService _fieldService;

      public ProjetoUsuarioAtividadeService(IWorkItemCoreWereRepository coreWereRepository,
            IWorkItemCustomLatestRepository customLatestRepository,
            IWorkItemService workItemService, IConstantService constantService, IFieldService fieldService)
        {
            _coreWereRepository = coreWereRepository;
            _customLatestRepository = customLatestRepository;
            _workItemService = workItemService;
          _constantService = constantService;
          _fieldService = fieldService;
        }

        public ProjetoUsuariosAtividadesDTO ConsultarAtividadesConcluidasProProjeto(string nomeProjeto)
        {
            return ConsultarAtividadesConcluidasProProjetoPorCiclo(nomeProjeto, null);
        }

        public ProjetoUsuariosAtividadesDTO ConsultarAtividadesConcluidasProProjetoPorCiclo(string nomeProjeto, string nomeCiclo)
        {
            List<WorkItemCoreLatest> workItemsCoreLatest;
            List<WorkItemCoreWere> workItemsCoreWere;
            List<WorkItemCustomLatest> workItemsCustomLatest;
            List<Constant> constants;
            List<Field> fields;

            workItemsCoreLatest = _workItemService.ConsultarTasksBugsComSituacaoDonePorProjetoPorCiclo(nomeProjeto, nomeCiclo);
            workItemsCoreWere = _coreWereRepository.ConsultarPorItemsCoreLatest(workItemsCoreLatest);
            workItemsCustomLatest = _customLatestRepository.ConsultarPorItemsCoreLatest(workItemsCoreLatest);

            constants = _constantService.ConsultarPorConfiguracaoComImagem(nomeProjeto);
            fields = _fieldService.ConsultarFieldPorConfiguracaoComTitulo();

            return ConverterEntidadesEmAtividadesUsuarioDto(nomeProjeto, workItemsCoreLatest, workItemsCoreWere, workItemsCustomLatest, constants, fields);
        }

        private ProjetoUsuariosAtividadesDTO ConverterEntidadesEmAtividadesUsuarioDto(string nomeProjeto,
            List<WorkItemCoreLatest> workItemsCoreLatest,
            List<WorkItemCoreWere> workItemsCoreWere,
            List<WorkItemCustomLatest> workItemsCustomLatest,
            List<Constant> constants,
            List<Field> fields)
        {
            ProjetoUsuariosAtividadesDTO projetoAtividade = new ProjetoUsuariosAtividadesDTO(nomeProjeto);

            List<UsuarioAtividadeDTO> usuariosAtividade = new List<UsuarioAtividadeDTO>();

            List<UsuarioInfoDTO> usuariosInfo = CriarUsuariosInfoDto(constants);
            List<CampoDTO> camposDto = CriarListaCampoDtos(fields);

            List<Task> tasks = new List<Task>();

            var workItemLatestTypeGrouped = workItemsCoreLatest.AsParallel().ToLookup(x => x.WorkItemType);
            var workItemWereTypeGrouped = workItemsCoreWere.AsParallel().ToLookup(x => x.WorkItemType);

            foreach (var itemUser in usuariosInfo)
            {
                UsuarioAtividadeDTO usuarioAtividade = new UsuarioAtividadeDTO {UsuarioInfo = itemUser };
                usuariosAtividade.Add(usuarioAtividade);

                //Apenas tarefas implementadas pelo usuário
                var tasksCoreLatestDoUser = new List<WorkItemCoreLatest>();
                var tasksCoreWereDoUser = new List<WorkItemCoreWere>();
                var tasksCustomLatestDoUser = new List<WorkItemCustomLatest>();
                if (workItemLatestTypeGrouped.Contains(WorkItemType.Task))
                {
                    tasksCoreLatestDoUser = workItemLatestTypeGrouped[WorkItemType.Task].AsParallel().Where(x => x.IdUsuarioPadrao == itemUser.Id).ToList();

                    tasksCoreWereDoUser = workItemWereTypeGrouped[WorkItemType.Task].AsParallel().Where(x => tasksCoreLatestDoUser.Any(
                        l => l.PartitionId == x.PartitionId && l.DataspaceId == x.DataspaceId && x.Id == l.Id)).ToList();

                    tasksCustomLatestDoUser = workItemsCustomLatest.AsParallel().Where(x => tasksCoreLatestDoUser.Any(
                        l => l.PartitionId == x.PartitionId && l.DataspaceId == x.DataspaceId && x.Id == l.Id)).ToList();
                }

                //Apenas problemas resolvidos pelo usuário
                //Obs: No WorkItemCoreLatest o problema esta aprovado, mas assinado para um usuário da equipe de teste o qual não participa da equipe de dev. 
                //Solução: Pegar apenas workitems com situação testing do WorkItemCoreWere, pois a situação de testing está assinado para o usuário de dev no histórico.
                var bugsCoreLatestDoUser = new List<WorkItemCoreLatest>();
                var bugsCoreWereDoUser = new List<WorkItemCoreWere>();
                var bugsCustomLatestDoUser = new List<WorkItemCustomLatest>();
                if (workItemLatestTypeGrouped.Contains(WorkItemType.Bug))
                {
                   var dinamicBugsCoreLatestDoUser = workItemWereTypeGrouped[WorkItemType.Bug].AsParallel().Where(x => x.IdUsuarioPadrao == itemUser.Id
                                                && x.State == WorkItemState.Testing).Select(x => new { x.PartitionId, x.DataspaceId, x.Id }).Distinct().ToList();

                    bugsCoreLatestDoUser = workItemLatestTypeGrouped[WorkItemType.Bug].AsParallel().Where(x => dinamicBugsCoreLatestDoUser.Any(
                        l => l.PartitionId == x.PartitionId && l.DataspaceId == x.DataspaceId && x.Id == l.Id)).ToList();

                    bugsCoreWereDoUser = workItemWereTypeGrouped[WorkItemType.Bug].AsParallel().Where(x => dinamicBugsCoreLatestDoUser.Any(
                        l => l.PartitionId == x.PartitionId && l.DataspaceId == x.DataspaceId && x.Id == l.Id)).ToList();

                    bugsCustomLatestDoUser = workItemsCustomLatest.AsParallel().Where(x => dinamicBugsCoreLatestDoUser.Any(
                        l => l.PartitionId == x.PartitionId && l.DataspaceId == x.DataspaceId && l.Id == x.Id)).ToList();
                }
                
                Task task = new Task(() =>
                {
                    usuarioAtividade.Atividades.AddRange(CriarWorkItemsAtividadeDto(tasksCoreLatestDoUser, tasksCoreWereDoUser, usuariosInfo, tasksCustomLatestDoUser, camposDto));
                    usuarioAtividade.Atividades.AddRange(CriarWorkItemsBugDto(bugsCoreLatestDoUser, bugsCoreWereDoUser, usuariosInfo, itemUser, bugsCustomLatestDoUser, camposDto));
                });

                tasks.Add(task);
                task.Start();
            }

            Task.WaitAll(tasks.ToArray());

            projetoAtividade.UsuariosAtividades.AddRange(usuariosAtividade);

            return projetoAtividade;
        }
        
        private List<AtividadeDTO> CriarWorkItemsAtividadeDto(List<WorkItemCoreLatest> workItemsCoreLatest, List<WorkItemCoreWere> workItemsCoreWere,
            List<UsuarioInfoDTO> usersInfo, List<WorkItemCustomLatest> workItemsCustomLatest, List<CampoDTO> fields)
        {
            List<AtividadeDTO> dtos = new List<AtividadeDTO>();

            foreach (var currentEntity in workItemsCoreLatest)
            {
                var dto = new AtividadeDTO();

                dto.PartitionId = currentEntity.PartitionId;
                dto.DataspaceId = currentEntity.DataspaceId;
                dto.Id = currentEntity.Id;
                dto.Ordem = 0;
                dto.Tipo = TipoAtividadeEnum.Tarefa;
                dto.Situacao = SituacaoAtividadeEnum.Feito;
                dto.Data = currentEntity.ChangedDate;

                var workItemsCoreWereDoCurrent = workItemsCoreWere.Where(x => x.PartitionId == dto.PartitionId
                                                    && x.DataspaceId == dto.DataspaceId && x.Id == dto.Id).OrderBy(x => x.ChangedDate).ToList();

                dto.Historico.AddRange(CriarWorkItemHistoricoDto(workItemsCoreWereDoCurrent, usersInfo, TipoAtividadeEnum.Tarefa));

                var workItemCustomDoCurrent = workItemsCustomLatest.Where(x => x.PartitionId == currentEntity.PartitionId
                                                   && x.DataspaceId == currentEntity.DataspaceId && x.Id == currentEntity.Id).ToList();

                dto.CamposValores.AddRange(CriarWorkItemsComValorDosCampos(workItemCustomDoCurrent, fields));

                dtos.Add(dto);
            }

            return dtos;
        }

        private List<AtividadeDTO> CriarWorkItemsBugDto(List<WorkItemCoreLatest> workItemsCoreLatest, List<WorkItemCoreWere> workItemsCoreWere,
            List<UsuarioInfoDTO> usersInfo, UsuarioInfoDTO currentUser, List<WorkItemCustomLatest> workItemsCustomLatest, List<CampoDTO> fields)
        {
            List<AtividadeDTO> dtos = new List<AtividadeDTO>();

            foreach (var currentEntity in workItemsCoreLatest)
            {
                var workItemsCoreWereDoCurrent = workItemsCoreWere.Where(x => x.PartitionId == currentEntity.PartitionId
                                                    && x.DataspaceId == currentEntity.DataspaceId && x.Id == currentEntity.Id).OrderBy(x => x.ChangedDate).ToList();

                //Para verificar se o problema foi resolvido pelo usuário atual, 
                //é verificado se a utlima situação de testing está assinada para o mesmo
                var ultimoWorkItemCoreWereTesting = workItemsCoreWereDoCurrent.LastOrDefault(x =>
                                                    x.State == WorkItemState.Testing
                                                    && usersInfo.Any(u => u.Id == x.IdUsuarioPadrao));

                //Se estiver assinado pra o usuario corrente, significa que foi ele quem corrigiu o problema
                //Se não, significa que o usuario tem hsitorico de testing, mas o problema foi reaberto e não foi corrigido por ele
                if (ultimoWorkItemCoreWereTesting == null ||
                    ultimoWorkItemCoreWereTesting.IdUsuarioPadrao != currentUser.Id) continue;

                var dto = new AtividadeDTO();

                dto.PartitionId = currentEntity.PartitionId;
                dto.DataspaceId = currentEntity.DataspaceId;
                dto.Id = currentEntity.Id;
                dto.Ordem = 0;
                dto.Tipo = TipoAtividadeEnum.Bug;
                dto.Situacao = SituacaoAtividadeEnum.Feito;
                dto.Data = currentEntity.ChangedDate;

                dto.Historico.AddRange(CriarWorkItemHistoricoDto(workItemsCoreWereDoCurrent, usersInfo, TipoAtividadeEnum.Bug));

                var workItemCustomDoCurrent = workItemsCustomLatest.Where(x => x.PartitionId == currentEntity.PartitionId
                                                   && x.DataspaceId == currentEntity.DataspaceId && x.Id == currentEntity.Id).ToList();

                dto.CamposValores.AddRange(CriarWorkItemsComValorDosCampos(workItemCustomDoCurrent, fields));

                dtos.Add(dto);
            }

            return dtos;
        }

        private List<AtividadeDTO> CriarWorkItemHistoricoDto(List<WorkItemCoreWere> workItemsCoreWere, List<UsuarioInfoDTO> usersInfo, TipoAtividadeEnum tipoAtividade)
        {
            List<AtividadeDTO> dtos = new List<AtividadeDTO>();

            for (int i = 0; i < workItemsCoreWere.Count; i++)
            {
                var currentEntity = workItemsCoreWere[i];

                var situacao = EscolherSituacao(currentEntity.State);
                if (!situacao.HasValue) continue;

                var dto = new AtividadeDTO();
                dto.PartitionId = currentEntity.PartitionId;
                dto.DataspaceId = currentEntity.DataspaceId;
                dto.Id = currentEntity.Id;
                dto.Ordem = i + 1;
                dto.Data = currentEntity.ChangedDate;

                dto.Situacao = situacao.Value;
                dto.Tipo = tipoAtividade;

                dto.UsuarioInfo = usersInfo.FirstOrDefault(x => x.Id == currentEntity.IdUsuarioPadrao);

                dtos.Add(dto);
            }

            return dtos;
        }

        private List<CampoValorDTO> CriarWorkItemsComValorDosCampos(List<WorkItemCustomLatest> workItemsCustomLatest, List<CampoDTO> camposDto)
        {
            List<CampoValorDTO> dtos = new List<CampoValorDTO>();

            foreach (var currentEntity in workItemsCustomLatest.Where(x => camposDto.Any(c => c.Id == x.FieldId)))
            {
                var dto = new CampoValorDTO();
                dto.PartitionId = currentEntity.PartitionId;
                dto.DataspaceId = currentEntity.DataspaceId;
                dto.Id = currentEntity.Id;
                dto.Campo = camposDto.FirstOrDefault(x => x.Id == currentEntity.FieldId);

                object valor;
                dto.TipoValor = EscolherTipoValor(currentEntity, out valor);
                dto.Valor = valor;

                dtos.Add(dto);
            }

            return dtos;
        }

        private List<CampoDTO> CriarListaCampoDtos(List<Field> fields)
        {
            List<CampoDTO> camposDtos = new List<CampoDTO>();

            foreach (var currentEntity in fields)
            {
                camposDtos.Add(new CampoDTO { Id = currentEntity.FieldId, Nome = currentEntity.Name });
            }

            return camposDtos;
        }

        private List<UsuarioInfoDTO> CriarUsuariosInfoDto(List<Constant> constants)
        {
            var dtos = new List<UsuarioInfoDTO>();

            foreach (var itemEntidade in constants)
            {
                var dto = new UsuarioInfoDTO();
                dto.Id = itemEntidade.ConstId;
                dto.Nome = itemEntidade.IdentityDisplayName;
                dto.Login = itemEntidade.NamePart;
                dto.Imagem = itemEntidade.ImageData;

                dtos.Add(dto);
            }

            return dtos;
        }

        #region ajuda

        private SituacaoAtividadeEnum? EscolherSituacao(string situacao)
        {
            switch (situacao)
            {
                case WorkItemState.Done:
                    return SituacaoAtividadeEnum.Feito;
                case WorkItemState.InProgress:
                    return SituacaoAtividadeEnum.Fazendo;
                case WorkItemState.ToDo:
                case WorkItemState.New:
                    return SituacaoAtividadeEnum.Afazer;
                case WorkItemState.Testing:
                    return SituacaoAtividadeEnum.Teste;
                default:
                    return null;
            }
        }
        
        private TipoValorCampoEnumDTO EscolherTipoValor(WorkItemCustomLatest entity, out object valor)
        {
            if (entity.IntValue.HasValue || entity.FloatValue.HasValue)
            {
                valor = entity.FloatValue.HasValue
                    ? entity.FloatValue.Value
                    : entity.IntValue.Value;

                return TipoValorCampoEnumDTO.Numero;
            }

            if (!string.IsNullOrEmpty(entity.TextValue) || !string.IsNullOrEmpty(entity.StringValue))
            {
                valor = !string.IsNullOrEmpty(entity.TextValue)
                    ? entity.TextValue
                    : entity.StringValue;

                return TipoValorCampoEnumDTO.Texto;
            }

            //if (entity.BitValue.HasValue)
            //{
            //    valor = entity.BitValue.Value;
            //    return TipoValorCampoEnumDTO.Booleano;
            //}

            //if (entity.DateTimeValue.HasValue)
            //{
            //    valor = entity.DateTimeValue.Value;
            //    return TipoValorCampoEnumDTO.Data;
            //}

            valor = null;
            return TipoValorCampoEnumDTO.Nulo;
        }
        #endregion
    }
}
