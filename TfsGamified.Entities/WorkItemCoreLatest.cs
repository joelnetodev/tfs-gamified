using System;

namespace TfsGamified.Entities
{

    // tbl_WorkItemCoreLatest
    
    public class WorkItemCoreLatest
    {
        public int PartitionId { get; set; } // PartitionId (Primary key via unique index PK_tbl_WorkItemCoreLatest)
        public int DataspaceId { get; set; } // DataspaceId (Primary key via unique index PK_tbl_WorkItemCoreLatest)
        public int Id { get; set; } // Id (Primary key via unique index PK_tbl_WorkItemCoreLatest)
        public int Rev { get; set; } // Rev
        public DateTime AuthorizedDate { get; set; } // AuthorizedDate
        public DateTime RevisedDate { get; set; } // RevisedDate
        public int AuthorizedAs { get; set; } // AuthorizedAs
        public string WorkItemType { get; set; } // WorkItemType (length: 256)
        public byte[] AreaPath { get; set; } // AreaPath (length: 60)
        public int? AreaId { get; private set; } // AreaId
        public byte[] IterationPath { get; set; } // IterationPath (length: 60)
        public int? IterationId { get; private set; } // IterationId
        public int CreatedBy { get; set; } // CreatedBy
        public DateTime CreatedDate { get; set; } // CreatedDate
        public int ChangedBy { get; set; } // ChangedBy
        public DateTime ChangedDate { get; set; } // ChangedDate
        public string State { get; set; } // State (length: 256)
        public string Reason { get; set; } // Reason (length: 256)
        public int? AssignedTo { get; set; } // AssignedTo
        public int Watermark { get; set; } // Watermark

        public int IdUsuarioPadrao
        {
            get { return AssignedTo.HasValue ? AssignedTo.Value : ChangedBy; }
        }
    }

}
// </auto-generated>
