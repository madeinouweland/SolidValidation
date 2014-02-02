using System;
using System.Collections.Generic;

namespace SolidValidation.Validation {
    /// <summary>
    /// This validator is used in the EditProp to
    /// return an error if the conversion failed
    /// </summary>
    public class ConversionValidator : ValidatorBaseSync {
        public override ValidationError Validate(object value) {
            // this validator always returns a validation error.
            return new ValidationError { Validator = this };
        }
    }
}
