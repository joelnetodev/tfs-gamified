using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CustomInfra.DataBase.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Repositories.Maps
{
    // tbl_WorkItemCoreWere


    [DbInfraMap]
    public class WorkItemCoreWereMap : EntityTypeConfiguration<WorkItemCoreWere>
    {
        public WorkItemCoreWereMap()
            : this("dbo")
        {
        }

        public WorkItemCoreWereMap(string schema)
        {
            ToTable("tbl_WorkItemCoreWere", schema);
            HasKey(x => new { x.PartitionId, x.DataspaceId, x.Id, x.RevisedDate });

            Property(x => x.PartitionId).HasColumnName(@"PartitionId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.DataspaceId).HasColumnName(@"DataspaceId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Rev).HasColumnName(@"Rev").HasColumnType("int").IsRequired();
            Property(x => x.AuthorizedDate).HasColumnName(@"AuthorizedDate").HasColumnType("datetime").IsRequired();
            Property(x => x.RevisedDate).HasColumnName(@"RevisedDate").HasColumnType("datetime").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.AuthorizedAs).HasColumnName(@"AuthorizedAs").HasColumnType("int").IsRequired();
            Property(x => x.WorkItemType).HasColumnName(@"WorkItemType").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
            Property(x => x.AreaPath).HasColumnName(@"AreaPath").HasColumnType("varbinary").IsRequired().HasMaxLength(60);
            Property(x => x.AreaId).HasColumnName(@"AreaId").HasColumnType("int").IsOptional().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(x => x.IterationPath).HasColumnName(@"IterationPath").HasColumnType("varbinary").IsRequired().HasMaxLength(60);
            Property(x => x.IterationId).HasColumnName(@"IterationId").HasColumnType("int").IsOptional().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired();
            Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").HasColumnType("datetime").IsRequired();
            Property(x => x.ChangedBy).HasColumnName(@"ChangedBy").HasColumnType("int").IsRequired();
            Property(x => x.ChangedDate).HasColumnName(@"ChangedDate").HasColumnType("datetime").IsRequired();
            Property(x => x.State).HasColumnName(@"State").HasColumnType("nvarchar").IsOptional().HasMaxLength(256);
            Property(x => x.Reason).HasColumnName(@"Reason").HasColumnType("nvarchar").IsOptional().HasMaxLength(256);
            Property(x => x.AssignedTo).HasColumnName(@"AssignedTo").HasColumnType("int").IsOptional();
            Property(x => x.Watermark).HasColumnName(@"Watermark").HasColumnType("int").IsRequired();
        }
    }

}
// </auto-generated>
