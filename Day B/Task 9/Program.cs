using static System.Console;

double A = Math.PI / 4;
double B = Math.PI / 2;
int M = 15;

double H = (B - A) / M;

double x = A;

WriteLine("Табулирование функции F(x) = ctg(x)");
WriteLine($"Интервал: [{A:F4}, {B:F4}]");
WriteLine($"Количество точек: M = {M}");
WriteLine($"Шаг: H = {H:F6}");
WriteLine(new string('-', 30));
WriteLine($"{"№",3} | {"x",10} | {"ctg(x)",12}");
WriteLine(new string('-', 30));

for (int i = 1; i <= M; i++)
{
    double y = Math.Cos(x) / Math.Sin(x);
    WriteLine($"{i,3} | {x,10:F6} | {y,12:F6}");
    x = x + H;
}