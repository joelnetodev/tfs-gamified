using System.Collections.Generic;

namespace TfsGamified.Entities.Dto
{
    public class EstatisticaDto
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        

        public byte[] Imagem { get; set; }

        public int MediaAtividadesSemana { get; set; }
        public int QuantidadeAtividadesTotal { get; set; }

        public int QuantidadeAtividadesJogador { get; set; }
        public int PorcentagemAtividadesJogador { get; set; }

        public int QuantidadeAtividadesPerdidas { get; set; }
        public int PorcentagemAtividadesPerdidas { get; set; }

        public int QuantidadeAtividadesReabertas { get; set; }
        public int PorcentagemAtividadesReabertas { get; set; }

    }
}
