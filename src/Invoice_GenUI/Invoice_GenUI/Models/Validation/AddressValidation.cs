using System.Globalization;
using System.Windows.Controls;

namespace Invoice_GenUI.Models.Validation;

public class AddressValidation : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string? input = value.ToString();

        if (string.IsNullOrWhiteSpace(input))
        {
            return new ValidationResult(false, "The clients address field must not be empty");
        }
        else
        {
            return ValidationResult.ValidResult;
        }
    }
}
