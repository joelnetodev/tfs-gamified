using System;
using System.Collections.Generic;
using System.Linq;
using TfsGamified.Configuration;
using TfsGamified.Entities.Dto;
using TfsGamified.Services.Interfaces;

namespace TfsGamified.Services.Classes.Jogo
{
    public class ProgressoService : QuantitativoBase, IProgressoService
    {
        public ProgressoDto ConsultarProgressoJogador(ProjetoUsuariosAtividadesDTO projetoUsuarioAtividade, string loginUsuario)
        {
            if(!projetoUsuarioAtividade.UsuariosAtividades.Any()) throw new NullReferenceException("usuariosAtividades");

            if (string.IsNullOrEmpty(loginUsuario)) throw new NullReferenceException("nomeUsuario");

            var nomeSplited = loginUsuario.Split('\\');
            loginUsuario = nomeSplited.Length > 1 ? nomeSplited[1] : loginUsuario;

            var usuarioAtividade = ObterUsuarioSolicitante(projetoUsuarioAtividade, loginUsuario);

            var listaresumos = CriarResumosQuantitativos(projetoUsuarioAtividade.UsuariosAtividades);

            return ObterProgressoUsuario(listaresumos, usuarioAtividade.UsuarioInfo, projetoUsuarioAtividade.NomeProjeto);
        }

        private ProgressoDto ObterProgressoUsuario(List<ResumoQuantitativoHelper> resumosUsuarios, UsuarioInfoDTO usuarioInfo, string nomeProjeto)
        {
            var resumosUsuariosAtivos = resumosUsuarios.Where(
                                            x => Configuracao.Xml.LoginsAtivos(nomeProjeto).Contains(x.Login)).ToList();

            var resumoSuporte = ObterQuemAtendeCriterioPorEmblema(resumosUsuariosAtivos, EmblemaEnum.Suporte);
            var resumoHalter = ObterQuemAtendeCriterioPorEmblema(resumosUsuariosAtivos, EmblemaEnum.Halterofilista);
            var resumoCerteiro = ObterQuemAtendeCriterioPorEmblema(resumosUsuariosAtivos, EmblemaEnum.Certeiro);
            var resumoAjudante = ObterQuemAtendeCriterioPorEmblema(resumosUsuariosAtivos, EmblemaEnum.Ajudante);
            var resumoSabio = ObterQuemAtendeCriterioPorEmblema(resumosUsuariosAtivos, EmblemaEnum.Resolvedor);

            var dicUsuarioResumoPontuacao = ObterPontuacoes(resumosUsuariosAtivos, resumoSuporte, resumoHalter,
                resumoCerteiro, resumoAjudante, resumoSabio);

            int posicao = 0;
            foreach (var itemDicUsuarioResumo in dicUsuarioResumoPontuacao.OrderByDescending(x => x.Value))
            {
                posicao +=1;

                var resumo = itemDicUsuarioResumo.Key;

                if (resumo.IdUsuario != usuarioInfo.Id) continue;

                int pontuacao = itemDicUsuarioResumo.Value;

                ProgressoDto p = new ProgressoDto();
                p.Nome = usuarioInfo.Nome;
                p.Login = usuarioInfo.Login;
                p.Pontuacao = pontuacao;
                p.Posicao = posicao;
                p.Imagem = usuarioInfo.Imagem;

                if (resumo.QuantidadeTarefasConcluidas >= Configuracao.Xml.QuantidadePremioTarefa)
                { p.Premios.Add(PremioEnum.Tarefa); }
                if (resumo.QuantidadeProblemasConcluidos >= Configuracao.Xml.QuantidadePremioProblema)
                { p.Premios.Add(PremioEnum.Problema); }

                if (resumo == resumoSuporte)
                { p.Emblemas.Add(EmblemaEnum.Suporte); }
                if (resumo == resumoHalter)
                { p.Emblemas.Add(EmblemaEnum.Halterofilista); }
                if (resumo == resumoCerteiro)
                { p.Emblemas.Add(EmblemaEnum.Certeiro); }
                if (resumo == resumoAjudante)
                { p.Emblemas.Add(EmblemaEnum.Ajudante); }
                if (resumo == resumoSabio)
                { p.Emblemas.Add(EmblemaEnum.Resolvedor); }

                bool houveReabertas = resumo.QuantidadeAtividadesConcluidasReabertas != 0;
                bool houvePerdidas = resumo.QuantidadeAtividadesFeitasQueOutroConcluiu != 0;
                p.Sugestoes = CriarSugestoes(p, houveReabertas, houvePerdidas, resumosUsuariosAtivos.Count);

                return p;
            }
            
            throw new Exception("Progresso não encontrado");
        }

        public Dictionary<ResumoQuantitativoHelper, int> ObterPontuacoes(List<ResumoQuantitativoHelper> resumosUsuarios,
            ResumoQuantitativoHelper resumoSuporte, ResumoQuantitativoHelper resumoHalter,
            ResumoQuantitativoHelper resumoCerteiro, ResumoQuantitativoHelper resumoAjudante, ResumoQuantitativoHelper resumoSabio)
        {
            var dicUsuarioResumoTotal = new Dictionary<ResumoQuantitativoHelper, int>();

            foreach (var itemResumo in resumosUsuarios)
            {
                int pontuacaoAtividades = ObterPontuacaoDeAtividades(itemResumo);

                int pontuacaoPremios = 0;
                pontuacaoPremios += ObterPontuacaoDePremios(itemResumo, PremioEnum.Tarefa);
                pontuacaoPremios += ObterPontuacaoDePremios(itemResumo, PremioEnum.Problema);

                int pontuacaoEmblemas = 0;

                if (itemResumo == resumoSuporte)
                    pontuacaoEmblemas += ObterPontuacaoDeEmblemas(EmblemaEnum.Suporte);
                if (itemResumo == resumoHalter)
                    pontuacaoEmblemas += ObterPontuacaoDeEmblemas(EmblemaEnum.Halterofilista);
                if (itemResumo == resumoCerteiro)
                    pontuacaoEmblemas += ObterPontuacaoDeEmblemas(EmblemaEnum.Certeiro);
                if (itemResumo == resumoAjudante)
                    pontuacaoEmblemas += ObterPontuacaoDeEmblemas(EmblemaEnum.Ajudante);
                if (itemResumo == resumoSabio)
                    pontuacaoEmblemas += ObterPontuacaoDeEmblemas(EmblemaEnum.Resolvedor);

                int pontuacaoTotal = pontuacaoAtividades + pontuacaoPremios + pontuacaoEmblemas;

                dicUsuarioResumoTotal.Add(itemResumo, pontuacaoTotal);
            }

            return dicUsuarioResumoTotal;
        }

        public int ObterPontuacaoDeAtividades(ResumoQuantitativoHelper resumo)
        {
            int pontuacao = 0;

            pontuacao = (resumo.QuantidadeTarefasConcluidas + resumo.QuantidadeProblemasConcluidos) * Configuracao.Xml.PontuacaoAtividade;

            pontuacao = pontuacao - resumo.QuantidadeAtividadesConcluidasReabertas * Configuracao.Xml.PontuacaoAtividadeReaberta;

            pontuacao = pontuacao + (resumo.QuantidadeAtividadesFazendoQueOutroConcluiu * Configuracao.Xml.PontuacaoAtividadePerdida);

            return pontuacao;
        }

        public int ObterPontuacaoDePremios(ResumoQuantitativoHelper resumo, PremioEnum premio)
        {

            switch (premio)
            {
                case PremioEnum.Tarefa:
                    {
                        if (resumo.QuantidadeTarefasConcluidas >= Configuracao.Xml.QuantidadePremioTarefa)
                        {
                            return Configuracao.Xml.PontuacaoPremioTarefa;
                        }
                    }
                    break;
                case PremioEnum.Problema:
                    {
                        if (resumo.QuantidadeProblemasConcluidos >= Configuracao.Xml.QuantidadePremioProblema)
                        {
                            return Configuracao.Xml.PontuacaoPremioProblema;
                        }
                    }
                    break;
            }

            return 0;
        }

        public ResumoQuantitativoHelper ObterQuemAtendeCriterioPorEmblema(List<ResumoQuantitativoHelper> resumosUsuarios, EmblemaEnum emblema)
        {
            switch (emblema)
            {
                case EmblemaEnum.Suporte:
                    {
                        return resumosUsuarios.OrderByDescending(x => x.QuantidadeProblemasConcluidos).First();
                    }
                    break;
                case EmblemaEnum.Halterofilista:
                    {
                        return resumosUsuarios.OrderByDescending(x => x.QuantidadeTarefasConcluidas).First();
                    }
                    break;
                case EmblemaEnum.Ajudante:
                    {
                        return resumosUsuarios.OrderByDescending(x => x.QuantidadeAtividadesFazendoQueOutroConcluiu).First();
                    }
                    break;
                case EmblemaEnum.Certeiro:
                    {
                        return resumosUsuarios.Where(x => x.ValorTotalEstimado != 0 && x.ValorTotalRealizado != 0)
                                .OrderBy(x => Math.Abs(x.ValorTotalEstimado - x.ValorTotalRealizado)).First();
                    }
                    break;
                case EmblemaEnum.Resolvedor:
                    {
                        return resumosUsuarios.OrderByDescending(x => x.QuantidadeAtividadesConcluidasQueOutrosFizeram).First();
                    }
                    break;
            }

            return null;
        }

        public int ObterPontuacaoDeEmblemas(EmblemaEnum emblema)
        {
            switch (emblema)
            {
                case EmblemaEnum.Suporte:
                    {
                        return Configuracao.Xml.PontuacaoSuporte;
                    }
                    break;
                case EmblemaEnum.Halterofilista:
                    {
                        return Configuracao.Xml.PontuacaoHalter;
                    }
                    break;
                case EmblemaEnum.Ajudante:
                    {
                        return Configuracao.Xml.PontuacaoAjudante;
                    }
                    break;
                case EmblemaEnum.Certeiro:
                    {
                        return Configuracao.Xml.PontuacaoCerteiro;
                    }
                    break;
                case EmblemaEnum.Resolvedor:
                    {
                        return Configuracao.Xml.PontuacaoSabio;
                    }
                    break;
            }

            return 0;
        }

        public List<string> CriarSugestoes(ProgressoDto progressoDto, bool houveReabertas, bool houvePerdidas, int qtdParticipantes)
        {
            List<string> lista = new List<string>();

            if (progressoDto.Posicao == qtdParticipantes)
            {
                lista.Add("Você está na ultima colocação. Precisamos melhorar esse quadro.");
            }

            switch (progressoDto.Posicao)
            {
                case 2:
                    lista.Add("A primeira posição está bem a sua frente. Continue com o bom trabalho e suba mais degraus.");
                    break;
                case 3:
                    lista.Add("O pódio é seu. Continue com o bom trabalho e suba mais degraus.");
                    break;
                case 4:
                    lista.Add("Você está perto do pódio. Continue com o bom trabalho e suba mais degraus.");
                    break;
                default:
                    break;
            }


            if (!progressoDto.Emblemas.Any())
                lista.Add("Além de ganhar pontos extras, o emblema o diferencia dos demais jogadores em algum aspecto. Tente atender os critérios.");

            if (!progressoDto.Premios.Any())
                lista.Add("Atenda as expectativas. Conquiste os prêmios definidos e ganhe pontos extras.");

            if (houveReabertas)
                lista.Add("Você teve várias atividades reabertas e isso faz com que você não alcance a pontuação máxima. Faça mais testes para ter certeza que as concluiu antes de fechá-las.");

            if (houvePerdidas)
                lista.Add("Após concluir as atividades, as mesmas foram reabertas e outro jogador as fechou. Preste mais atenção para não perder os pontos.");

            return lista;
        }
    }
}