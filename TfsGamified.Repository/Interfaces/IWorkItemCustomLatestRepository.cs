using System.Collections.Generic;
using CustomInfra.DataBase.Simple.Repository;
using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Repositories.Interfaces
{
    [IoCInfraRegister]
    public interface IWorkItemCustomLatestRepository : IDbInfraRepository<WorkItemCustomLatest>
    {
        /// <summary>
        /// Consulta ultimo valor dos campos de WorkItems
        /// </summary>
        /// <param name="WorkItemsCoreLatest"></param>
        /// <returns></returns>
        List<WorkItemCustomLatest> ConsultarPorItemsCoreLatest(List<WorkItemCoreLatest> WorkItemsCoreLatest);
    }
}
