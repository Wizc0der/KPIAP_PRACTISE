class Battery
{
    public int Capacity { get; set; }
    public Battery(int capacity) => Capacity = capacity;
    public override string ToString() => $"{Capacity} мАч";
}