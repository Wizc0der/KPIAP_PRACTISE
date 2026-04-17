using static System.Console;

Write("Введите вещественное число A (-5 <= A <= 5): ");
double A = double.Parse(ReadLine().Replace('.', ','));

Write("Введите целое число N (1 <= N <= 10): ");
int N = int.Parse(ReadLine());

double sum = 1.0;

double currentPower = 1.0;

for (int i = 1; i <= N; i++)
{
    currentPower *= A;
    sum += currentPower;
}


WriteLine($"Сумма ряда: {sum:F4}");