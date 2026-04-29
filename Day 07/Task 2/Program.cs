class Program
{
    static void Main()
    {
        var client = new ApiClient();
        var handler = new RequestHandler();

        try
        {
            handler.HandleRequest(client, "https://api.example.com");
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"\nОшибка: {ex.Message}");
            Console.WriteLine($"Внутреннее: {ex.InnerException?.Message}");
            Console.WriteLine($"Стек:\n{ex.StackTrace}");
        }
    }
}