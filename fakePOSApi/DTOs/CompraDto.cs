namespace fakePOSApi.DTOs
{
    public class CompraDto
    {
        public int IDCompra { get; set; }
        public string Factura { get; set; }
        public DateOnly Fecha { get; set; }
        public string MetodoPago { get; set; }
        public double TotalCompra { get; set; }
        public UsuarioDto Usuario { get; set; }
        public List<DetalleCompraDto> DetalleCompra { get; set; }
    }
}
