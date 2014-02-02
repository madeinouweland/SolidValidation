using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SolidValidation.ExtensionMethods;

namespace SolidValidation.Validation {
    /// <summary>
    /// Checks if an email addres has is valid format a@b.c
    /// </summary>
    public class EmailAddressValidator : ValidatorBaseSync {
        public override ValidationError Validate(object value) {
            if ( (value + "").IsFilled()) {
                var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                if (!regex.IsMatch(value + "")) {
                    return new ValidationError { Validator = this };
                }
            }
            return null;
        }
    }
}
