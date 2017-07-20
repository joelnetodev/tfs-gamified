using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CustomInfra.DataBase.Simple.Repository;
using TfsGamified.Entities;
using TfsGamified.Repositories.Interfaces;

namespace TfsGamified.Repositories.Classes
{
 public class FieldRepository : DbInfraRepository<Field>, IFieldRepository
    {
        public List<Field> ConsultarPorIds(List<int> ids)
        {
            return (from obj in DbEntity
                    where ids.Contains(obj.FieldId)
                    select obj).AsNoTracking().ToList();
        }

        public List<Field> ConsultarPorNomes(List<string> nomes)
        {
            return (from obj in DbEntity
                    where nomes.Contains(obj.Name)
                    select obj).AsNoTracking().ToList();
        }
    }
}
