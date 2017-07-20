using System.Collections.Generic;
using CustomInfra.DataBase.Simple.Repository;
using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Repositories.Interfaces
{
    [IoCInfraRegister]
    public interface IWorkItemCoreWereRepository : IDbInfraRepository<WorkItemCoreWere>
    {
        /// <summary>
        /// Consulta histórico de WorkItem e situação por WorkItemsCoreLatest
        /// </summary>
        /// <param name="WorkItemsCoreLatest"></param>
        /// <returns></returns>
        List<WorkItemCoreWere> ConsultarPorItemsCoreLatest(List<WorkItemCoreLatest> WorkItemsCoreLatest);
    }
}
