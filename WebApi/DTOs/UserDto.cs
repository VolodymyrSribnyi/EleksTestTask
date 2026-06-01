namespace WebApi.DTOs
{
    public record UserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
