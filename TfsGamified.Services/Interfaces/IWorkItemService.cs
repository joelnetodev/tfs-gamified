using System.Collections.Generic;
using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Services.Interfaces
{
    [IoCInfraRegister]
    public interface IWorkItemService
    {
        /// <summary>
        /// Consulta WorkItems do tipo Task e Bugs com situação Done por Projeto agrupados por usuário
        /// </summary>
        /// <param name="nomeProjeto"></param>
        /// <returns></returns>
        List<WorkItemCoreLatest> ConsultarTasksBugsComSituacaoDonePorProjeto(string nomeProjeto);

        /// <summary>
        /// Consulta WorkItems do tipo Task e Bugs com situação Done por Projeto em determinada iteração (Sprint, release, etc)
        /// </summary>
        /// <param name="nomeProjeto"></param>
        /// <param name="nomeCiclo"></param>
        /// <returns></returns>
        List<WorkItemCoreLatest> ConsultarTasksBugsComSituacaoDonePorProjetoPorCiclo(string nomeProjeto, string nomeCiclo);
    }
}
