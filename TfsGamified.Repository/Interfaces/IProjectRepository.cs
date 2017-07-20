using System.Collections.Generic;
using CustomInfra.DataBase.Simple.Repository;
using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Repositories.Interfaces
{
    [IoCInfraRegister]
    public interface IProjectRepository : IDbInfraRepository<Project>
    {
        /// <summary>
        /// Obtem Projeto por Nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        Project ObterPorNome(string nome);

        /// <summary>
        /// Consulta projetos aos quais o login está relacionado
        /// </summary>
        /// <param name="login">Login usuário</param>
        /// <returns></returns>
        List<Project> ConsusltarPorLogin(string login);
    }
}
