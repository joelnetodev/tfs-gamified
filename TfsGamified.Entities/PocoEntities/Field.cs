namespace TfsGamified.Entities
{

    // tbl_Field
    
    public class Field
    {
        public int PartitionId { get; set; } // PartitionId (Primary key via unique index PK_tbl_Field)
        public int FieldId { get; set; } // FieldId
        public int ParentFieldId { get; set; } // ParentFieldId
        public string ReferenceName { get; set; } // ReferenceName (length: 70)
        public string Name { get; set; } // Name (length: 128)
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsEditable { get; set; } // IsEditable
        public bool IsSemiEditable { get; set; } // IsSemiEditable
        public int Type { get; set; } // Type
        public int DataType { get; private set; } // DataType
        public int DataSubType { get; private set; } // DataSubType

        public Field()
        {
            ParentFieldId = 0;
            IsDeleted = false;
            IsEditable = true;
            IsSemiEditable = false;
            Type = 16;
        }
    }

}
// </auto-generated>
