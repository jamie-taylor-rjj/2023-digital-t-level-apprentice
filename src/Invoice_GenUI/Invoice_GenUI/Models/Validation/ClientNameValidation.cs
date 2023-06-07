

using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Invoice_GenUI.Models.Validation;

public class ClientNameValidation : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        Regex regex = new Regex("^[A-Z]{1,100}");
        string? input = value.ToString();
        if (!regex.IsMatch(input))
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "Name field is empty");
            }
            return new ValidationResult(false, "Enter a valid name");
        }
        else
        {
            return ValidationResult.ValidResult;
        }
    }
}
