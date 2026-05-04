class SunroofDecorator : CarDecorator
{
    public SunroofDecorator(ICar car) : base(car) { }
    public override string GetFeatures() => base.GetFeatures() + ", Sunroof";
    public override double GetPrice() => base.GetPrice() + 1500;
}