using System;

namespace SolidValidation.Validation {
    public class CurrencyConverter : DefaultConverter<decimal> {
        public override string Preformat(string text) {
            // strip currency symbol
            text = text.Replace("€", "");
            
            text = text.Replace(".", ",");

            // find last separator and replace with ~
            int lastDecimalCharPos = text.LastIndexOf(",");
            if (lastDecimalCharPos > 0) {
                text = text.Remove(lastDecimalCharPos, 1).Insert(lastDecimalCharPos, "~");

                // strip all thousand separators
                text = text.Replace(",", "");

                text = text.Replace("~", ",");
            }
            return text;
        }

        public override string Format(object value) {
            string text = String.Format("{0:##,##0.00 €}", value);
            return text;
        }
    }
}
