using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities.Dto;

namespace TfsGamified.Services.Interfaces
{
    [IoCInfraRegister]
    public interface IProjetoUsuarioAtividadeService
    {
        /// <summary>
        /// Consulta atividades concluidas por Projeto
        /// </summary>
        /// <param name="nomeProjeto"></param>
        /// <returns></returns>
        ProjetoUsuariosAtividadesDTO ConsultarAtividadesConcluidasProProjeto(string nomeProjeto);

        /// <summary>
        /// Consulta atividades concluidas por Projeto em determinada iteração (Sprint, release, etc)
        /// </summary>
        /// <param name="nomeProjeto"></param>
        /// <param name="nomeCiclo"></param>
        /// <returns></returns>
        ProjetoUsuariosAtividadesDTO ConsultarAtividadesConcluidasProProjetoPorCiclo(string nomeProjeto, string nomeCiclo);
    }
}
