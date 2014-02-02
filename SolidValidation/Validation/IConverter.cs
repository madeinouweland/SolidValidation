
namespace SolidValidation.Validation {
    public interface IConverter {
        object ConvertFromText(IEditProp editProp);
        string Format(object value);
    }
}
