using System.Collections.Generic;
using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Services.Interfaces
{
    [IoCInfraRegister]
    public interface IConstantService
    {
        /// <summary>
        /// Consulta as constants referentes aos logins nas configurações com imagem
        /// </summary>
        /// <param name="nomeProjeto"></param>
        /// <returns></returns>
        List<Constant> ConsultarPorConfiguracaoComImagem(string nomeProjeto);
    }
}
