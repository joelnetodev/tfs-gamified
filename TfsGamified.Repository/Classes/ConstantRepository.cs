using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using CustomInfra.DataBase.Simple.Repository;
using TfsGamified.Entities;
using TfsGamified.Entities.Constants;
using TfsGamified.Entities.Dto;
using TfsGamified.Repositories.Interfaces;

namespace TfsGamified.Repositories.Classes
{
    public class ConstantRepository : DbInfraRepository<Constant>, IConstantRepository
    {
        public List<Constant> ConsultarPorIds(List<int> ids)
        {
            return (from obj in DbEntity
                    where ids.Contains(obj.ConstId)
                    select obj).AsNoTracking().ToList();
        }

        public List<Constant> ConsultarPorIdsComImagem(List<string> nomes)
        {
            if(!nomes.Any())
                return new List<Constant>();

            var nomesQuery = new List<string>();
            nomes.ForEach(x => nomesQuery.Add("'"+x+"'"));

            StringBuilder bQuery = new StringBuilder();

            bQuery.Append("    select C.*, V.BinaryValue as ImageData                             ");
            bQuery.Append("    from Constants C                                                   ");

            bQuery.Append("    inner join [Tfs_Configuration].[dbo].[tbl_Identity] I              ");
            bQuery.Append("    on I.Sid = C.SID                                                   ");

            bQuery.Append("    left join [Tfs_Configuration].[dbo].[tbl_PropertyValue] V          ");
            bQuery.Append("    on V.ArtifactId = CONVERT(varbinary(64), I.Id)                     ");

            bQuery.Append("    left join [Tfs_Configuration].[dbo].[tbl_PropertyDefinition] P     ");
            bQuery.Append("    on P.PropertyId = V.PropertyId                                     ");
            bQuery.Append("    and P.Name = '{0}'                                                 ");

            bQuery.Append("    where I.AccountName in ({1})                                       ");

            var query = string.Format(bQuery.ToString(), PropertyNames.IdentityImageData, string.Join(",", nomesQuery));

            return base.SqlQuery<Constant>(query).ToList();
        }
    }
}
