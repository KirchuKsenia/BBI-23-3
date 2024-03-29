using System;
using System.Collections.Generic;

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
    private static int _idCounter = 1;
    private int _id;
    private double _finalScore;

    public ChessPlayer(string name, string surname, int wins, double draws, int losses) : base(name, surname)
    {
        _id = _idCounter++;
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
        for (int i = 0; i < participants.Length - 1; i++)
        {
            for (int j = i + 1; j < participants.Length; j++)
            {
                if (participants[j].FinalScore > participants[i].FinalScore)
                {
                    ChessPlayer temp = participants[j];
                    participants[j] = participants[i];
                    participants[i] = temp;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        ChessPlayer[] participants = new ChessPlayer[]
        {
            new ChessPlayer("Ivan", "Ptichin", 3, 7, 0),
            new ChessPlayer("Sergei", "Zhuravlev", 1, 4, 5),
            new ChessPlayer("Viktor", "Abricosov", 6, 3, 2),
            new ChessPlayer("Alexei", "Volcov", 7, 0, 3),
            new ChessPlayer("Dmitri", "Sobakevich", 10, 0, 0)
        };

        Sort(participants);

        Console.WriteLine("ФИО\t\tИД\tИтоговый балл");

        foreach (ChessPlayer player in participants)
        {
            player.Print();
        }

        Console.ReadKey();
    }
}