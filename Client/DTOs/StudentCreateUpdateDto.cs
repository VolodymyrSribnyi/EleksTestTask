using System;
using System.Collections.Generic;
using System.Text;

namespace Client.DTOs
{
    public record StudentCreateUpdateDto
    {
        public required string StudentFirstName { get; set; }
        public required string StudentLastName { get; set; }
    }
}
