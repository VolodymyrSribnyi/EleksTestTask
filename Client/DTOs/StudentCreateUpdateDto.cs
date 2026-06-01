using System;
using System.Collections.Generic;
using System.Text;

namespace Client.DTOs
{
    public record StudentCreateUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
