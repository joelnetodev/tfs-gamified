using System.Collections.Generic;
using CustomInfra.DataBase.Simple.Repository;
using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Repositories.Interfaces
{
    [IoCInfraRegister]
    public interface IWorkItemCustomWereRepository : IDbInfraRepository<WorkItemCustomWere>
    {
        /// <summary>
        /// Consulta historico de valor dos campos de WorkItems
        /// </summary>
        /// <returns></returns>
        List<WorkItemCustomWere> ConsultarPorItemsCoreLatest(List<WorkItemCoreLatest> WorkItemsCoreLatest);
    }
}
