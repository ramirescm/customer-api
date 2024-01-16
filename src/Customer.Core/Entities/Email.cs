using System.Text.RegularExpressions;

namespace Customer.Core.Entities;

public class Email
{
    public string Address { get; }

    private Email() { }
    
    public Email(string address)
    {
        if (!IsValidEmail(address))
        {
            throw new ArgumentException("Invalid email address", nameof(address));
        }

        Address = address;
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }
}