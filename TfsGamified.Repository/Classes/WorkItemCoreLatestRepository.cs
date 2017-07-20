using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CustomInfra.DataBase.Simple.Repository;
using TfsGamified.Entities;
using TfsGamified.Repositories.Interfaces;

namespace TfsGamified.Repositories.Classes
{
  public class WorkItemCoreLatestRepository : DbInfraRepository<WorkItemCoreLatest>, IWorkItemCoreLatestRepository
    {
        public List<WorkItemCoreLatest> ConsultarTasksBugsComSituacaoDonePorArea(List<int> idsAreaNodePath)
        {
            return (from obj in DbEntity

                    where obj.State == WorkItemState.Done
                    && (obj.WorkItemType == WorkItemType.Task || obj.WorkItemType == WorkItemType.Bug)
                    && (obj.AreaId.HasValue && idsAreaNodePath.Contains(obj.AreaId.Value))

                    select obj).AsNoTracking().ToList();
        }

        public List<WorkItemCoreLatest> ConsultarTasksBugsComSituacaoDonePorIteracoes(List<int> idsAreaNodePath, List<int> idsIteracoesNodePath)
        {
            return (from obj in DbEntity

                    where obj.State == WorkItemState.Done
                    && (obj.WorkItemType == WorkItemType.Task || obj.WorkItemType == WorkItemType.Bug)
                    && (obj.AreaId.HasValue && idsAreaNodePath.Contains(obj.AreaId.Value))
                    && (obj.IterationId.HasValue && idsIteracoesNodePath.Contains(obj.IterationId.Value))

                    select obj).AsNoTracking().ToList();
        }
    }
}
