using System.Collections.Generic;

namespace TfsGamified.Entities.Dto
{
    public class ProjetoUsuariosAtividadesDTO
    {
        public string NomeProjeto { get; set; }
        public List<UsuarioAtividadeDTO> UsuariosAtividades { get; set; }

        public ProjetoUsuariosAtividadesDTO(string nomeProjeto = null)
        {
            NomeProjeto = nomeProjeto;
            UsuariosAtividades = new List<UsuarioAtividadeDTO>();
        }
    }
}
