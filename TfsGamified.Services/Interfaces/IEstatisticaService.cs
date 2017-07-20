using System.Collections.Generic;
using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities.Dto;

namespace TfsGamified.Services.Interfaces
{
    [IoCInfraRegister]
    public interface IEstatisticaService
    {
        /// <summary>
        /// Consulta as quantidades e porcentagens de atividades do usuário no jogo
        /// </summary>
        /// <param name="projetoUsuarioAtividade"></param>
        /// <param name="nomeUsuario"></param>
        /// <returns></returns>
        EstatisticaDto ConsultarEstatisticasJogador(ProjetoUsuariosAtividadesDTO projetoUsuarioAtividade, string nomeUsuario);
    }
}
