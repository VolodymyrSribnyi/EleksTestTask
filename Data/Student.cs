using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Student
    {
        public Guid Id { get; set; }
        public required string StudentFirstName { get; set; } 
        public required string StudentLastName { get; set; }
    }
}
