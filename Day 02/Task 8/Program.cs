using static System.Console;

class Program
{
    static void Main()
    {
        Write("Введите текст: ");
        string input = ReadLine();

        Write("Введите ключ (сдвиг): ");
        int key = int.Parse(ReadLine());

        string encrypted = CaesarCipher(input, key);
        WriteLine($"\nЗашифровано: {encrypted}");

        string decrypted = CaesarCipher(encrypted, -key);
        WriteLine($"Расшифровано: {decrypted}");
    }

    static string CaesarCipher(string text, int shift)
    {
        char[] buffer = text.ToCharArray();

        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = buffer[i];

            if (char.IsLetter(letter))
            {
                char offset = char.IsUpper(letter) ? 'A' : 'a';
                letter = (char)(offset + (letter - offset + shift % 26 + 26) % 26);
            }

            buffer[i] = letter;
        }

        return new string(buffer);
    }
}