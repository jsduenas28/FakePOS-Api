namespace fakePOSApi.DTOs
{
    public class VentaDto
    {
        public int IDVenta { get; set; }
        public string Factura { get; set; }
        public DateOnly Fecha { get; set; }
        public string MetodoPago { get; set; }
        public double TotalVenta { get; set; }
        public bool IsContable { get; set; }
        public UsuarioDto Usuario { get; set; }
        public List<DetalleVentaDto> DetalleVenta { get; set; }
    }
}
