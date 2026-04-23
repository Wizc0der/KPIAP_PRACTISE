using static System.Console;
Write("Введите строку: ");
string input = ReadLine();

var freq = new Dictionary<char, int>();
foreach (char c in input)
{
    if (freq.ContainsKey(c)) freq[c]++;
    else freq[c] = 1;
}

int oddCount = 0;
foreach (int count in freq.Values)
    if (count % 2 != 0) oddCount++;

bool isPalindrom = oddCount <= 1;

WriteLine(isPalindrom ? "Да" : "Нет");