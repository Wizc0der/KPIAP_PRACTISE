class Program
{
    static void Main()
    {
        var formatter = new TextFormatter(new UpperCaseFormat());
        string text = "привет мир";

        Console.WriteLine("Исходный текст: " + text);
        Console.WriteLine("UPPER: " + formatter.FormatText(text));

        formatter.SetStrategy(new LowerCaseFormat());
        Console.WriteLine("lower: " + formatter.FormatText(text));

        formatter.SetStrategy(new TitleCaseFormat());
        Console.WriteLine("Title: " + formatter.FormatText(text));
    }
}