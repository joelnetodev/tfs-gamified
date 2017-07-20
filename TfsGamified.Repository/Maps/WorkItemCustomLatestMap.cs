using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CustomInfra.DataBase.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Repositories.Maps
{
    // tbl_WorkItemCustomLatest


    [DbInfraMap]
    public class WorkItemCustomLatestMap : EntityTypeConfiguration<WorkItemCustomLatest>
    {
        public WorkItemCustomLatestMap()
            : this("dbo")
        {
        }

        public WorkItemCustomLatestMap(string schema)
        {
            ToTable("tbl_WorkItemCustomLatest", schema);
            HasKey(x => new { x.PartitionId, x.DataspaceId, x.Id, x.FieldId, x.RevisedDate });

            Property(x => x.PartitionId).HasColumnName(@"PartitionId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.DataspaceId).HasColumnName(@"DataspaceId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.FieldId).HasColumnName(@"FieldId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.AuthorizedDate).HasColumnName(@"AuthorizedDate").HasColumnType("datetime").IsRequired();
            Property(x => x.RevisedDate).HasColumnName(@"RevisedDate").HasColumnType("datetime").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.IntValue).HasColumnName(@"IntValue").HasColumnType("int").IsOptional();
            Property(x => x.FloatValue).HasColumnName(@"FloatValue").HasColumnType("float").IsOptional();
            Property(x => x.DateTimeValue).HasColumnName(@"DateTimeValue").HasColumnType("datetime").IsOptional();
            Property(x => x.GuidValue).HasColumnName(@"GuidValue").HasColumnType("uniqueidentifier").IsOptional();
            Property(x => x.BitValue).HasColumnName(@"BitValue").HasColumnType("bit").IsOptional();
            Property(x => x.StringValue).HasColumnName(@"StringValue").HasColumnType("nvarchar").IsOptional().HasMaxLength(256);
            Property(x => x.TextValue).HasColumnName(@"TextValue").HasColumnType("nvarchar").IsOptional().HasMaxLength(256);
        }
    }

}
// </auto-generated>
