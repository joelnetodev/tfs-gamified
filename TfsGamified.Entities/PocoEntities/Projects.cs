using System;

namespace TfsGamified.Entities
{

    // tbl_projects
    
    public class Projects
    {
        public int PartitionId { get; set; } // PartitionId (Primary key via unique index ixc_Project)
        public System.Guid ProjectIdUri { get; set; } // ProjectId
        public string ProjectName { get; set; } // ProjectName (length: 256)
    }

}
// </auto-generated>
