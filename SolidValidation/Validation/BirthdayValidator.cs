using System;
using SolidValidation.ExtensionMethods;

namespace SolidValidation.Validation {
    /// <summary>
    /// Checks if an email addres has is valid format a@b.c
    /// </summary>
    public class BirthdayValidator : ValidatorBaseSync {
        public override ValidationError Validate(object value) {
            if ( (value + "").IsFilled()) {
                DateTime dt = DateTime.Parse(value + "");
                if (dt>DateTime.Now) {
                    return new ValidationError { Validator = this };
                }
            }
            return null;
        }
    }
}
