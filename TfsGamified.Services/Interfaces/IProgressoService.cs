using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities.Dto;

namespace TfsGamified.Services.Interfaces
{
    [IoCInfraRegister]
    public interface IProgressoService
    {
        /// <summary>
        /// Consulta o progresso do usuário referente a pontuação conquistada no jogo
        /// </summary>
        /// <param name="projetoUsuarioAtividade"></param>
        /// <param name="nomeUsuario"></param>
        /// <returns></returns>
        ProgressoDto ConsultarProgressoJogador(ProjetoUsuariosAtividadesDTO projetoUsuarioAtividade, string nomeUsuario);
    }
}
