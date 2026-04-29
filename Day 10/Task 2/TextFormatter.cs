interface ITextFormatStrategy
{
    string Format(string text);
}

class UpperCaseFormat : ITextFormatStrategy
{
    public string Format(string text) => text.ToUpper();
}

class LowerCaseFormat : ITextFormatStrategy
{
    public string Format(string text) => text.ToLower();
}

class TitleCaseFormat : ITextFormatStrategy
{
    public string Format(string text)
    {
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
    }
}

class TextFormatter
{
    private ITextFormatStrategy strategy;

    public TextFormatter(ITextFormatStrategy strategy)
    {
        this.strategy = strategy;
    }

    public void SetStrategy(ITextFormatStrategy strategy)
    {
        this.strategy = strategy;
    }

    public string FormatText(string text)
    {
        return strategy.Format(text);
    }
}