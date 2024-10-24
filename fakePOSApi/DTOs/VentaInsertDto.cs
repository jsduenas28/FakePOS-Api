namespace fakePOSApi.DTOs
{
    public class VentaInsertDto
    {
        public string Factura { get; set; }
        public DateOnly Fecha { get; set; }
        public string MetodoPago { get; set; }
        public List<DetalleVentaInsertDto> DetalleVenta { get; set; }
    }
}
