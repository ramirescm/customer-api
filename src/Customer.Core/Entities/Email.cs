using System.Text.RegularExpressions;

namespace Customer.Core.Entities;

public class Email
{
    private Email()
    {
    }

    public Email(string address)
    {
        if (!IsValidEmail(address)) throw new ArgumentException("Invalid email address", nameof(address));

        Address = address;
    }

    public string Address { get; }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        var regex = new Regex(pattern);
        return regex.IsMatch(email);
    }
}