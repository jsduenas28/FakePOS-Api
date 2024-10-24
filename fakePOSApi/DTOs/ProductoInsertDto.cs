namespace fakePOSApi.DTOs
{
    public class ProductoInsertDto
    {
        public string CodProducto { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int IDCategoria { get; set; }
    }
}
