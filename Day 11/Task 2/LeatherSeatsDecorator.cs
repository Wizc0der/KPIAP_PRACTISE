class LeatherSeatsDecorator : CarDecorator
{
    public LeatherSeatsDecorator(ICar car) : base(car) { }
    public override string GetFeatures() => base.GetFeatures() + ", Leather Seats";
    public override double GetPrice() => base.GetPrice() + 1200;
}
