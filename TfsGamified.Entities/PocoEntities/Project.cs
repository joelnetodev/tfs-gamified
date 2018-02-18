namespace TfsGamified.Entities
{

    // tbl_Project

    public class Project
    {
        public int PartitionId { get; set; } // PartitionId (Primary key via unique index ixc_Project)
        public int ProjectId { get; set; } // ProjectId
        public int DataspaceId { get; set; } // DataspaceId
        public string ProjectUri { get; set; } // ProjectUri (length: 256)
        public string ProjectName { get; set; } // ProjectName (length: 256)
        public int SequenceId { get; set; } // SequenceId
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsResolutionStateCustomized { get; set; } // IsResolutionStateCustomized
        public bool IsFailureTypeCustomized { get; set; } // IsFailureTypeCustomized
        public int MigrationState { get; set; } // MigrationState
        public string MigrationError { get; set; } // MigrationError

        public Project()
        {
            IsDeleted = false;
            IsResolutionStateCustomized = false;
            IsFailureTypeCustomized = false;
            MigrationState = 0;
        }

        public string Uri { get
            {
                var uriSplited = ProjectUri.Split('/');

                return uriSplited.Length > 0 ? uriSplited[uriSplited.Length - 1] : string.Empty;
            } }
    }

}
// </auto-generated>
