using System.Collections.Generic;

namespace TfsGamified.Entities.Dto
{
    public class ProgressoDto
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        
        public int Posicao { get; set; }

        public int Pontuacao { get; set; }

        public byte[] Imagem { get; set; }

        public List<EmblemaEnum> Emblemas { get; set; }

        public List<PremioEnum> Premios { get; set; }

        public List<string> Sugestoes { get; set; }

        public ProgressoDto()
        {
            Emblemas = new List<EmblemaEnum>();
            Premios = new List<PremioEnum>();

            Sugestoes = new List<string>();
        }
    }

    public enum EmblemaEnum
    {
        Suporte = 1,
        Halterofilista = 3,
        Certeiro = 7,
        Ajudante = 15,
        Resolvedor = 31,
    }

    public enum PremioEnum
    {
        Tarefa = 1,
        Problema = 3
    }
}
