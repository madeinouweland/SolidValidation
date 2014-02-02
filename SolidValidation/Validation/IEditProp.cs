using System;
using System.Collections.ObjectModel;

namespace SolidValidation.Validation {
    public interface IEditProp {
        void AddValidator(ValidatorBase validator);
        bool HasError { get; }
        void Validate(Action<IEditProp> ready);
        string Text { get; set; }
        string Caption { get; set; }
        ObservableCollection<ValidationError> ValidationErrors { get; }
        void SetValue(object value); // needed to set with reflection
        object GetValue(); // needed to get with reflection
        IConverter ValidatorConverter { get; set; }
        bool IsNullable { get; }
    }
}
