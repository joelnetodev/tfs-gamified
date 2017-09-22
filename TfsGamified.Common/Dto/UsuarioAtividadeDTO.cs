using System.Collections.Generic;

namespace TfsGamified.Entities.Dto
{
    public class UsuarioAtividadeDTO
    {
        public UsuarioInfoDTO UsuarioInfo { get; set; }
        public List<AtividadeDTO> Atividades { get; set; }

        public UsuarioAtividadeDTO()
        {
            Atividades = new List<AtividadeDTO>();
        }
    }
}
