using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

abstract class Task
{
    protected string Text;
    public Task(string Text)
    {
        this.Text = Text;
    }
    public abstract (string Text, Dictionary<string, string> tableCodes) ParseText();
}

class Program
{
    private static string text1 = "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ данных показал, что основной участник разрушения лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений. ";

    public static void Main(string[] args)
    {
        Task8 task8 = new Task8(text1);
        foreach (var line in task8.lines)
        {
            Console.WriteLine(line);
        }
        Task9 task9 = new Task9(text1);

        var parsedText = task9.ParseText();
        string codeText = parsedText.Item1;
        Dictionary<string, string> codes = parsedText.Item2;
        Console.WriteLine(codeText);

        Task10 task10 = new Task10(codeText, codes);
        Console.WriteLine(task10.ParseText().Item1);

        Task12 task12 = new Task12(text1);
        var parsedText12 = task12.ParseText();
        string codedText = parsedText12.Item1;
        Dictionary<string, string> wordCodes = parsedText12.Item2;
        Console.WriteLine(codedText);
        Console.WriteLine("Decoded Text:");
        foreach (var pair in wordCodes)
        {
            codedText = codedText.Replace(pair.Value, pair.Key);
        }
        Console.WriteLine(codedText);

        Task13 task13 = new Task13(text1);
        var parsedText13 = task13.ParseText();
        Console.WriteLine("Процент слов, начинающихся на разные буквы:");
        Console.WriteLine(parsedText13.Item1);

        Task15 task15 = new Task15(text1);
        var parsedText15 = task15.ParseText();
        Console.WriteLine(parsedText15.Text);
    }
}


class Task8 : Task
{
    public List<string> lines = new List<string>();

    public Task8(string text) : base(text)
    {
        ParseText();
    }

    public override (string Text, Dictionary<string, string> tableCodes) ParseText()
    {
        string[] words = Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string line = ""; 
        int lineLength = 0; 

        foreach (string word in words)
        {
            if (lineLength + word.Length > 50)
            {
                string[] spacedWords = line.Trim().Split(' ');
                int vseProbeli = 50 - line.Replace(" ", "").Length;
                int ProbeliSlov = vseProbeli / (spacedWords.Length - 1);
                int dopProbeli = vseProbeli % (spacedWords.Length - 1);
                line = "";
                for (int i = 0; i < spacedWords.Length - 1; i++)
                {
                    line += spacedWords[i] + new string(' ', ProbeliSlov + (i < dopProbeli ? 1 : 0));
                }
                line += spacedWords[spacedWords.Length - 1];
                lines.Add(line);
                line = "";
                lineLength = 0;
            }
            line += word + " ";
            lineLength += word.Length + 1; 
        }
        if (!string.IsNullOrEmpty(line))
        {
            lines.Add(line.Trim());
        }
        return (null, null);
    }
}


class Task9 : Task
{

    public Task9(string text) : base(text) { }

    public override (string Text, Dictionary<string, string> tableCodes) ParseText()
    {
        string _text = Text.Replace(" ", "");

        Dictionary<string, string> tableCodes = new Dictionary<string, string>();
        int countOfPar = 5;
        int counter = 0;
        HashSet<string> letters = new HashSet<string>
        {
            "`", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=",
            "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "[", "]",
            "A", "S", "D", "F", "G", "H", "J", "K", "L", ";", "'",
            "Z", "X", "C", "V", "B", "N", "M", ",", ".", "/",
            "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "+",
            "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "{", "}",
            "a", "s", "d", "f", "g", "h", "j", "k", "l", ":", "\"",
            "z", "x", "c", "v", "b", "n", "m", "<", ">", "?",
            "ё", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=",
            "й", "ц", "у", "к", "е", "н", "г", "ш", "щ", "з", "х", "ъ",
            "ф", "ы", "в", "а", "п", "р", "о", "л", "д", "ж", "э",
            "я", "ч", "с", "м", "и", "т", "ь", "б", "ю", ".",
            "!", "\"", "№", ";", "%", ":", "?", "*", "(", ")", "_", "+",
            "Ё", "!", "\"", "№", ";", "%", ":", "?", "*", "(", ")", "_", "+",
            "Й", "Ц", "У", "К", "Е", "Н", "Г", "Ш", "Щ", "З", "Х", "Ъ",
            "Ф", "Ы", "В", "А", "П", "Р", "О", "Л", "Д", "Ж", "Э",
            "Я", "Ч", "С", "М", "И", "Т", "Ь", "Б", "Ю", ","
        };

        HashSet<string> _tmp = new HashSet<string>(letters);
        foreach (string s in _tmp)
        {
            if (_text.IndexOf(s) != -1)
                letters.Remove(s);
        }

        Dictionary<string, int> meetings = new Dictionary<string, int>();
        for (int i = 0; i < _text.Length - 1; i++)
        {
            string key = _text.Substring(i, 2);
            if (!meetings.ContainsKey(key))
                meetings[key] = 0;
            meetings[key] = meetings[key] + 1;
        }

        var sortedMeetings = meetings.OrderBy(pair => pair.Value).Reverse();
        foreach (var pair in sortedMeetings)
        {
            if (counter >= countOfPar)
                break;
            Text = Text.Replace(pair.Key, letters.ElementAtOrDefault(counter));
            tableCodes[pair.Key] = letters.ElementAtOrDefault(counter);
            counter++;
        }

        foreach (var pair in tableCodes)
        {
            Console.WriteLine($"{pair.Key} -> {pair.Value}");
        }
        return (Text, tableCodes);
    }
}


class Task10 : Task
{
    private Dictionary<string, string> tableCodes;

    public Task10(string text, Dictionary<string, string> tableCodes) : base(text)
    {
        this.tableCodes = tableCodes;
    }

    public override (string Text, Dictionary<string, string> tableCodes) ParseText()
    {
        foreach (var pair in tableCodes)
        {
            Text = Text.Replace(pair.Value, pair.Key);
        }
        return (Text, null);
    }
}



class Task12 : Task
{
    private Dictionary<string, string> wordCodes = new Dictionary<string, string>();
    public Task12(string text) : base(text) { }

    public override (string Text, Dictionary<string, string> tableCodes) ParseText()
    {
        string[] words = Text.Split(' ');
        foreach (string word in words)
        {
            if (!wordCodes.ContainsKey(word))
            {
                string code = GenerateCode();
                wordCodes[word] = code;
            }
        }

        string[] codedText = new string[words.Length];
        for (int i = 0; i < words.Length; i++)
        {
            if (wordCodes.ContainsKey(words[i]))
            {
                codedText[i] = wordCodes[words[i]];
            }
            else
            {
                codedText[i] = words[i];
            }
        }

        string codedTextString = string.Join(" ", codedText);

        return (codedTextString, wordCodes);
    }

    private string GenerateCode()
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}


class Task13 : Task
{
    public Task13(string text) : base(text) { }

    public override (string Text, Dictionary<string, string> tableCodes) ParseText()
    {
        string[] words = Text.Split(new char[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        Dictionary<char, int> letterCounts = new Dictionary<char, int>();
        int totalWords = words.Length;
        foreach (string word in words)
        {
            char firstLetter = word[0];
            if (!letterCounts.ContainsKey(firstLetter))
            {
                letterCounts[firstLetter] = 0;
            }
            letterCounts[firstLetter]++;
        }

        Dictionary<char, double> letterPercentages = new Dictionary<char, double>();

        foreach (var entry in letterCounts)
        {
            double percentage = (double)entry.Value / totalWords * 100;
            letterPercentages[entry.Key] = percentage;
        }
        string result = "";
        foreach (var entry in letterPercentages)
        {
            result += $"{entry.Key}: {entry.Value:F2}%\n";
        }
        return (result, null);
    }
}



class Task15 : Task
{
    public Task15(string text) : base(text) { }
    public override (string Text, Dictionary<string, string> tableCodes) ParseText()
    {
        string[] wordsAndNumbers = Text.Split(new char[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        int sum = 0;
        foreach (string wordOrNumber in wordsAndNumbers)
        {
            if (int.TryParse(wordOrNumber, out int number))
            {
                sum += number;
            }
        }
        return ($"Сумма чисел в тексте: {sum}", null);
    }
}


