class NavigationDecorator : CarDecorator
{
    public NavigationDecorator(ICar car) : base(car) { }
    public override string GetFeatures() => base.GetFeatures() + ", Navigation";
    public override double GetPrice() => base.GetPrice() + 2000;
}