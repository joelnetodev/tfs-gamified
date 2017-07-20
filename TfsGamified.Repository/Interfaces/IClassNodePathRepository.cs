using System.Collections.Generic;
using CustomInfra.DataBase.Simple.Repository;
using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Repositories.Interfaces
{
    [IoCInfraRegister]
    public interface IClassNodePathRepository : IDbInfraRepository<ClassificationNodePath>
    {
        /// <summary>
        /// Consulta NodesPath que tenham o nome do projeto ou uri do projeto
        /// </summary>
        /// <param name="nomeProjeto"></param>
        /// /// <param name="uriProjeto"></param>
        /// <returns></returns>
        List<ClassificationNodePath> ConsultarPorNomeOuUri(string nomeProjeto, string uriProjeto);
    }
}
