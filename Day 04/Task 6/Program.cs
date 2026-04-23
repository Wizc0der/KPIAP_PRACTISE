using static System.Console;

class Program
{
    static void PrevDate(ref int D, ref int M, ref int Y)
    {
        int[] days = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        if (--D == 0)
        {
            if (--M == 0) { M = 12; Y--; }
            D = M == 2 && (Y % 4 == 0 && Y % 100 != 0 || Y % 400 == 0) ? 29 : days[M - 1];
        }
    }

    static void Main()
    {
        (int d, int m, int y)[] dates = { (15, 5, 2023), (1, 3, 2024), (1, 1, 2025) };

        foreach (var (d, m, y) in dates)
        {
            int D = d, M = m, Y = y;
            PrevDate(ref D, ref M, ref Y);
            WriteLine($"{d:D2}.{m:D2}.{y} -> {D:D2}.{M:D2}.{Y}");
        }
    }
}
