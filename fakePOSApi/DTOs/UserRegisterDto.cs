namespace fakePOSApi.DTOs
{
    public class UserRegisterDto
    {
        public string CodUser { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
