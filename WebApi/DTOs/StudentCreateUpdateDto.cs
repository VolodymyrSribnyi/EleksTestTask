namespace WebApi.DTOs
{
    public record StudentCreateUpdateDto
    {
        public required string StudentFirstName { get; set; }
        public required string StudentLastName { get; set; }
    }
}
