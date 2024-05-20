using Laba_9_3.Serializers;
using System.Xml.Linq;
using ProtoBuf;
using System.Xml.Serialization;

[Serializable, ProtoContract]
public class Participant
{
    private string _firstName; private string _lastName;
    private int[] _exams; protected double _averageScore;
    [ProtoMember(1)]
    public string FirstName { get { return _firstName; } set { _firstName = value; } }
    [ProtoMember(2)]
    public string LastName { get { return _lastName; } set { _lastName = value; } }
    [ProtoMember(3)]
    public int[] Exams { get { return _exams; } set { _exams = value; } }
    [ProtoMember(4)]
    public double AvarageScore { get { return _averageScore; } set { _averageScore = value; } }

    public Participant() { }
    public Participant(string firstName, string lastName, int[] exams)
    {
        _firstName = firstName;
        _lastName = lastName; _exams = exams;
        _averageScore = CalculateAverageScore();
    }
    public double CalculateAverageScore()
    {
        int sum = 0;
        foreach (int exam in _exams)
        {
            sum += exam;
        }
        return (double)sum / _exams.Length;
    }
    public void Print()
    {
        Console.WriteLine($"{_lastName} {_firstName} {_averageScore}");
    }
}
[Serializable, ProtoContract]
[XmlInclude(typeof(Group))]
[XmlInclude(typeof(GroupA))]
[XmlInclude(typeof(GroupB))]
[XmlInclude(typeof(GroupC))]
[ProtoInclude(100, typeof(GroupA))]
[ProtoInclude(102, typeof(GroupB))]
[ProtoInclude(103, typeof(GroupC))]
//[ProtoInclude(104,typeof(Participant))]

public class Group
{
    protected Participant[] _participants;
    protected double _avarageScore;
    [ProtoMember(1)]
    public Participant[] Participants { get { return _participants; } set { _participants= value; } }
    [ProtoMember(2)]

    public double AvarageScore { get { return _avarageScore; } set { _avarageScore = value; } }
    public Group() { }
    public Group(Participant[] participants)
    {
        _participants = participants;
        _avarageScore = CalculateAverageScore();
    }
    public virtual double CalculateAverageScore()
    {
        double totalScore = 0;
        foreach (Participant participant in _participants)
        {
            totalScore += participant.CalculateAverageScore();
        }
        return totalScore / _participants.Length;
    }
    public virtual void Print()
    {
        for (int i = 0; i < _participants.Length; i++)
        {
            _participants[i].Print();

        }
        Console.Write(" " + _avarageScore);
        Console.WriteLine();
    }
}
public class GroupA : Group
{
    public GroupA() { }
    public GroupA(Participant[] participants) : base(participants) { }
    public override void Print()
    {
        for (int i = 0; i < _participants.Length; i++)
        {
            _participants[i].Print();

        }
        Console.Write("A " + _avarageScore);
        Console.WriteLine();
    }
}
public class GroupB : Group
{
    public GroupB() { }
    public GroupB(Participant[] participants) : base(participants) { }
    public override void Print()
    {
        for (int i = 0; i < _participants.Length; i++)
        {
            _participants[i].Print();

        }
        Console.Write("B " + _avarageScore);
        Console.WriteLine();
    }
}
public class GroupC : Group
{
    public GroupC() { }
    public GroupC(Participant[] participants) : base(participants) { }
    public override void Print()
    {
        for (int i = 0; i < _participants.Length; i++)
        {
            _participants[i].Print();

        }
        Console.Write("C " + _avarageScore);
        Console.WriteLine();
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        Participant[] participantsA = new Participant[]
        {
            new Participant("Ivan", "Ptichin", new int[] { 78, 67, 56, 90 }),
            new Participant("Sergei", "Zhuravlev", new int[] { 64, 89, 51, 99 }),
        };

        Participant[] participantsB = new Participant[]
        {
            new Participant("Stepa", "Pupkin", new int[] { 78, 67, 56, 90 }),
            new Participant("Dmitri", "Lebedev", new int[] { 64, 89, 51, 99 }),
        };
        Participant[] participantsC = new Participant[]
        {
            new Participant("Lesha", "Sitnikov", new int[] { 78, 67, 56, 90 }),
            new Participant("Sergei", "Samaev", new int[] { 64, 89, 51, 99 }),
        };
        Group gr = new Group(participantsC);
        GroupA groupA = new GroupA(participantsA);
        GroupB groupB = new GroupB(participantsB);
        GroupC groupC = new GroupC(participantsC);
        Group[] groups = new Group[] { groupA, groupB, groupC };

        Console.WriteLine("Группа\tСредний балл за сессию");
        string dirName = "Lab 9 3 - Solutions";
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
            "groups.json",
            "groups.xml",
            "groups.bin"
        };
        for (int i = 0; i < Serializers.Length; i++)
        {
            Serializers[i].Write(groups, Path.Combine(path, FileNames[i]));
        }
        for(int i=0; i < Serializers.Length; i++)
        {
            var grp = Serializers[i].Read<Group[]>(Path.Combine(path, FileNames[i]));
            foreach(var g in grp)
            {
                g.Print();
            }
            Console.WriteLine();
        }

    }
}
