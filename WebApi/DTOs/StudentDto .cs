namespace WebApi.DTOs
{
    public record StudentDto
    {
        public Guid Id { get; set; }
        public required string StudentFirstName { get; set; }
        public required string StudentLastName { get; set; }
    }
}
