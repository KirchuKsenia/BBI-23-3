using System;
using System.Collections.Generic;
using System.Linq;
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

// Класс Task8 отвечает за разбиение текста на строки
class Task8 : Task
{
    // Поле класса Task8 для хранения строк
    public List<string> lines = new List<string>();

    // Конструктор класса Task8
    public Task8(string text) : base(text)
    {
        ParseText(); // Вызов метода ParseText при создании объекта
    }

    // Реализация абстрактного метода ParseText для разбиения текста на строки
    public override (string Text, Dictionary<string, string> tableCodes) ParseText()
    {
        // Разбиение текста на слова по пробелам
        string[] words = Text.Split(' ');
        int currentLineLength = 0;
        string line = "";

        // Перебор каждого слова в тексте
        foreach (string word in words)
        {
            // Если текущая строка слишком длинная, создаем новую строку
            if (currentLineLength + word.Length > 50)
            {
                // Добавляем пробелы до 50 символов
                while (line.Length < 50)
                {
                    line += " ";
                }
                // Добавляем текущую строку в список строк
                lines.Add(line);
                // Начинаем новую строку с текущим словом
                line = word + " ";
                currentLineLength = line.Length;
            }
            else
            {
                // Добавляем текущее слово к текущей строке
                line += word + " ";
                currentLineLength += word.Length + 1;
            }
        }

        // Добавляем последнюю строку, если она не пуста
        if (!string.IsNullOrEmpty(line))
        {
            lines.Add(line);
        }

        // Возвращаем пустые значения, так как не используем таблицу кодов
        return (null, null);
    }
}

// Класс Task9 отвечает за генерацию таблицы кодов для текста
class Task9 : Task
{
    // Конструктор класса Task9
    public Task9(string text) : base(text) { }

    // Реализация абстрактного метода ParseText для генерации таблицы кодов
    public override (string Text, Dictionary<string, string> tableCodes) ParseText()
    {
        // Удаляем пробелы из текста
        string _text = Text.Replace(" ", "");
        // Создаем словарь для хранения таблицы кодов
        Dictionary<string, string> tableCodes = new Dictionary<string, string>();
        // Количество наиболее часто встречающихся пар символов, которые мы хотим заменить
        int countOfPar = 5;
        // Счетчик для отслеживания уже использованных символов замены
        int counter = 0;
        // Множество символов, которые могут использоваться для замены
        HashSet<string> letters = new HashSet<string>
        {
            // Все символы, которые могут встречаться в тексте
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
        // Удаляем из letters символы, которые уже есть в тексте, чтобы не использовать их для замены
        HashSet<string> _tmp = new HashSet<string>(letters);
        foreach (string s in _tmp)
        {
            if (_text.IndexOf(s) != -1)
                letters.Remove(s);
        }
        
        // Считаем встречаемость каждой пары символов в тексте
        Dictionary<string, int> meetings = new Dictionary<string, int>();
        for (int i = 0; i < _text.Length - 1; i++)
        {
            string key = _text.Substring(i, 2);
            if (!meetings.ContainsKey(key))
                meetings[key] = 0;
            meetings[key] = meetings[key] + 1;
        }
        
        // Сортируем пары символов по частоте встречаемости
        var sortedMeetings = meetings.OrderBy(pair => pair.Value).Reverse();
        // Проходим по отсортированным парам символов и заменяем их в тексте на символы из letters
        foreach (var pair in sortedMeetings)
        {
            // Если достигнуто максимальное количество замен, выходим из цикла
            if (counter >= countOfPar)
                break;
            // Заменяем пару символов в тексте на символ из letters
            Text = Text.Replace(pair.Key, letters.ElementAtOrDefault(counter));
            // Добавляем замененную пару и символ замены в таблицу кодов
            tableCodes[pair.Key] = letters.ElementAtOrDefault(counter);
            // Увеличиваем счетчик замененных символов
            counter++;
        }

        // Выводим таблицу кодов в консоль (для отладки)
        foreach (var pair in tableCodes)
        {
            Console.WriteLine($"{pair.Key} -> {pair.Value}");
        }
        // Возвращаем измененный текст и таблицу кодов
        return (Text, tableCodes);
    }
}

// Класс Task10 отвечает за обратное преобразование текста с использованием таблицы кодов
class Task10 : Task
{
    private Dictionary<string, string> tableCodes; // Таблица кодов для обратного преобразования

    // Конструктор класса Task10
    public Task10(string text, Dictionary<string, string> tableCodes) : base(text)
    {
        this.tableCodes = tableCodes; // Инициализация таблицы кодов
    }

    // Реализация абстрактного метода ParseText для обратного преобразования текста
    public override (string Text, Dictionary<string, string> tableCodes) ParseText()
    {
        // Проходим по таблице кодов и заменяем коды в тексте на соответствующие им слова
        foreach (var pair in tableCodes)
        {
            Text = Text.Replace(pair.Value, pair.Key);
        }
        // Возвращаем обратно преобразованный текст и null, так как таблица кодов не нужна после этого
        return (Text, null);
    }
}


// Класс Task12 отвечает за кодирование текста и создание таблицы кодов
class Task12 : Task
{
    private Dictionary<string, string> wordCodes = new Dictionary<string, string>(); // Таблица кодов для слов

    // Конструктор класса Task12
    public Task12(string text) : base(text) { }

    // Реализация абстрактного метода ParseText для кодирования текста и создания таблицы кодов
    public override (string Text, Dictionary<string, string> tableCodes) ParseText()
    {
        string[] words = Text.Split(' ');

        // Генерация и сохранение кодов для слов
        foreach (string word in words)
        {
            // Проверяем, не был ли уже сгенерирован код для данного слова
            if (!wordCodes.ContainsKey(word))
            {
                // Генерируем случайный код
                string code = GenerateCode();
                // Сохраняем соответствие между словом и его кодом в таблице кодов
                wordCodes[word] = code;
            }
        }

        // Замена слов на соответствующие им коды
        string[] codedText = new string[words.Length];
        for (int i = 0; i < words.Length; i++)
        {
            // Проверяем, есть ли код для данного слова в таблице кодов
            if (wordCodes.ContainsKey(words[i]))
            {
                // Если есть, заменяем слово его кодом
                codedText[i] = wordCodes[words[i]];
            }
            else
            {
                // Если слово не имеет кода, оставляем его без изменений
                codedText[i] = words[i];
            }
        }

        // Сборка текста из закодированных слов
        string codedTextString = string.Join(" ", codedText);

        return (codedTextString, wordCodes);
    }

    // Метод для генерации случайного кода
    private string GenerateCode()
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        // Генерируем случайную строку длиной 5 из символов chars
        return new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}

// Класс Task13 отвечает за анализ текста и подсчет процента слов, начинающихся с разных букв алфавита
class Task13 : Task
{
    // Конструктор класса Task13
    public Task13(string text) : base(text) { }

    // Реализация абстрактного метода ParseText для подсчета процента слов, начинающихся с разных букв алфавита
    public override (string Text, Dictionary<string, string> tableCodes) ParseText()
    {
        // Разделение текста на слова, игнорируя знаки пунктуации и пробелы
        string[] words = Text.Split(new char[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        // Словарь для хранения количества слов, начинающихся с каждой буквы алфавита
        Dictionary<char, int> letterCounts = new Dictionary<char, int>();
        int totalWords = words.Length;

        // Подсчет количества слов, начинающихся с каждой буквы алфавита
        foreach (string word in words)
        {
            char firstLetter = word[0];
            if (!letterCounts.ContainsKey(firstLetter))
            {
                letterCounts[firstLetter] = 0;
            }
            letterCounts[firstLetter]++;
        }

        // Словарь для хранения процента слов, начинающихся с каждой буквы алфавита
        Dictionary<char, double> letterPercentages = new Dictionary<char, double>();

        // Вычисление процента слов, начинающихся с каждой буквы алфавита
        foreach (var entry in letterCounts)
        {
            double percentage = (double)entry.Value / totalWords * 100;
            letterPercentages[entry.Key] = percentage;
        }

        // Формирование строки с процентами исходя из словаря letterPercentages
        string result = "";
        foreach (var entry in letterPercentages)
        {
            result += $"{entry.Key}: {entry.Value:F2}%\n"; // Вывод процента с двумя знаками после запятой
        }

        // Возвращаем сформированную строку с процентами и null, так как tableCodes не используется в этом случае
        return (result, null);
    }
}


// Класс Task15 отвечает за анализ текста и подсчет суммы чисел, встречающихся в тексте
class Task15 : Task
{
    // Конструктор класса Task15
    public Task15(string text) : base(text) { }

    // Реализация абстрактного метода ParseText для подсчета суммы чисел в тексте
    public override (string Text, Dictionary<string, string> tableCodes) ParseText()
    {
        // Разделение текста на слова и числа, игнорируя знаки пунктуации и пробелы
        string[] wordsAndNumbers = Text.Split(new char[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        // Переменная для хранения суммы чисел в тексте
        int sum = 0;
        
        // Подсчет суммы чисел в тексте
        foreach (string wordOrNumber in wordsAndNumbers)
        {
            if (int.TryParse(wordOrNumber, out int number)) // Пытаемся преобразовать слово в число
            {
                sum += number; // Если удалось, добавляем число к сумме
            }
        }

        // Возвращаем сумму чисел в тексте и null, так как tableCodes не используется в этом случае
        return ($"Сумма чисел в тексте: {sum}", null);
    }
}

