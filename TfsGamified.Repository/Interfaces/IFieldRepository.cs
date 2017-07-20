using System.Collections.Generic;
using CustomInfra.DataBase.Simple.Repository;
using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Repositories.Interfaces
{
    [IoCInfraRegister]
    public interface IFieldRepository : IDbInfraRepository<Field>
    {
        /// <summary>
        /// Consulta Fields por IdsFields
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<Field> ConsultarPorIds(List<int> ids);

        /// <summary>
        /// Consulta Fields por nomes dos campos
        /// </summary>
        /// <param name="nomes"></param>
        /// <returns></returns>
        List<Field> ConsultarPorNomes(List<string> nomes);
    }
}
