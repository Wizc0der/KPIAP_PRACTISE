using static System.Console;

static string ReverseString(string s)
{
    if (s.Length <= 1) return s;
    return ReverseString(s.Substring(1)) + s[0];
}

Write("Введите строку: ");
string input = ReadLine();

string reversed = ReverseString(input);
WriteLine($"Реверс: {reversed}");