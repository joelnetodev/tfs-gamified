using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CustomInfra.DataBase.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.DataBase.Maps
{
    // tbl_Project


    [DbInfraMap]
    public class ProjectsMap : EntityTypeConfiguration<Projects>
    {
        public ProjectsMap()
            : this("dbo")
        {
        }

        public ProjectsMap(string schema)
        {
            ToTable("tbl_projects", schema);
            HasKey(x => x.ProjectIdUri);

            Property(x => x.PartitionId).HasColumnName(@"PartitionId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ProjectIdUri).HasColumnName(@"project_id").HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.ProjectName).HasColumnName(@"project_name").HasColumnType("nvarchar").IsRequired();
        }

    }

}
// </auto-generated>
