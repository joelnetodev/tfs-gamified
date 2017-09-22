using System;
using System.Collections.Generic;

namespace TfsGamified.Entities.Dto
{
    public class AtividadeDTO
    {
        public int PartitionId { get; set; }
        public int DataspaceId { get; set; }
        public int Id { get; set; }
        public int Ordem { get; set; }

        public DateTime Data { get; set; }

        public UsuarioInfoDTO  UsuarioInfo { get; set; }

        public SituacaoAtividadeEnum Situacao { get; set; }

        public TipoAtividadeEnum Tipo { get; set; }

        public List<AtividadeDTO> Historico { get; set; }

        public List<CampoValorDTO> CamposValores { get; set; }

        public AtividadeDTO()
        {
            Historico = new List<AtividadeDTO>();
            CamposValores = new List<CampoValorDTO>();
        }
    }

    public enum SituacaoAtividadeEnum
    {
        Afazer = 1,
        Fazendo = 3,
        Feito = 7,
        Teste = 15
    }

    public enum TipoAtividadeEnum
    {
        Tarefa = 1,
        Bug = 3
    }
}
