using static System.Console;
class Program
{
    static int Calculate(int a, int b) => a + b;
    static double Calculate(double a, double b) => a + b;
    static void Main()
    {
        WriteLine($"Calculate(3, 2) = {Calculate(3, 2)}");
        WriteLine($"Calculate(3.5, 2.5) = {Calculate(3.5, 2.5)}");
    }
}