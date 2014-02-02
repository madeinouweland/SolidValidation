using System;
using System.Collections.Generic;

// not used yet. this is an example
namespace SolidValidation.Validation {
    /// <summary>
    /// This validator can be used for object-wide validations
    /// like checking 2 conditions of the same object
    /// </summary>
    public class CustomValidator : ValidatorBaseSync {
        public override ValidationError Validate(object value) {
            // this validator always returns a validation error.
            return new ValidationError { Validator = this };
        }
    }
}
