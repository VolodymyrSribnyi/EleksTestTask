namespace WebApi.DTOs
{
    public record UserDto
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
