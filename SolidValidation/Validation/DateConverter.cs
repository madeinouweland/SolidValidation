using System;

namespace SolidValidation.Validation {
    // CHRIS: ik moet hier de DateTime? als type opgeven. Dat wil ik eigenlijk niet
    // want nu moet ik voor een nullable date en een gewone date 2 converters maken
    public class DateConverter : DefaultConverter<DateTime?> {
        public override string Format(object value) {
            DateTime dt = DateTime.Parse(value + "");
            return dt.ToString("dd-MM-yyyy");
        }
    }
}
