using System.Collections.Generic;
using CustomInfra.Injector.Simple.Attribute;
using TfsGamified.Entities;

namespace TfsGamified.Services.Interfaces
{
    [IoCInfraRegister]
    public interface IFieldService
    {
        /// <summary>
        /// Consulta os campos de valor estimado e realizado
        /// </summary>
        /// <returns>Lista de campos</returns>
        List<Field> ConsultarFieldPorConfiguracao();


        /// <summary>
        /// Consulta os campos de valor estimado, realizado nas configurações e título
        /// </summary>
        /// <returns>Lista de campos</returns>
        List<Field> ConsultarFieldPorConfiguracaoComTitulo();
    }
}
