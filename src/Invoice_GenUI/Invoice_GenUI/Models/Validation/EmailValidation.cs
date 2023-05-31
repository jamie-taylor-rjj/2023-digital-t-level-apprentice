

using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Invoice_GenUI.Models.Validation;

public class EmailValidation : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        Regex regex = new Regex("^[\\w-\\+\\.\\_]+(\\.[\\w-\\+\\.\\_]+)*@[\\w-\\+\\.\\_]+(\\.[\\w\\+\\.\\_]+)*(\\.[A-Za-z]{2,})$");
        string? input = value.ToString();
        if (!regex.IsMatch(input))
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "Email field is empty");
            }
            return new ValidationResult(false, "Enter a valid email Address");
        }
        else
        {
            return ValidationResult.ValidResult;
        }
    }
}
