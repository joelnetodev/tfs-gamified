using System;
using System.Collections.Generic;
using System.Linq;
using TfsGamified.Configuration;
using TfsGamified.Entities.Dto;
using TfsGamified.Services.Interfaces;

namespace TfsGamified.Services.Classes.Jogo
{
    public class EstatisticaService : QuantitativoBase, IEstatisticaService
    {
        public EstatisticaDto ConsultarEstatisticasJogador(ProjetoUsuariosAtividadesDTO projetoUsuarioAtividade, string loginUsuario)
        {
            if(!projetoUsuarioAtividade.UsuariosAtividades.Any()) throw new NullReferenceException("usuariosAtividades");

            if (string.IsNullOrEmpty(loginUsuario)) throw new NullReferenceException("nomeUsuario");

            var nomeSplited = loginUsuario.Split('\\');
            loginUsuario = nomeSplited.Length > 1 ? nomeSplited[1] : loginUsuario;

            var usuarioAtividade = ObterUsuarioSolicitante(projetoUsuarioAtividade, loginUsuario);

            var listaresumos = CriarResumosQuantitativos(projetoUsuarioAtividade.UsuariosAtividades);

            return ObterProgressoUsuario(listaresumos, usuarioAtividade.UsuarioInfo, projetoUsuarioAtividade.NomeProjeto);
        }

        private EstatisticaDto ObterProgressoUsuario(List<ResumoQuantitativoHelper> resumosUsuarios,
            UsuarioInfoDTO usuarioInfo, string nomeProjeto)
        {
            var resumosUsuariosAtivos = resumosUsuarios.Where(
                                        x => Configuracao.Xml.LoginsAtivos(nomeProjeto).Contains(x.Login)).ToList();

            var resumo = resumosUsuariosAtivos.FirstOrDefault(x => x.IdUsuario == usuarioInfo.Id);

            if (resumo == null) throw new Exception("Estatística não encontrada");

            EstatisticaDto e = new EstatisticaDto();
            e.Nome = usuarioInfo.Nome;
            e.Login = usuarioInfo.Login;
            e.Imagem = usuarioInfo.Imagem;

            e.QuantidadeAtividadesTotal = resumosUsuarios.Sum(x => x.QuantidadeTarefasConcluidas + x.QuantidadeProblemasConcluidos);
            e.QuantidadeAtividadesJogador = resumo.QuantidadeTarefasConcluidas + resumo.QuantidadeProblemasConcluidos;
            e.QuantidadeAtividadesPerdidas = resumo.QuantidadeAtividadesFeitasQueOutroConcluiu;
            e.QuantidadeAtividadesReabertas = resumo.QuantidadeAtividadesConcluidasReabertas;

            e.PorcentagemAtividadesJogador = CalcularPorcentagem(e.QuantidadeAtividadesJogador, e.QuantidadeAtividadesTotal);
            e.PorcentagemAtividadesPerdidas = CalcularPorcentagem(e.QuantidadeAtividadesPerdidas, e.QuantidadeAtividadesJogador);
            e.PorcentagemAtividadesReabertas = CalcularPorcentagem(e.QuantidadeAtividadesReabertas, e.QuantidadeAtividadesJogador);

            return e;
        }

        public int CalcularPorcentagem(decimal valor, decimal maximo)
        {
            if (maximo == 0 && valor != 0) return 100;

            if ((valor == 0 && maximo != 0)
                || (valor == 0 && maximo == 0)) return 0;

            return Convert.ToInt32((valor * 100) / maximo);
        }
    }
}