namespace fakePOSApi.DTOs
{
    public class DetalleCompraDto
    {
        public int IDDetalleCompra { get; set; }
        public int IDCompra { get; set; }
        public int Cantidad { get; set; }
        public double SubTotal { get; set; }
        public ProductoDto Producto { get; set; }
    }
}
