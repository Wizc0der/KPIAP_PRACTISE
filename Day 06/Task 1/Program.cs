using static System.Console;

delegate decimal DiscountCalculator(decimal price);
class StudentDiscount
{
    public decimal ApplyDiscount(decimal price)
    {
        decimal discount = price * 0.15m;
        WriteLine($"Студенческая скидка: -{discount:C}");
        return price - discount;
    }
}

class SeniorDiscount
{
    public decimal ApplyDiscount(decimal price)
    {
        decimal discount = price * 0.20m;
        WriteLine($"Скидка пенсионерам: -{discount:C}");
        return price - discount;
    }
}

class Program
{
    static void Main()
    {
 
        StudentDiscount student = new StudentDiscount();
        SeniorDiscount senior = new SeniorDiscount();

        DiscountCalculator studentCalc = student.ApplyDiscount;
        DiscountCalculator seniorCalc = senior.ApplyDiscount;

        WriteLine("=== РАСЧЁТ СКИДОК ===\n");

        decimal originalPrice = 1000m;
        WriteLine($"Исходная цена: {originalPrice:C}\n");

        WriteLine("--- Студент ---");
        decimal studentPrice = studentCalc(originalPrice);
        WriteLine($"Итоговая цена: {studentPrice:C}\n");

        WriteLine("--- Пенсионер ---");
        decimal seniorPrice = seniorCalc(originalPrice);
        WriteLine($"Итоговая цена: {seniorPrice:C}\n");

        WriteLine("=== МУЛЬТИСОСТАВНОЙ ДЕЛЕГАТ ===");
        DiscountCalculator combined = studentCalc + seniorCalc;
        WriteLine("Применяем обе скидки последовательно:");
        decimal combinedPrice = combined(originalPrice);
        WriteLine($"Итоговая цена: {combinedPrice:C}\n");

        WriteLine("=== ПРОВЕРКА ТИПА ===");
        WriteLine($"Делегат studentCalc: {studentCalc.Method.Name}");
        WriteLine($"Делегат seniorCalc: {seniorCalc.Method.Name}");
    }
}