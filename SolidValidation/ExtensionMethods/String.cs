
namespace SolidValidation.ExtensionMethods {
    public static class StringMethods {
        public static bool IsEmpty(this string s) {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsFilled(this string s) {
            return !string.IsNullOrEmpty(s);
        }
    }
}

