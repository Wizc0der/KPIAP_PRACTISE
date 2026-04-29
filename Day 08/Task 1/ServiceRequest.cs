class ServiceRequest
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string RequestType { get; set; }

    public ServiceRequest(int id, string name, string type)
    {
        Id = id;
        CustomerName = name;
        RequestType = type;
    }

    public override string ToString() => $"#{Id} | {CustomerName} | {RequestType}";
}
