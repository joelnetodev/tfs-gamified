 using System;
 using System.Collections.Generic;
 using System.Linq;
 using TfsGamified.Configuration;
 using TfsGamified.Entities;
 using TfsGamified.Entities.Constants;
 using TfsGamified.Repositories.Interfaces;
 using TfsGamified.Services.Interfaces;

namespace TfsGamified.Services.Classes
{
    public class ConstantService : IConstantService
    {
        private readonly IConstantRepository _constantRepository;

        public ConstantService(IConstantRepository constantRepository)
        {
            _constantRepository = constantRepository;
        }

        public List<Constant> ConsultarPorConfiguracaoComImagem(string nomeProjeto)
        {
            return _constantRepository.ConsultarPorIdsComImagem(Configuracao.Xml.LoginsDesenvolvedores(nomeProjeto));
        }
    }
}