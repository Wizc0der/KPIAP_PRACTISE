class InvalidEmailException : Exception
{
    public string Email { get; }
    public InvalidEmailException(string email) : base($"Некорректный email: {email}") => Email = email;
}
