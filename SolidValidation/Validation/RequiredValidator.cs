using System;
using System.Collections.Generic;
using SolidValidation.ExtensionMethods;

namespace SolidValidation.Validation {
    public class RequiredValidator : ValidatorBaseSync {
        public override ValidationError Validate(object value) {
            if ((value + "").IsEmpty()) {
                return new ValidationError { Validator=this };
            }
            return null;
        }
    }
}
