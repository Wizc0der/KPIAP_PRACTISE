using System;

class TrigonometricCalculator
{
    static void Main()
    {
        Console.WriteLine("=== Расчет по двум тригонометрическим формулам ===\n");
        Console.WriteLine("z1 = (cos a + sin a) / (cos a - sin a)");
        Console.WriteLine("z2 = tan(2a) + sec(2a)\n");
        Console.WriteLine(new string('-', 60));

        // Тестовые примеры (в градусах и радианах)
        double[] testAngles = { 0, 10, 15, 30, 45, 60, 90, 120, 135, 180 };

        foreach (double degrees in testAngles)
        {
            double alpha = degrees * Math.PI / 180; // перевод в радианы
            CalculateAndCompare(alpha, degrees);
        }
   
    }

    static void CalculateAndCompare(double alpha, double degrees)
    {
        Console.WriteLine($"\na = {degrees:F2}° ({alpha:F4} рад)");
        Console.WriteLine(new string('-', 40));

        try
        {
            // Формула 1: z₁ = (cos α + sin α) / (cos α - sin α)
            double cosAlpha = Math.Cos(alpha);
            double sinAlpha = Math.Sin(alpha);
            double denominator1 = cosAlpha - sinAlpha;

            if (Math.Abs(denominator1) < 1e-10)
            {
                Console.WriteLine("z₁: не определено (деление на ноль)");
            }
            else
            {
                double z1 = (cosAlpha + sinAlpha) / denominator1;
                Console.WriteLine($"z1 = {z1:F6}");
            }

            // Формула 2: z₂ = tan(2α) + sec(2α)
            double twoAlpha = 2 * alpha;
            double cosTwoAlpha = Math.Cos(twoAlpha);
            double sinTwoAlpha = Math.Sin(twoAlpha);

            if (Math.Abs(cosTwoAlpha) < 1e-10)
            {
                Console.WriteLine("z2: не определено (cos(2α) = 0)");
            }
            else
            {
                double tanTwoAlpha = sinTwoAlpha / cosTwoAlpha;
                double secTwoAlpha = 1 / cosTwoAlpha;
                double z2 = tanTwoAlpha + secTwoAlpha;
                Console.WriteLine($"z2 = {z2:F6}");
            }

            // Проверка совпадения результатов
            Console.WriteLine($"Разница: {Math.Abs(CalculateZ1(alpha) - CalculateZ2(alpha)):E2}");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static double CalculateZ1(double alpha)
    {
        double cosAlpha = Math.Cos(alpha);
        double sinAlpha = Math.Sin(alpha);
        return (cosAlpha + sinAlpha) / (cosAlpha - sinAlpha);
    }

    static double CalculateZ2(double alpha)
    {
        double cosTwoAlpha = Math.Cos(2 * alpha);
        double sinTwoAlpha = Math.Sin(2 * alpha);
        return (sinTwoAlpha / cosTwoAlpha) + (1 / cosTwoAlpha);
    }
}