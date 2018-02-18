using System.Linq;
using System.Web.Mvc;
using CustomInfra.Injector.Simple.IoC;
using TfsGamified.Entities.Dto;
using TfsGamified.Services.Interfaces;
using TfsGamified.Web.Models;

namespace TfsGamified.Web.Controllers
{
    [Route("Jogo")]
    public class JogoController : Controller
    {
        readonly IProjetoUsuarioAtividadeService _atividadeService;
        readonly IProgressoService _jogadorService;
        readonly IEstatisticaService _estatisticaService;

        public JogoController()
        {
            _atividadeService = IoCInfra.Container.GetInstance<IProjetoUsuarioAtividadeService>();
            _jogadorService =  IoCInfra.Container.GetInstance<IProgressoService>();
            _estatisticaService = IoCInfra.Container.GetInstance<IEstatisticaService>();
        }

        [Route("Progresso/{nomeProjeto}")]
        public ActionResult Progresso(string nomeProjeto)
        {
            if (string.IsNullOrEmpty(nomeProjeto))
                return Projeto();

            PreencherTempData(nomeProjeto);


            ProjetoUsuariosAtividadesDTO listaAtividadesUsuario = null;

            listaAtividadesUsuario = _atividadeService.ConsultarAtividadesConcluidasProProjeto(nomeProjeto);


            string usuarioLogado = System.Web.HttpContext.Current.User.Identity.Name;

            var perfilDto = _jogadorService.ConsultarProgressoJogador(listaAtividadesUsuario, usuarioLogado);

            return View("Progresso", ParseToProgressoModel(perfilDto));
        }

        [Route("Estatisticas/{nomeProjeto}")]
        public ActionResult Estatisticas(string nomeProjeto)
        {
            if (string.IsNullOrEmpty(nomeProjeto))
                return Projeto();

            PreencherTempData(nomeProjeto);

            ProjetoUsuariosAtividadesDTO listaAtividadesUsuario = null;

            listaAtividadesUsuario = _atividadeService.ConsultarAtividadesConcluidasProProjeto(nomeProjeto);

            string usuarioLogado = System.Web.HttpContext.Current.User.Identity.Name;
            var perfilDto = _estatisticaService.ConsultarEstatisticasJogador(listaAtividadesUsuario, usuarioLogado);

            return View("Estatisticas", ParseToEstatisticaModel(perfilDto));
        }

        public ActionResult Emblemas()
        {
            return View("Emblemas");
        }

        public ActionResult Premios()
        {
            return View("Premios");
        }

        public ActionResult Sobre()
        {
            return View("Sobre");
        }

        public ActionResult Projeto()
        {
            return View("Projeto");
        }

       
        public void PreencherTempData(string nomeProjeto)
        {
            Session["NomeProjeto"] = nomeProjeto.ToUpper();
        }

        private ProgressoModel ParseToProgressoModel(ProgressoDto dto)
        {
            ProgressoModel model = new ProgressoModel();
            model.Nome = dto.Nome;
            model.Login = dto.Login;
            model.Pontuacao = dto.Pontuacao;
            model.Posicao = dto.Posicao;

            model.ImagemByte = dto.Imagem;

            model.Sugestoes = dto.Sugestoes;

            model.PremioTarefa = dto.Premios.Any(x => x == PremioEnum.Tarefa);
            model.PremioProblema = dto.Premios.Any(x => x == PremioEnum.Problema);

            model.EmblemaAjudante = dto.Emblemas.Any(x => x == EmblemaEnum.Ajudante);
            model.EmblemaCerteiro = dto.Emblemas.Any(x => x == EmblemaEnum.Certeiro);
            model.EmblemaHalter = dto.Emblemas.Any(x => x == EmblemaEnum.Halterofilista);
            model.EmblemaSuporte = dto.Emblemas.Any(x => x == EmblemaEnum.Suporte);
            model.EmblemaResolvedor = dto.Emblemas.Any(x => x == EmblemaEnum.Resolvedor);

            return model;
        }

        private EstatisticaModel ParseToEstatisticaModel(EstatisticaDto dto)
        {
            EstatisticaModel model = new EstatisticaModel();
            model.Nome = dto.Nome;
            model.Login = dto.Login;

            model.ImagemByte = dto.Imagem;

            model.MediaAtividadesSemana = dto.MediaAtividadesSemana;

            model.QuantidadeAtividadesTotal = dto.QuantidadeAtividadesTotal;
            model.QuantidadeAtividadesJogador = dto.QuantidadeAtividadesJogador;
            model.QuantidadeAtividadesReabertas = dto.QuantidadeAtividadesReabertas;
            model.QuantidadeAtividadesPerdidas = dto.QuantidadeAtividadesPerdidas;

            model.PorcentagemAtividadesJogador = dto.PorcentagemAtividadesJogador;
            model.PorcentagemAtividadesReabertas = dto.PorcentagemAtividadesReabertas;
            model.PorcentagemAtividadesPerdidas = dto.PorcentagemAtividadesPerdidas;

            return model;
        }
    }
}