using System;
using System.Collections.Generic;
using System.Text;

namespace Client.DTOs
{
    public record UserDto
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
