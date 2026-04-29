using System.Text.RegularExpressions;

class EmailValidator
{
    public void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new InvalidEmailException(email);
        Console.WriteLine($"Email валиден: {email}");
    }
}
