 namespace fakePOSApi.DTOs
{
    public class DetalleVentaDto
    {
        public int IDDetalleVenta { get; set; }
        public int IDVenta { get; set; }
        public int Cantidad { get; set; }
        public double SubTotal { get; set; }
        public ProductoDto Producto { get; set; }
    }
}
