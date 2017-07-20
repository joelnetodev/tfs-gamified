using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using CustomInfra.DataBase.Simple.Repository;
using TfsGamified.Entities;
using TfsGamified.Repositories.Interfaces;

namespace TfsGamified.Repositories.Classes
{
    public class ProjectRepository : DbInfraRepository<Project>, IProjectRepository
    {
        public Project ObterPorNome(string nome)
        {
            return (from obj in DbEntity
                where obj.ProjectName.ToLower().Equals(nome.ToLower())
                select obj).AsNoTracking().FirstOrDefault();
        }

        public List<Project> ConsusltarPorLogin(string login)
        {
            throw new NotImplementedException();
        }
    }
}