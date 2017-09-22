using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CustomInfra.DataBase.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.DataBase.Maps
{
    // tbl_Project


    [DbInfraMap]
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
            : this("dbo")
        {
        }

        public ProjectMap(string schema)
        {
            ToTable("tbl_Project", schema);
            HasKey(x => x.PartitionId);

            Property(x => x.PartitionId).HasColumnName(@"PartitionId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ProjectId).HasColumnName(@"ProjectId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DataspaceId).HasColumnName(@"DataspaceId").HasColumnType("int").IsRequired();
            Property(x => x.ProjectUri).HasColumnName(@"ProjectUri").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
            Property(x => x.ProjectName).HasColumnName(@"ProjectName").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
            Property(x => x.SequenceId).HasColumnName(@"SequenceId").HasColumnType("int").IsRequired();
            Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            Property(x => x.IsResolutionStateCustomized).HasColumnName(@"IsResolutionStateCustomized").HasColumnType("bit").IsRequired();
            Property(x => x.IsFailureTypeCustomized).HasColumnName(@"IsFailureTypeCustomized").HasColumnType("bit").IsRequired();
            Property(x => x.MigrationState).HasColumnName(@"MigrationState").HasColumnType("int").IsRequired();
            Property(x => x.MigrationError).HasColumnName(@"MigrationError").HasColumnType("nvarchar(max)").IsOptional();
        }
    }

}
// </auto-generated>
