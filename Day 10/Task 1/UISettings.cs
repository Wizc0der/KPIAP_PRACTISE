class UISettings
{
    private static UISettings instance;
    private static readonly object lockObj = new object();

    private string theme;

    private UISettings()
    {
        theme = "Светлая";
    }

    public static UISettings GetInstance()
    {
        if (instance == null)
        {
            lock (lockObj)
            {
                if (instance == null)
                    instance = new UISettings();
            }
        }
        return instance;
    }

    public void SetTheme(string theme)
    {
        this.theme = theme;
        Console.WriteLine($"Тема установлена: {theme}");
    }

    public string GetTheme()
    {
        return theme;
    }
}
