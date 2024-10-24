namespace fakePOSApi.DTOs
{
    public class KardexListDto
    {
        public int IDProducto { get; set; }
        public string CodProducto { get; set; }
        public int TotalEntradas { get; set; }
        public int TotalSalidas { get; set; }
        public int StockActual { get; set; }
        public List<KardexDto> Kardex { get; set; }
    }
}
