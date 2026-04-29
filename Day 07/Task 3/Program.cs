class Program
{
    static void Main()
    {
        var validator = new EmailValidator();
        string[] emails = { "user@example.com", "invalid", "test@domain" };

        foreach (var email in emails)
        {
            try
            {
                validator.ValidateEmail(email);
            }
            catch (InvalidEmailException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}