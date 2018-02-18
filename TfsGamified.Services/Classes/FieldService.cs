using System.Collections.Generic;
using TfsGamified.Configuration;
using TfsGamified.Entities;
using TfsGamified.Entities.Constants;
using TfsGamified.Repositories.Interfaces;
using TfsGamified.Services.Interfaces;

namespace TfsGamified.Services.Classes
{
    public class FieldService : IFieldService
    {
        private readonly IFieldRepository _fieldRepository;

        public FieldService(IFieldRepository fieldRepository)
        {
            _fieldRepository = fieldRepository;
        }

        public List<Field> ConsultarFieldPorConfiguracao()
        {
            List<string> nomes = new List<string> {
                Configuracao.Xml.CampoEstimadoTarefa,
                Configuracao.Xml.CampoRealizadoTarefa,
                Configuracao.Xml.CampoEstimadoProblema,
                Configuracao.Xml.CampoRealizadoProblema };

            return _fieldRepository.ConsultarPorNomes(nomes);
        }

        public List<Field> ConsultarFieldPorConfiguracaoComTitulo()
        {
            List<string> nomes = new List<string>{
                Configuracao.Xml.CampoEstimadoTarefa,
                Configuracao.Xml.CampoRealizadoTarefa,
                Configuracao.Xml.CampoEstimadoProblema,
                Configuracao.Xml.CampoRealizadoProblema,
                FieldsName.Title };

            return _fieldRepository.ConsultarPorNomes(nomes);
        }
    }
}