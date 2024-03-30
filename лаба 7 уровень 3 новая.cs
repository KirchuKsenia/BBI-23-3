public class Participant
{
    private string _firstName; private string _lastName;
    private int[] _exams; protected double _averageScore;
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
public class Group
{
    protected Participant[] _participants;
    protected double _avarageScore;
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
internal class Program
{
    static void Main(string[] args)
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
        for (int i = 0; i < groups.Length; i++)
        {
            groups[i].Print();
        }
        Console.ReadKey();
    }
}
