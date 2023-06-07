

using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Invoice_GenUI.Models.Validation;

public class AddressValidation : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        Regex regex = new Regex("^[A-Za-z0-9]{1,100}");
        string? input = value.ToString();
        if (!regex.IsMatch(input))
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "Address field is empty");
            }
            return new ValidationResult(false, "Enter a valid UK address");
        }
        else
        {
            return ValidationResult.ValidResult;
        }
    }
}
