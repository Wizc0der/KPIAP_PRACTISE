class LinkedListManager<T>
{
    private MyLinkedList<T> list = new MyLinkedList<T>();

    public void Add(T item) => list.AddLast(item);

    public void Remove(T item) => Console.WriteLine(list.Remove(item) != null ? $"Найдено: {item}" : $"Не найдено: {item}");
    public void Find(T item) => Console.WriteLine(list.Find(item) != null ? $"Найдено: {item}" : $"Не найдено: {item}");

    public void ShowAll() => list.ShowAll();
    public int Count => list.Count;
}