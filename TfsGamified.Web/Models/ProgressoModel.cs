using System;
using System.Collections.Generic;

namespace TfsGamified.Web.Models
{
    public class ProgressoModel
    {
        public string Nome { get; set; }

        public string Login { get; set; }
        public byte[] ImagemByte { private get; set; }

        public string Imagem
        {
            get { return ImagemByte != null 
                    ? string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(ImagemByte))
                    : null; }
        }

        public int Posicao { get; set; }

        public int Pontuacao { get; set; }

        public bool PremioTarefa { get; set; }

        public bool PremioProblema { get; set; }

        public bool EmblemaSuporte { get; set; }

        public bool EmblemaHalter { get; set; }

        public bool EmblemaCerteiro { get; set; }

        public bool EmblemaAjudante { get; set; }

        public bool EmblemaResolvedor { get; set; }

        public List<string> Sugestoes { get; set; }

        public ProgressoModel()
        {
            Sugestoes = new List<string>();
        }
    }
}