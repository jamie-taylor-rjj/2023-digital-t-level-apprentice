

using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Invoice_GenUI.Models.Validation;

public class AddressValidation : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        Regex regex = new Regex("(^\\s?[^qvx][^ijz]?[0-9]{1,2}[A,B,C,D,E,F,G,H,J,K,S,T,U,W]?\\s?[0-9][^CIKMOV]{2}\\s?)");
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
