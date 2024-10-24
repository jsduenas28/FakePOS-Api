namespace fakePOSApi.DTOs
{
    public class ProductoDto
    {
        public int IDProducto { get; set; }
        public string CodProducto { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public double Precio { get; set; }
        public int IDCategoria { get; set; }
    }
}
