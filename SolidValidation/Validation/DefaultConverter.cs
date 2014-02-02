using System;
using SolidValidation.ExtensionMethods;

namespace SolidValidation.Validation {
    public class DefaultConverter<T> : IConverter {
        public object ConvertFromText(IEditProp editProp) {
            try {
                Type t = typeof(T);
                Type u = Nullable.GetUnderlyingType(t);
                T v = default(T);

                var text = Preformat(editProp.Text);

                if (u != null) {
                    // is nullable
                    if (text.IsEmpty()) {
                        v = default(T);
                    } else {
                        v = (T)Convert.ChangeType(text, u, null);
                    }
                } else {
                    // A string is not a nullable type but is allowed to be null so
                    // we have to check for it here
                    if (text.IsEmpty() && t != typeof(String)) {
                        // not nullable but empty string
                        var formatValidator = new ConversionValidator { ValidationFailedMessage = "Enter a " + editProp.Caption };
                        editProp.ValidationErrors.Add(new ValidationError { Validator = formatValidator });
                        return null;
                    } else {
                        v = (T)Convert.ChangeType(text, t, null);
                    }
                }
                // if you are here, a valid type was entered
                return v;
            } catch {
                // if you are here, a wrong format was typed
                var formatValidator = new ConversionValidator { ValidationFailedMessage = "Enter a valid " + editProp.Caption };
                editProp.ValidationErrors.Add(new ValidationError { Validator = formatValidator });
                return null;
            }
        }

        public virtual string Preformat(string text) {
            return text + "";
        }
        public virtual string Format(object value) {
            return value + "";
        }
    }
}
