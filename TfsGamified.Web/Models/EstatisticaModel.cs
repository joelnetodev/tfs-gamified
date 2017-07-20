using System;
using System.Collections.Generic;

namespace TfsGamified.Web.Models
{
    public class EstatisticaModel
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

        public int MediaAtividadesSemana { get; set; }

        public int QuantidadeAtividadesTotal { get; set; }

        public int QuantidadeAtividadesJogador { get; set; }
        public int PorcentagemAtividadesJogador { get; set; }

        public int QuantidadeAtividadesPerdidas { get; set; }
        public int PorcentagemAtividadesPerdidas { get; set; }

        public int QuantidadeAtividadesReabertas { get; set; }
        public int PorcentagemAtividadesReabertas { get; set; }

        public List<string> Sugestoes { get; set; }
    }
}