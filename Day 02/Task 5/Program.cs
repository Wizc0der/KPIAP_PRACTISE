using static System.Console;
class Program
{
    static void Main()
    {
        double[][] matrix = new double[3][];
        matrix[0] = new double[] { 2, 4, 6 };
        matrix[1] = new double[] { 1, 3 };
        matrix[2] = new double[] { 5, 5, 5, 5 };

        WriteLine("Исходный массив:");
        Print(matrix);

        // 2. Вычисление средних арифметических по строкам
        double[] averages = new double[matrix.Length];
        for (int i = 0; i < matrix.Length; i++)
        {
            double sum = 0;
            foreach (double val in matrix[i]) sum += val;
            averages[i] = matrix[i].Length > 0 ? sum / matrix[i].Length : 0;
        }

        // 3. Добавление новой строки в ступенчатый массив
        Array.Resize(ref matrix, matrix.Length + 1);
        matrix[matrix.Length - 1] = averages;

        WriteLine("\nПосле добавления строки средних:");
        Print(matrix);
    }

    // Вспомогательный метод для вывода
    static void Print(double[][] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Write($"[{i}] ");
            foreach (double v in arr[i]) Write($"{v:F2} ");
            WriteLine();
        }
    }
}