 namespace fakePOSApi.DTOs
{
    public class DetalleVentaDto
    {
        public int IDDetalleVenta { get; set; }
        public int IDVenta { get; set; }
        public int IDProducto { get; set; }
        public int Cantidad { get; set; }
        public double SubTotal { get; set; }
    }
}
