using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class User
    {
        public Guid Id { get; set; }
        public required string UserLogin { get; set; }
        public string UserPassword { get; set; }
    }
}
