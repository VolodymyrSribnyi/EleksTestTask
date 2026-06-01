using System;
using System.Collections.Generic;
using System.Text;

namespace Client.DTOs
{
    public record UserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
