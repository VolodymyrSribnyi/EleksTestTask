using System;
using System.Collections.Generic;
using System.Text;

namespace Client.DTOs
{
    public record StudentCreateUpdateDto
    {
        public string StudentFirstName { get; set; } = string.Empty;
        public string StudentLastName { get; set; } = string.Empty;
    }
}
