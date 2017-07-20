 using System;
 using System.Collections.Generic;
 using System.Linq;
 using TfsGamified.Entities;
 using TfsGamified.Repositories.Interfaces;
 using TfsGamified.Services.Interfaces;

namespace TfsGamified.Services.Classes
{
    public class WorkItemService : IWorkItemService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IClassNodePathRepository _nodePathRepository;
        private readonly IWorkItemCoreLatestRepository _coreLatestRepository;

        public WorkItemService(IProjectRepository projectRepository, 
            IClassNodePathRepository nodePathRepository, 
            IWorkItemCoreLatestRepository coreLatestRepository)
        {
            _projectRepository = projectRepository;
            _nodePathRepository = nodePathRepository;
            _coreLatestRepository = coreLatestRepository;
        }

        
        public List<WorkItemCoreLatest> ConsultarTasksBugsComSituacaoDonePorProjeto(string nomeProjeto)
        {
            return ConsultarTasksBugsComSituacaoDonePorProjetoPorCiclo(nomeProjeto, string.Empty);
        }

        public List<WorkItemCoreLatest> ConsultarTasksBugsComSituacaoDonePorProjetoPorCiclo(string nomeProjeto, string nomeCiclo)
        {
            var projeto = _projectRepository.ObterPorNome(nomeProjeto);

            if (projeto == null)
            {
                throw new Exception(string.Format("{0} {1}", "Projeto não encontrado ", nomeProjeto));
            }

            var nodesPathDoProjeto = _nodePathRepository.ConsultarPorNomeOuUri(nomeProjeto, projeto.Uri);

            if (!nodesPathDoProjeto.Any()) throw new NullReferenceException("NodesPath não encontrado");

            var idsAreasDeNodePath = nodesPathDoProjeto.Select(x => x.Id).ToList();

            List<WorkItemCoreLatest> listaWorkItemCoreLatest = null;
            if (string.IsNullOrEmpty(nomeCiclo))
            {
                listaWorkItemCoreLatest = _coreLatestRepository.ConsultarTasksBugsComSituacaoDonePorArea(idsAreasDeNodePath);
            }
            else
            {
                var idsIteracoesDeNodePath = nodesPathDoProjeto
                    .Where(x => x.IterationPath.ToLower().Contains(nomeCiclo.ToLower())).Select(x => x.Id).ToList();

                listaWorkItemCoreLatest = _coreLatestRepository.ConsultarTasksBugsComSituacaoDonePorIteracoes(idsAreasDeNodePath, idsIteracoesDeNodePath);
            }

            return listaWorkItemCoreLatest;
        }
    }
}
