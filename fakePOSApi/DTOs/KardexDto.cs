namespace fakePOSApi.DTOs
{
    public class KardexDto
    {
        public int IDKardex { get; set; }
        public int IDProducto { get; set; }
        public string NumDocumento { get; set; }
        public string TipoMovimiento { get; set; }
        public int Entrada { get; set; }
        public int Salida { get; set; }
        public DateOnly Fecha { get; set; }
        public UsuarioDto Usuario { get; set; }
    }
}
