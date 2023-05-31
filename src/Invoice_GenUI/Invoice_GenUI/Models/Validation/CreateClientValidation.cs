

using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Invoice_GenUI.Models.Validation;

public class UsernameValidation : ValidationRule
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
public class ContactValidation : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        Regex regex = new Regex("^(?:(?:0(?:0|11)[\\s-]?|\\+)44[\\s-]?(?:0[\\s-]?)?|0)(?:\\d{2}[\\s-]?\\d{4}[\\s-]?\\d{4}|\\d{3}[\\s-]?\\d{3}[\\s-]?\\d{3,4}|\\d{4}[\\s-]?(?:\\d{5}|\\d{3}[\\s-]?\\d{3})|\\d{5}[\\s-]?\\d{4,5}|8(?:00[\\s-]?11[\\s-]?11|45[\\s-]?46[\\s-]?4\\d))$");
        string? input = value.ToString();
        if (!regex.IsMatch(input))
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "Contact field is empty");
            }
            return new ValidationResult(false, "Enter a valid UK contact number");
        }
        else
        {
            return ValidationResult.ValidResult;
        }
    }
}

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
