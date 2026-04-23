using static System.Console;
using System.Linq;

abstract class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal TotalValue => Price * Quantity;
    public override string ToString() => $"{Name}: {Price}$ x {Quantity} = {TotalValue}$";
}

sealed class Electronics : Product { public Electronics(string n, decimal p, int q) => (Name, Price, Quantity) = (n, p, q); }
sealed class Clothing : Product { public Clothing(string n, decimal p, int q) => (Name, Price, Quantity) = (n, p, q); }

class Warehouse
{
    public Product[] Products { get; set; } = Array.Empty<Product>();

    public decimal GetTotalStockValue() => Products.Sum(p => p.TotalValue);
    public Product FindMostExpensiveProduct() => Products.OrderByDescending(p => p.Price).FirstOrDefault();
}

class Program
{
    static void Main()
    {
        var warehouse = new Warehouse
        {
            Products = new Product[]
            {
                new Electronics("Ноутбук", 1200, 5),
                new Clothing("Футболка", 25, 50),
                new Electronics("Телефон", 800, 10),
                new Clothing("Джинсы", 60, 30)
            }
        };

       WriteLine("=== ТОВАРЫ НА СКЛАДЕ ===");
        foreach (var p in warehouse.Products) WriteLine(p);

        WriteLine($"\nОбщая стоимость: {warehouse.GetTotalStockValue()}$");

        var expensive = warehouse.FindMostExpensiveProduct();
        WriteLine($"Самый дорогой: {expensive?.Name} ({expensive?.Price}$)");
    }
}