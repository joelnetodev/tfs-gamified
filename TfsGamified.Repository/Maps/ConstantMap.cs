using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CustomInfra.DataBase.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Repositories.Maps
{
    // Constants


    [DbInfraMap]
    public class ConstantMap : EntityTypeConfiguration<Constant>
    {
        public ConstantMap()
            : this("dbo")
        {
        }

        public ConstantMap(string schema)
        {
            ToTable("Constants", schema);
            HasKey(x => x.PartitionId);

            Property(x => x.PartitionId).HasColumnName(@"PartitionId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ConstId).HasColumnName(@"ConstID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DomainPart).HasColumnName(@"DomainPart").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
            Property(x => x.FInTrustedDomain).HasColumnName(@"fInTrustedDomain").HasColumnType("bit").IsRequired();
            Property(x => x.NamePart).HasColumnName(@"NamePart").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
            Property(x => x.IdentityDisplayName).HasColumnName(@"IdentityDisplayName").HasColumnType("nvarchar").IsOptional().HasMaxLength(256); 
            Property(x => x.DisplayPart).HasColumnName(@"DisplayPart").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
            Property(x => x.String).HasColumnName(@"String").HasColumnType("nvarchar").IsRequired().HasMaxLength(513).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(x => x.ChangerId).HasColumnName(@"ChangerID").HasColumnType("int").IsRequired();
            Property(x => x.AddedDate).HasColumnName(@"AddedDate").HasColumnType("datetime").IsRequired();
            Property(x => x.RemovedDate).HasColumnName(@"RemovedDate").HasColumnType("datetime").IsRequired();
        }
    }

}
// </auto-generated>
