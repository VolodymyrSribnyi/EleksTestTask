namespace WebApi.DTOs
{
    public record StudentCreateUpdateDto
    {
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
    }
}
