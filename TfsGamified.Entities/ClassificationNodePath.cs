namespace TfsGamified.Entities
{

    // tbl_ClassificationNodePath
    
    public class ClassificationNodePath
    {
        public int PartitionId { get; set; } // PartitionId (Primary key via unique index PK_tbl_ClassificationNodePath)
        public int DataspaceId { get; set; } // DataspaceId (Primary key via unique index PK_tbl_ClassificationNodePath)
        public int Id { get; set; } // Id (Primary key via unique index PK_tbl_ClassificationNodePath)
        public string NodeName { get; set; } // NodeName (length: 255)
        public bool IsDeleted { get; set; } // IsDeleted
        public string TeamProject { get; set; } // TeamProject (length: 255)
        public string AreaLevel1 { get; set; } // AreaLevel1 (length: 255)
        public string AreaLevel2 { get; set; } // AreaLevel2 (length: 255)
        public string AreaLevel3 { get; set; } // AreaLevel3 (length: 255)
        public string AreaLevel4 { get; set; } // AreaLevel4 (length: 255)
        public string AreaLevel5 { get; set; } // AreaLevel5 (length: 255)
        public string AreaLevel6 { get; set; } // AreaLevel6 (length: 255)
        public string AreaLevel7 { get; set; } // AreaLevel7 (length: 255)
        public string IterationLevel1 { get; set; } // IterationLevel1 (length: 255)
        public string IterationLevel2 { get; set; } // IterationLevel2 (length: 255)
        public string IterationLevel3 { get; set; } // IterationLevel3 (length: 255)
        public string IterationLevel4 { get; set; } // IterationLevel4 (length: 255)
        public string IterationLevel5 { get; set; } // IterationLevel5 (length: 255)
        public string IterationLevel6 { get; set; } // IterationLevel6 (length: 255)
        public string IterationLevel7 { get; set; } // IterationLevel7 (length: 255)
        public string AreaPath { get; set; } // AreaPath (length: 4000)
        public string IterationPath { get; private set; } // IterationPath (length: 4000)
        public byte[] Path { get; set; } // Path (length: 60)
    }
}
// </auto-generated>
