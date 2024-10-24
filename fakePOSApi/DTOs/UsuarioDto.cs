namespace fakePOSApi.DTOs
{
    public class UsuarioDto
    {
        public int IDUser { get; set; }
        public string CodUser { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }
}
