using System;
using System.Collections.Generic;
using System.Linq;
using TfsGamified.Configuration;
using TfsGamified.Entities.Dto;

namespace TfsGamified.Services.Classes.Jogo
{
    public abstract class QuantitativoBase
    {
        public UsuarioAtividadeDTO ObterUsuarioSolicitante(ProjetoUsuariosAtividadesDTO projetoUsuarioAtividade, string loginUsuario)
        {
            if (!Configuracao.Xml.LoginsDesenvolvedores(projetoUsuarioAtividade.NomeProjeto).Contains(loginUsuario))
                throw new Exception("Usuário não configurado como desenvolvedor");

            if (!Configuracao.Xml.LoginsAtivos(projetoUsuarioAtividade.NomeProjeto).Contains(loginUsuario))
                throw new Exception("Desenvolvedor não está ativo no jogo");

            var usuarioAtividade = projetoUsuarioAtividade.UsuariosAtividades.OrderByDescending(x => x.Atividades.Count)
                                    .FirstOrDefault(x => x.UsuarioInfo.Login == loginUsuario);

            if(usuarioAtividade == null)
                throw new Exception("Não foi encontrado usuário nos registros de atividades");

            return usuarioAtividade;
        }

        public List<ResumoQuantitativoHelper> CriarResumosQuantitativos(List<UsuarioAtividadeDTO> usuariosAtividades)
        {
            List<ResumoQuantitativoHelper> detalhamentoUsers = new List<ResumoQuantitativoHelper>();

            foreach (var usuarioAtividade in usuariosAtividades)
            {
                detalhamentoUsers.Add(CriarResumoQuantitativo(usuarioAtividade, usuariosAtividades));
            }
            
            return detalhamentoUsers;
        }

        public ResumoQuantitativoHelper CriarResumoQuantitativo(UsuarioAtividadeDTO usuarioAtividade, List<UsuarioAtividadeDTO> usuariosAtividades)
        {
            ResumoQuantitativoHelper detalhe = new ResumoQuantitativoHelper();

            detalhe.IdUsuario = usuarioAtividade.UsuarioInfo.Id;
            detalhe.Login = usuarioAtividade.UsuarioInfo.Login;

            foreach (var itemAtividade in usuarioAtividade.Atividades)
            {
                //Verifica quantas atividades foram feitas
                bool atividadeTipoTarefa = itemAtividade.Tipo == TipoAtividadeEnum.Tarefa;

                if (atividadeTipoTarefa)
                    detalhe.QuantidadeTarefasConcluidas += 1;
                else
                    detalhe.QuantidadeProblemasConcluidos += 1;

                //Verifica quantas atividades foram feitas e depois reabertas em nome do usuário corrente
                var situacaoFeito = itemAtividade.Historico.FirstOrDefault(x => (x.Situacao == SituacaoAtividadeEnum.Feito || x.Situacao == SituacaoAtividadeEnum.Teste)
                                                                                  && x.UsuarioInfo == usuarioAtividade.UsuarioInfo);

                var situacaoFazendoOuAFazer = itemAtividade.Historico.LastOrDefault(x => x.Situacao == SituacaoAtividadeEnum.Fazendo || x.Situacao == SituacaoAtividadeEnum.Afazer);

                if (situacaoFeito != null && situacaoFazendoOuAFazer != null
                    && situacaoFeito.Ordem < situacaoFazendoOuAFazer.Ordem)
                {
                    detalhe.QuantidadeAtividadesConcluidasReabertas += 1;
                }

                //Verifica estimativas
                var estimado = itemAtividade.CamposValores.FirstOrDefault(x => x.Campo.Nome == Configuracao.Xml.CampoEstimadoTarefa
                                                                                || x.Campo.Nome == Configuracao.Xml.CampoEstimadoProblema);

                var realizado = itemAtividade.CamposValores.FirstOrDefault(x => x.Campo.Nome == Configuracao.Xml.CampoRealizadoTarefa
                                                                                || x.Campo.Nome == Configuracao.Xml.CampoRealizadoProblema);

                if (estimado != null && realizado != null)
                {
                    detalhe.ValorTotalEstimado += estimado.TipoValor == TipoValorCampoEnumDTO.Numero
                        ? Convert.ToDecimal(estimado.Valor)
                        : 0;

                    detalhe.ValorTotalRealizado += realizado.TipoValor == TipoValorCampoEnumDTO.Numero
                        ? Convert.ToDecimal(realizado.Valor)
                        : 0;
                }

                //Verifica se a atividade foi concluida por outro usuário anteriormente
                if(itemAtividade.Historico.Any(x => x.UsuarioInfo != null && x.Situacao == SituacaoAtividadeEnum.Feito && x.UsuarioInfo != usuarioAtividade.UsuarioInfo))
                    detalhe.QuantidadeAtividadesConcluidasQueOutrosFizeram += 1;
            }

            //Verifica informações em relação a atividades de outros ususarios
            foreach (var itemAtividadeOutroUsuario in usuariosAtividades.Where(x => x.UsuarioInfo != usuarioAtividade.UsuarioInfo).SelectMany(x => x.Atividades))
            {
                var hsitoricoComAlteracoesDoUsuarioCorrente = itemAtividadeOutroUsuario.Historico.Where(x => x.UsuarioInfo == usuarioAtividade.UsuarioInfo);

                if (hsitoricoComAlteracoesDoUsuarioCorrente.Any(x => x.Situacao == SituacaoAtividadeEnum.Fazendo))
                {
                    detalhe.QuantidadeAtividadesFazendoQueOutroConcluiu += 1;
                }

                if (hsitoricoComAlteracoesDoUsuarioCorrente.Any(x => x.Situacao == SituacaoAtividadeEnum.Feito || x.Situacao == SituacaoAtividadeEnum.Teste))
                {
                    detalhe.QuantidadeAtividadesFeitasQueOutroConcluiu += 1;
                }
            }

            return detalhe;
        }
    }

    public class ResumoQuantitativoHelper
    {
        public string Login { get; set; }
        public int IdUsuario { get; set; }
        public int QuantidadeTarefasConcluidas { get; set; }

        public int QuantidadeProblemasConcluidos { get; set; }

        public int QuantidadeAtividadesConcluidasReabertas { get; set; }

        public int QuantidadeAtividadesFeitasQueOutroConcluiu { get; set; }

        public int QuantidadeAtividadesFazendoQueOutroConcluiu { get; set; }

        public int QuantidadeAtividadesConcluidasQueOutrosFizeram { get; set; }

        public decimal ValorTotalEstimado { get; set; }

        public decimal ValorTotalRealizado { get; set; }

    }
}