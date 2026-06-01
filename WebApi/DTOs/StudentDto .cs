namespace WebApi.DTOs
{
    public record StudentDto
    {
        public Guid Id { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
    }
}
