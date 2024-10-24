namespace fakePOSApi.DTOs
{
    public class CompraUpdateDto
    {
        public string Factura { get; set; }
        public DateOnly Fecha { get; set; }
        public string MetodoPago { get; set; }
    }
}
