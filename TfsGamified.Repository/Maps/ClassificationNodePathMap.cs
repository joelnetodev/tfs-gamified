using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CustomInfra.DataBase.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Repositories.Maps
{
    // tbl_ClassificationNodePath
    

	[DbInfraMap]
    public class ClassificationNodePathMap : EntityTypeConfiguration<ClassificationNodePath>
    {
        public ClassificationNodePathMap()
            : this("dbo")
        {
        }

        public ClassificationNodePathMap(string schema)
        {
            ToTable("tbl_ClassificationNodePath", schema);
            HasKey(x => new { x.PartitionId, x.DataspaceId, x.Id });

            Property(x => x.PartitionId).HasColumnName(@"PartitionId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.DataspaceId).HasColumnName(@"DataspaceId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.NodeName).HasColumnName(@"NodeName").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            Property(x => x.TeamProject).HasColumnName(@"TeamProject").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.AreaLevel1).HasColumnName(@"AreaLevel1").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.AreaLevel2).HasColumnName(@"AreaLevel2").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.AreaLevel3).HasColumnName(@"AreaLevel3").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.AreaLevel4).HasColumnName(@"AreaLevel4").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.AreaLevel5).HasColumnName(@"AreaLevel5").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.AreaLevel6).HasColumnName(@"AreaLevel6").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.AreaLevel7).HasColumnName(@"AreaLevel7").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.IterationLevel1).HasColumnName(@"IterationLevel1").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.IterationLevel2).HasColumnName(@"IterationLevel2").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.IterationLevel3).HasColumnName(@"IterationLevel3").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.IterationLevel4).HasColumnName(@"IterationLevel4").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.IterationLevel5).HasColumnName(@"IterationLevel5").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.IterationLevel6).HasColumnName(@"IterationLevel6").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.IterationLevel7).HasColumnName(@"IterationLevel7").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.AreaPath).HasColumnName(@"AreaPath").HasColumnType("nvarchar").IsRequired().HasMaxLength(4000);
            Property(x => x.IterationPath).HasColumnName(@"IterationPath").HasColumnType("nvarchar").IsRequired().HasMaxLength(4000).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(x => x.Path).HasColumnName(@"Path").HasColumnType("varbinary").IsRequired().HasMaxLength(60);
        }
    }

}
// </auto-generated>
