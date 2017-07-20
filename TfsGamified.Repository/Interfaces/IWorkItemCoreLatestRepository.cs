using System.Collections.Generic;
using CustomInfra.DataBase.Simple.Repository;
using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Repositories.Interfaces
{
    [IoCInfraRegister]
    public interface IWorkItemCoreLatestRepository : IDbInfraRepository<WorkItemCoreLatest>
    {
        /// <summary>
        /// Consulta ultima situação de WorkItems do tipo Task e Bug Por Areas (ClassNodePath)
        /// </summary>
        /// <param name="idsAreaNodePath"></param>
        /// <returns></returns>
        List<WorkItemCoreLatest> ConsultarTasksBugsComSituacaoDonePorArea(List<int> idsAreaNodePath);

        /// <summary>
        /// Consulta ultima situação de WorkItems tipo Task e Bug Por Areas e Por Iteracoes (ClassNodePath)
        /// </summary>
        /// <param name="idsAreaNodePath"></param>
        /// <param name="idsIteracoesNodePath"></param>
        /// <returns></returns>
        List<WorkItemCoreLatest> ConsultarTasksBugsComSituacaoDonePorIteracoes(List<int> idsAreaNodePath, List<int> idsIteracoesNodePath);
    }
}
