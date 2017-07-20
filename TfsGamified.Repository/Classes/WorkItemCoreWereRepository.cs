using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CustomInfra.DataBase.Simple.Repository;
using TfsGamified.Entities;
using TfsGamified.Repositories.Interfaces;

namespace TfsGamified.Repositories.Classes
{
   public class WorkItemCoreWereRepository : DbInfraRepository<WorkItemCoreWere>, IWorkItemCoreWereRepository
    {
        public List<WorkItemCoreWere> ConsultarPorItemsCoreLatest(List<WorkItemCoreLatest> WorkItemsCoreLatest)
        {
            var idsPartition = WorkItemsCoreLatest.Select(x => x.PartitionId).Distinct().ToArray();

            var idsDataSpace = WorkItemsCoreLatest.Select(x => x.DataspaceId).Distinct().ToArray();

            var ids = WorkItemsCoreLatest.Select(x => x.Id).Distinct().ToArray();

            return (from obj in DbEntity

                    where

                   idsPartition.Contains(obj.PartitionId)
                   && idsDataSpace.Contains(obj.DataspaceId)
                   && ids.Contains(obj.Id)

                    select obj).AsNoTracking().ToList();
        }
    }
}
