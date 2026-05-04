abstract class CarDecorator : ICar
{
    protected ICar car;
    protected CarDecorator(ICar car) => this.car = car;
    public virtual string GetFeatures() => car.GetFeatures();
    public virtual double GetPrice() => car.GetPrice();
}