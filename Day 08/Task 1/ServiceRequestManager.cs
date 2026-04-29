using System.Collections;

class ServiceRequestManager
{
    private Queue requests = new Queue();
    private int nextId = 1;

    public void AddRequest(string customer, string type)
    {
        var request = new ServiceRequest(nextId++, customer, type);
        requests.Enqueue(request);
        Console.WriteLine($"+ Добавлена заявка: {request}");
    }

    public void ProcessNext()
    {
        if (requests.Count == 0)
        {
            Console.WriteLine("Очередь пуста");
            return;
        }
        var request = requests.Dequeue();
        Console.WriteLine($"! Обработана: {request}");
    }

    public void ShowAll()
    {
        Console.WriteLine("\nВсе заявки в очереди:");
        foreach (ServiceRequest r in requests)
            Console.WriteLine($"   {r}");
    }

    public int Count => requests.Count;
}