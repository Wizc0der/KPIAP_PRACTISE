class ApiClient
{
    public void SendRequest(string url)
    {
        Console.WriteLine($"Запрос к {url}...");
        throw new HttpRequestException("Сервер недоступен");
    }
}
