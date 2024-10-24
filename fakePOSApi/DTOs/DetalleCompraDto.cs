namespace fakePOSApi.DTOs
{
    public class DetalleCompraDto
    {
        public int IDDetalleCompra { get; set; }
        public int IDCompra { get; set; }
        public int IDProducto { get; set; }
        public int Cantidad { get; set; }
        public double SubTotal { get; set; }
    }
}
