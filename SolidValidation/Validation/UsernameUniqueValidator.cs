using System;
using SolidValidation.ExtensionMethods;
using SolidValidation.Models;

namespace SolidValidation.Validation {
    public class UsernameUniqueValidator : ValidatorBaseASync  {
        private Employee oldEmployee;
        public UsernameUniqueValidator(Employee oldEmployee){
            this.oldEmployee = oldEmployee;
        }
        public override void Validate(object value, Action<ValidationError> error) {
            base.Validate(value,error);
            string username = value + "";
            if (username.IsFilled()) {
                if (oldEmployee != null && username == oldEmployee.Username) {
                    // TODO: probleem...als je niet verder wil valideren moet je zelf error(null) aanroepen.
                    // Als je dat vergeet, komt je validator nooit uit het asynchrone stukje
                    CancelValidation();
                    return;
                }
                // simulate that username "loek" already exists 
                if (username == "loek") {
                    InValidate();
                } else {
                    // TODO: zelfde probleem...als je geen errors bent tegengekomen,
                    // moet je zelf error(null) aanroepen.
                    // Als je dat vergeet, komt je validator nooit uit het asynchrone stukje
                    ValidationSuccess();
                }
            }
        }
    }
}
