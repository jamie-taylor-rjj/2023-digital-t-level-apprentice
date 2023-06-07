using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Invoice_GenUI.Models.Validation;

public class EmailValidation : ValidationRule // Get rid of class
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        Regex regex = new Regex("^((?!\\.)[\\w-_.]*[^.])(@\\w+)(\\.\\w+(\\.\\w+)?[^.\\W])$"); 
        string? input = value.ToString();
        if (!regex.IsMatch(input))
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "The client email field must not be empty");
            }
            return new ValidationResult(false, "This is an invalid email Address");
        }
        else
        {
            return ValidationResult.ValidResult;
        }
    }
}
