using System;

public class People
{
    private string _name;
    private string _surname;

    public People(string name, string surname)
    {
        _name = name;
        _surname = surname;
    }

    public string Name => _name;
    public string Surname => _surname;
}

public class ChessPlayer : People
{
    private int _id;
    private double _finalScore;

    public ChessPlayer(string name, string surname, int id, int wins, double draws, int losses) : base(name, surname)
    {
        _id = id;
        _finalScore = wins + draws / 2;
    }

    public int Id => _id;
    public double FinalScore => _finalScore;

    public void Print()
    {
        Console.WriteLine($"{Name}\t{Surname}\t{_id}\t{_finalScore}");
    }
}

internal class Program
{
    static void Sort(ChessPlayer[] participants)
    {
        for (int i = 1; i < participants.Length; i++)
        {
            for (int j = 1; j < participants.Length; j++)
            {
                if (participants[j].FinalScore > participants[j - 1].FinalScore)
                {
                    ChessPlayer temp = participants[j];
                    participants[j] = participants[j - 1];
                    participants[j - 1] = temp;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        ChessPlayer[] participants = new ChessPlayer[5]
        {
            new ChessPlayer("Ivan", "Ptichin", 1, 3, 7, 0),
            new ChessPlayer("Sergei", "Zhuravlev", 2, 1, 4, 5),
            new ChessPlayer("Viktor", "Abricosov", 3, 6, 2, 2),
            new ChessPlayer("Alexei", "Volcov", 4, 7, 0, 3),
            new ChessPlayer("Dmitri", "Sobakevich", 5, 10, 0, 0)
        };

        Sort(participants);

        Console.WriteLine("ФИО\t\tИД\tИтоговый балл");
        for (int i = 0; i < participants.Length; i++)
        {
            participants[i].Print();
        }

        Console.ReadKey();
    }
}
