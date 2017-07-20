using System.Collections.Generic;
using CustomInfra.DataBase.Simple.Repository;
using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities;
using TfsGamified.Entities.Dto;

namespace TfsGamified.Repositories.Interfaces
{
    [IoCInfraRegister]
    public interface IConstantRepository : IDbInfraRepository<Constant>
    {
        /// <summary>
        /// Consulta Constants (Usuários) por IdsConstants
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<Constant> ConsultarPorIds(List<int> ids);

        /// <summary>
        /// Consulta Constants com imagen em Configuration > Identity por IdsConstants
        /// </summary>
        /// <param name="nomes"></param>
        /// <returns></returns>
        List<Constant> ConsultarPorIdsComImagem(List<string> nomes);
    }
}
