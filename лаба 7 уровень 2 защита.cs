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

    protected string Name => _name;
    protected string Surname => _surname;
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

    

    public void Print()
    {
        Console.WriteLine($"{Name}\t{Surname}\t{_id}\t{_finalScore}");
    }

    public static void Merge(ChessPlayer[] participants, int left, int mid, int right)
    {
        int n1 = mid - left + 1;

        int n2 = right - mid;

        ChessPlayer[] L = new ChessPlayer[n1];
        ChessPlayer[] R = new ChessPlayer[n2];

        for (int h = 0; h < n1; h++)
        {
            L[h] = participants[left + h];
        }

        for (int p = 0; p < n2; p++)
        {
            R[p] = participants[mid + 1 + p];
        }

        int k = left;
        int i = 0, j = 0;

        while (i < n1 && j < n2)
        {
            if (L[i]._finalScore >= R[j]._finalScore)
            {
                participants[k] = L[i];
                i++;
            }
            else
            {
                participants[k] = R[j];
                j++;
            }
            k++;
        }

        while (i < n1)
        {
            participants[k] = L[i];
            i++;
            k++;
        }

        while (j < n2)
        {
            participants[k] = R[j];
            j++;
            k++;
        }
    }
     public static void MergeSort(ChessPlayer[] participants, int left, int right)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;

            MergeSort(participants, left, mid);
            MergeSort(participants, mid + 1, right);

            ChessPlayer.Merge(participants, left, mid, right);
        }
    }
}

internal class Program
{

    static void Main(string[] args)
    {
        ChessPlayer[] participants = new ChessPlayer[]
        {
            new ChessPlayer("Ivan", "Ptichin", 3, 7, 0),
            new ChessPlayer("Sergei", "Zhuravlev", 1, 4, 5),
            new ChessPlayer("Viktor", "Abricosov", 6, 2, 2),
            new ChessPlayer("Alexei", "Volcov", 7, 0, 3),
            new ChessPlayer("Dmitri", "Sobakevich", 10, 0, 0)
        };

        ChessPlayer.MergeSort(participants, 0, participants.Length-1);


        Console.WriteLine("ФИО\t\tИД\tИтоговый балл");

        foreach (ChessPlayer player in participants)
        {
            player.Print();
        }

        Console.ReadKey();
    }
}
