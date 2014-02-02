using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using SolidValidation.ViewModels;

namespace SolidValidation.Validation {
    /// <summary>
    /// Use this property to bind UI Elements to
    /// Value is the typed value
    /// Text is the user input
    /// </summary>
    [DebuggerDisplay("Text: {Text}")]
    public class EditProp<T> : ObservableObject, IEditProp {
        private List<ValidatorBase> validators = new List<ValidatorBase>();
        public IConverter ValidatorConverter { get; set; }
        public EditProp() {
            ValidatorConverter = new DefaultConverter<T>();
            ValidationErrors = new ObservableCollection<ValidationError>();
            ValidationErrors.CollectionChanged += delegate {
                UpdateErrorLabel();
                HasError = ValidationErrors.Count > 0;
            };
        }
        public string Caption { get; set; }
        public bool IsNullable {
            get {
                if (validators.OfType<RequiredValidator>().FirstOrDefault()!=null){
                    return false;
                }
                Type t = typeof(T);
                Type u = Nullable.GetUnderlyingType(t);
                return u != null || t == typeof(String);
            }
        }
        private string text;
        public string Text {
            get { return text; }
            set {
                if (text != value) {
                    text = value;
                    OnPropertyChanged(() => Text);
                    Validate(ready => { });
                }
            }
        }

        public object GetValue() {
            return value;
        }

        public void SetValue(object value) {
            ValidationErrors.Clear();
            this.value = (T)value;
            text = ValidatorConverter.Format((T)value);
            OnPropertyChanged(() => Text);
            OnPropertyChanged(() => Value);
        }

        private T value;
        public T Value {
            get { return value; }
            set {
                if (this.value == null || !this.value.Equals(value)) {
                    this.value = value;
                    text = ValidatorConverter.Format(value);
                    OnPropertyChanged(() => Text);
                    OnPropertyChanged(() => Value);
                }
            }
        }

        public ObservableCollection<ValidationError> ValidationErrors { get; private set; }

        private void UpdateErrorLabel() {
            var errorLabel = "";
            foreach (var error in ValidationErrors) {
                errorLabel += error.Message;
            }
            ErrorLabel = errorLabel;
        }

        private string errorLabel;

        public string ErrorLabel {
            get { return errorLabel; }
            set {
                if (errorLabel != value) {
                    errorLabel = value;
                    OnPropertyChanged(() => ErrorLabel);
                }
            }
        }

        private bool hasError;
        public bool HasError {
            get { return hasError; }
            set {
                if (hasError != value) {
                    hasError = value;
                    OnPropertyChanged(() => HasError);
                }
            }
        }

        public void AddValidator(ValidatorBase validator) {
            validators.Add(validator);
        }

        public void Validate(Action<IEditProp> ready) {
            ValidationErrors.Clear();

            var result = ValidatorConverter.ConvertFromText(this);

            // format error? Stop checking the rest of the rules
            if (hasError) {
                ready(this);
                return;
            }

            Value = (T)result;

            // check for synchronous errors (email format checkers etc.)
            foreach (var validator in validators.OfType<ValidatorBaseSync>()) {
                var v = validator.Validate(value);
                if (v != null) {
                    ValidationErrors.Add(v);
                    // stop checking after 1 error
                    ready(this);
                    return;
                }
            }

            var asyncValidators = validators.OfType<ValidatorBaseASync>().ToList();
            int asyncCounter = asyncValidators.Count;
            if (asyncCounter == 0) {
                ready(this);
                return;
            }

            // check for Asynchronous errors (database ID unique etc.)
            foreach (var validator in asyncValidators) {
                validator.Validate(value, error => {
                    if (error != null) {
                        ValidationErrors.Add(error);
                        // stop checking after first call-back error
                        ready(this);
                        return;
                    }
                    asyncCounter--;
                    if (asyncCounter == 0) {
                        ready(this);
                    }
                });
            }
        }
    }
}
