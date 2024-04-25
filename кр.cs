using System.IO;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics.Tracing;
using System.Security.AccessControl;
using System.Globalization;

abstract class Task
{
    protected string text;
    // для десериализации обязательно прописывайте свойства для всех полей
    public string Text
    {
        get => text;
        protected set => text = value;
    }
    public Task(string text)
    {
        this.text = text;
    }
}
class Task1 : Task
{
    [JsonConstructor]
    public Task1(string text) : base(text) { }
    public override string ToString()
    {
        return LongestSentence(text);
    }
    private string LongestSentence(string text)
    {
        string[] sentences = text.Split(".!?".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        int max = 0;
        string longestSentence = "";
        foreach (string sentence in sentences)
        {
            if (sentence.Length > max)
            {
                max = sentence.Length;
                longestSentence = sentence;
            }
        }
        return longestSentence;
    }
}
class Task2 : Task
{
    private int amount;

    public Task2(string text) : base(text)
    {
    }

    public override string ToString()
    {
        return PalWords(text).ToString();
    }
    private int PalWords(string text)
    {
        string[] wordsInText = text.Split(" .,()-\"?:;!".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < wordsInText.Length; i++)
        {
            int middleOfAWord = wordsInText[i].Length / 2;
            for (int j = 0; j < middleOfAWord; j++)
            {
                if (!(wordsInText[i][j] == wordsInText[i][wordsInText[i].Length - 1 - j]))
                {
                    amount--; break;

                }

            }
            amount++;
        }
        return amount;
    }
}
class Task3 : Task
{
    public Task3(string text) : base(text) { }
    public override string ToString()
    {
        return MiddleWordInText(text);
    }
    private string MiddleWordInText(string text)
    {
        string word = "";
        var wordsIntext = text.Split(" .,()-\"?:;!".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        word = wordsIntext[wordsIntext.Length / 2];
        return word;
    }
}
class Task4 : Task
{
    private int amount;
    public Task4(string text) : base(text) { }
    public override string ToString()
    {
        return AmountOfUniqueWods(text).ToString();
    }
    private int AmountOfUniqueWods(string text)
    {
        var wordsIntext = text.Split(" .,()-\"?:;!".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, int> words = new Dictionary<string, int>();
        for (int i = 0; i < wordsIntext.Length; i++)
        {
            words[wordsIntext[i]] = 1;
        }
        for (int i = 0; i < wordsIntext.Length; i++)
        {
            if (words.ContainsKey(wordsIntext[i]) && !char.IsDigit(wordsIntext[i][0]))
            {
                words[wordsIntext[i]]++;
            }
        }
        foreach (var w in words)
        {
            if (w.Value == 1)
            {
                amount++;
            }
        }
        return amount;
    }
}
class Task5 : Task
{
    private int amount;
    public Task5(string text) : base(text)
    {

    }
    public override string ToString()
    {
        string words = "";
        foreach (var word in AllWordsWithChar(text))
        {
            words += word + " ";
        }

        return words;
    }
   
    private char FindMostOftenLetter(string text)
    {
        Dictionary<char, int> letters = new Dictionary<char, int>();
        foreach (char c in text.ToLower()) // Преобразуем в нижний регистр для унификации
        {
            if (char.IsLetter(c)) // Учитываем только буквы
            {
                if (letters.ContainsKey(c))
                {
                    letters[c]++;
                }

                else
                {
                    letters[c] = 1;
                }

            }
        }
        char mostCommonLetter = letters.OrderByDescending(x => x.Value).First().Key;

        return mostCommonLetter;
    }
    private string[] AllWordsWithChar(string text)
    {
        char charT = FindMostOftenLetter(text);
        List<string> wordsWithChar = new List<string>();
        var wordsIntext = text.Split(" .,()-\"?:;!".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (var word in wordsIntext)
        {
            if (word.Contains(charT))
            {
                wordsWithChar.Add(word);
            }
        }
        return wordsWithChar.ToArray();
    }
}


class JsonIO
{
    #region для тех, кто хочет максимум, используйте обобщение:
    public static void Write<T>(T obj, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, obj); // преобразовать данные(куда, что)
        }
        // или так:
        //FileStream fs2 = new FileStream(filePath, FileMode.OpenOrCreate);
        //JsonSerializer.Serialize(fs2, obj); // преобразовать данные(куда, что)
        //fs2.Close();
    }
    public static T Read<T>(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<T>(fs); // преобразовать данные<во что>(откуда)
        }
        return default(T);
    }
    #endregion 
}
class Program
{
    static void Main()
    {
        Task[] tasks = {
            new Task1(""),
            new Task2("оно опо пенис дед жопа потоп казак гегегег жежежеж пнеси жопа жопа жопа жопа"),
            new Task3("ого крта типа ноно жопа бобо гого бабо"),
            new Task4("ты я он она она он он он жопа 1 12"),
            new Task5("оно опо пенис дед жопа потоп казак гегегег жежежеж пнеси")

        };
        Console.WriteLine(tasks[1]);
        string path = @"C:\Users\user\Documents"; // исходную папку ищем в компьютере
        string folderName = "Anwser"; // если нужно создать подпапку
        path = Path.Combine(path, folderName);
        if (!Directory.Exists(path))    // создать отсутствующую подпапку
        {
            Directory.CreateDirectory(path);
        }
        //string fileName1 = "cw_2_task_1.json"; // имена файлов
        //string fileName2 = "cw_2_task_2.json";

        //fileName1 = Path.Combine(path, fileName1);
        //fileName2 = Path.Combine(path, fileName2);

        //#region для тех, кто не делает 4е задание с сериализацией, создаете пустые файлы
        //if (!File.Exists(fileName1))
        //{
        //    var filec = File.Create(fileName1);
        //    filec.Close();
        //}

        //#endregion

        //#region 4е задание на JSON сериализацию (5 баллов)
        //if (!File.Exists(fileName2)) // создаем файл, если его нет
        //{
        //    JsonIO.Write<Task1>(tasks[0] as Task1, fileName1);  // можно так приводить к нужному типу
        //    JsonIO.Write<Task2>((Task2)tasks[1], fileName2);    // а можно так
        //}
        //else // читаем файл (если меняли логику заданий, то удалите старые файлы!)
        //{
        //    var t1 = JsonIO.Read<Task1>(fileName1);
        //    var t2 = JsonIO.Read<Task2>(fileName2);
        //    Console.WriteLine(t1);
        //    Console.WriteLine(t2);
        //}
        //#endregion
    }
}
