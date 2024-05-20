using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using Laba_9_2.Serializers;
using ProtoBuf;
[XmlInclude(typeof (ChessPlayer))]
[ProtoInclude(3, typeof(ChessPlayer))]
[ProtoInclude(4, typeof(ChessPlayer))]
[Serializable, ProtoContract]
public class People
{
    private string _name;
    private string _surname;
    public People() { }
    public People(string name, string surname)
    {
        _name = name;
        _surname = surname;
    }
    [ProtoMember(1)]
    public string Name { get { return _name; } set { _name = value; } }
    [ProtoMember(2)]
    public string Surname { get { return _surname; } set { _surname = value; } }
}
[Serializable, ProtoContract]
public class ChessPlayer : People
{
    private static int _idCounter = 1;
    private int _id;
    private double _finalScore;
    [ProtoMember(3)]
    public int ID { get { return _id; } set { _id = value; } }
    [ProtoMember(4)]
    public double FinalScore { get { return _finalScore; } set { _finalScore = value; } }
    public ChessPlayer() { }
    public ChessPlayer(string name, string surname, int wins, double draws, int losses) : base(name, surname)
    {
        _id = _idCounter++;
        _finalScore = wins + draws / 2;
    }

    

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
            new ChessPlayer("Viktor", "Abricosov", 6, 2, 2),
            new ChessPlayer("Alexei", "Volcov", 7, 0, 3),
            new ChessPlayer("Dmitri", "Sobakevich", 10, 0, 0)
        };

        Sort(participants);

        Console.WriteLine("ФИО\t\tИД\tИтоговый балл");
        string dirName = "Lab 9 2 - Solutions";
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        path = Path.Combine(path, dirName);
        if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
        MySer[] Serializers =
        {
            new MyJson(),
            new MyXml(),
            new MyBin()
        };
        string[] FileNames =
        {
            "ChessPlayers.json",
            "ChessPlayers.xml",
            "ChessPlayers.bin"
        };
        for (int i = 0; i < Serializers.Length; i++)
        {
            Serializers[i].Write(participants, Path.Combine(path, FileNames[i]));
        }
        for (int i = 0; i < Serializers.Length; i++)
        {
            var ChessPlayers = Serializers[i].Read<ChessPlayer[]>(Path.Combine(path, FileNames[i]));
            foreach(var CP in ChessPlayers)
            {
                CP.Print();
            }
            Console.WriteLine();
        }

    }
}