namespace TfsGamified.Entities.Dto
{
    public class CampoValorDTO
    {
        public int PartitionId { get; set; }
        public int DataspaceId { get; set; }
        public int Id { get; set; }

        public CampoDTO Campo { get; set; }

        public object Valor { get; set; }

        public TipoValorCampoEnumDTO TipoValor { get; set; }
    }

    public enum TipoValorCampoEnumDTO
    {
        Texto = 1,
        Data = 3,
        Numero = 7,
        Booleano = 15,
        Nulo = 31
    }
}
