interface ICar
{
    string GetFeatures();
    double GetPrice();
}

class BasicCar : ICar
{
    public string GetFeatures() => "Basic Car";
    public double GetPrice() => 20000;
}