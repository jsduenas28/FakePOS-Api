namespace fakePOSApi.DTOs
{
    public class VentaUpdateDto
    {
        public string Factura { get; set; }
        public DateOnly Fecha { get; set; }
        public string MetodoPago { get; set; }
    }
}
