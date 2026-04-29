class MyLinkedList<T>
{
    public class Node
    {
        public T Data;
        public Node Prev, Next;
        public Node(T data) => Data = data;
    }

    private Node head, tail;
    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        var node = new Node(item);
        if (head == null) head = tail = node;
        else { node.Next = head; head.Prev = node; head = node; }
        Count++;
    }

    public void AddLast(T item)
    {
        var node = new Node(item);
        if (tail == null) head = tail = node;
        else { tail.Next = node; node.Prev = tail; tail = node; }
        Count++;
    }

    public bool Remove(T item)
    {
        var current = head;
        while (current != null)
        {
            if (current.Data.Equals(item))
            {
                if (current.Prev != null) current.Prev.Next = current.Next;
                else head = current.Next;
                if (current.Next != null) current.Next.Prev = current.Prev;
                else tail = current.Prev;
                Count--;
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public Node Find(T item)
    {
        var current = head;
        while (current != null)
        {
            if (current.Data.Equals(item)) return current;
            current = current.Next;
        }
        return null;
    }

    public void ShowAll()
    {
        var current = head;
        while (current != null)
        {
            Console.Write($"{current.Data} ");
            current = current.Next;
        }
        Console.WriteLine();
    }
}