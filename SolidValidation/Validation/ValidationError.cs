namespace SolidValidation.Validation {
    /// <summary>
    /// These errors are added to EditProps
    /// They contain the original validator
    /// </summary>
    public class ValidationError {
        public ValidatorBase Validator { get; set; }
        public string Message { get { return Validator.ValidationFailedMessage; } }
    }
}
