class RequestHandler
{
    public void HandleRequest(ApiClient client, string url)
    {
        try
        {
            client.SendRequest(url);
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException("Ошибка при вызове API", ex);
        }
    }
}
