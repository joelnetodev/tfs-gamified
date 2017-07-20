using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CustomInfra.DataBase.Simple.Repository;
using TfsGamified.Entities;
using TfsGamified.Repositories.Interfaces;

namespace TfsGamified.Repositories.Classes
{
    public class ClassNodePathRepository : DbInfraRepository<ClassificationNodePath>, IClassNodePathRepository
    {
        public List<ClassificationNodePath> ConsultarPorNomeOuUri(string nomeProjeto, string uriProjeto)
        {
            return (from obj in DbEntity
                    where obj.TeamProject.ToLower().Equals(nomeProjeto.ToLower()) 
                    || obj.TeamProject.ToLower().Equals(uriProjeto.ToLower())
                    select obj).AsNoTracking().ToList();
        }
    }
}
