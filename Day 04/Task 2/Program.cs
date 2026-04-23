using static System.Console;

static void DigitCountSum(int K, out int C, out int S)
{
    C = 0;
    S = 0;
    while (K > 0)
    {
        S += K % 10;
        C++;
        K /= 10;
    }
}

int[] nums = { 12345, 708, 99, 4000, 6 };

foreach (int n in nums)
{
    DigitCountSum(n, out int count, out int sum);
    WriteLine($"Число: {n,5} | Цифр: {count} | Сумма: {sum}");
}