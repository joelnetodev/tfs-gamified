using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CustomInfra.DataBase.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Repositories.Maps
{
    // tbl_Field


    [DbInfraMap]
    public class FieldMap : EntityTypeConfiguration<Field>
    {
        public FieldMap()
            : this("dbo")
        {
        }

        public FieldMap(string schema)
        {
            ToTable("tbl_Field", schema);
            HasKey(x => x.PartitionId);

            Property(x => x.PartitionId).HasColumnName(@"PartitionId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.FieldId).HasColumnName(@"FieldId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ParentFieldId).HasColumnName(@"ParentFieldId").HasColumnType("int").IsRequired();
            Property(x => x.ReferenceName).HasColumnName(@"ReferenceName").HasColumnType("nvarchar").IsRequired().HasMaxLength(70);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(128);
            Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            Property(x => x.IsEditable).HasColumnName(@"IsEditable").HasColumnType("bit").IsRequired();
            Property(x => x.IsSemiEditable).HasColumnName(@"IsSemiEditable").HasColumnType("bit").IsRequired();
            Property(x => x.Type).HasColumnName(@"Type").HasColumnType("int").IsRequired();
            Property(x => x.DataType).HasColumnName(@"DataType").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(x => x.DataSubType).HasColumnName(@"DataSubType").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

}
// </auto-generated>
