using System;
namespace SolidValidation.Validation {
    public abstract class ValidatorBase {
        public string ValidationFailedMessage { get; set; }
    }

    public abstract class ValidatorBaseSync : ValidatorBase {
        public virtual ValidationError Validate(object value) {
            return null;
        }
    }

    public abstract class ValidatorBaseASync : ValidatorBase {
        protected Action<ValidationError> error;
        public virtual void Validate(object value, Action<ValidationError> error) {
            this.error=error;
        }
        protected void CancelValidation() {
            error(null);
        }
        protected void ValidationSuccess() {
            error(null);
        }
        protected void InValidate() {
            if (error == null) {
                throw new Exception("Please call base.Validate(value,error); in overidden Validate functions");
            }
            error(new ValidationError { Validator = this });
        }
    }
}
