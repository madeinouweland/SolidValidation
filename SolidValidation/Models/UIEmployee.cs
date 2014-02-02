using System;
using SolidValidation.Validation;

namespace SolidValidation.Models {
    public class UIEmployee : EditableObject {
        public EditProp<string> Username { get; set; }
        public EditProp<string> Name { get; set; }
        public EditProp<decimal> Salary { get; set; }
        public EditProp<DateTime?> BirthDate { get; set; }
        public EditProp<string> Email { get; set; }
        public EditProp<Function> Function { get; set; }
    }
}
