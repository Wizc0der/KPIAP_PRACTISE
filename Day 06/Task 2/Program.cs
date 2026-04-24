using static System.Console;

delegate void OrderProcessor(int orderId);

class Program
{
    static void ProcessOrder(int orderId, OrderProcessor processor)
    {
        processor(orderId);
    }

    static void ApproveOrder(int orderId)
    {
        WriteLine($"Заказ {orderId} одобрен");
    }

    static void CancelOrder(int orderId)
    {
        WriteLine($"Заказ {orderId} отменён");
    }

    static void Main()
    {
        ProcessOrder(101, ApproveOrder);
        ProcessOrder(102, CancelOrder);
    }
}