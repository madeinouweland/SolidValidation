using System;
using System.Diagnostics;

namespace SolidValidation.Models {
    [DebuggerDisplay("Name: {Name}")]
    public class Employee {
        public string Username { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public Function Function { get; set; }
    }
}
