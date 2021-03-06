using System;

namespace TfsGamified.Entities
{

    // tbl_WorkItemCustomWere
    
    public class WorkItemCustomWere
    {
        public int PartitionId { get; set; } // PartitionId (Primary key via unique index PK_tbl_WorkItemCustomWere)
        public int DataspaceId { get; set; } // DataspaceId (Primary key via unique index PK_tbl_WorkItemCustomWere)
        public int Id { get; set; } // Id (Primary key via unique index PK_tbl_WorkItemCustomWere)
        public int FieldId { get; set; } // FieldId (Primary key via unique index PK_tbl_WorkItemCustomWere)
        public DateTime AuthorizedDate { get; set; } // AuthorizedDate
        public DateTime RevisedDate { get; set; } // RevisedDate (Primary key via unique index PK_tbl_WorkItemCustomWere)
        public int? IntValue { get; set; } // IntValue
        public double? FloatValue { get; set; } // FloatValue
        public DateTime? DateTimeValue { get; set; } // DateTimeValue
        public Guid? GuidValue { get; set; } // GuidValue
        public bool? BitValue { get; set; } // BitValue
        public string StringValue { get; set; } // StringValue (length: 256)
        public string TextValue { get; set; } // TextValue (length: 256)
    }
}
// </auto-generated>
