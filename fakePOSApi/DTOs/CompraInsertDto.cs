namespace fakePOSApi.DTOs
{
    public class CompraInsertDto
    {
        public string Factura { get; set; }
        public DateOnly Fecha { get; set; }
        public string MetodoPago { get; set; }
        public List<DetalleCompraInsertDto> DetalleCompra { get; set; }
    }
}
