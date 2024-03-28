using System;

public class Group
{
    protected int[] _exams = new int[4]; 

    public virtual double CalculateAverageScore()
    {
        int sum = 0;
        foreach (int exam in _exams)
        {
            sum += exam;
        }
        return (double)sum / _exams.Length;
    }
}

public class GroupA : Group
{
    private int[] _additionalExams = new int[2]; 

    public GroupA(int[] exams, int[] additionalExams)
    {
        _exams = exams;
        _additionalExams = additionalExams;
    }

    public override double CalculateAverageScore()
    {
        int sum = 0;
        foreach (int exam in _exams)
        {
            sum += exam;
        }
        foreach (int exam in _additionalExams)
        {
            sum += exam;
        }
        return (double)sum / (_exams.Length + _additionalExams.Length);
    }
}

public class GroupB : Group
{
    private int[] _additionalExams = new int[2]; 

    public GroupB(int[] exams, int[] additionalExams)
    {
        _exams = exams;
        _additionalExams = additionalExams;
    }

    public override double CalculateAverageScore()
    {
        int sum = 0;
        foreach (int exam in _exams)
        {
            sum += exam;
        }
        foreach (int exam in _additionalExams)
        {
            sum += exam;
        }
        return (double)sum / (_exams.Length + _additionalExams.Length);
    }
}

public class GroupC : Group
{
    private int[] _additionalExams = new int[2]; 

    public GroupC(int[] exams, int[] additionalExams)
    {
        _exams = exams;
        _additionalExams = additionalExams;
    }

    public override double CalculateAverageScore()
    {
        int sum = 0;
        foreach (int exam in _exams)
        {
            sum += exam;
        }
        foreach (int exam in _additionalExams)
        {
            sum += exam;
        }
        return (double)sum / (_exams.Length + _additionalExams.Length);
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        Group[] groups = new Group[]
        {
            new GroupA(new int[] { 78, 67, 56, 90 }, new int[] { 82, 75 }),
            new GroupB(new int[] { 64, 89, 51, 99 }, new int[] { 88, 77 }),
            new GroupC(new int[] { 76, 59, 81, 63 }, new int[] { 75, 70 }),
        };

        Console.WriteLine("Группа\tСредний балл за сессию");
        foreach (Group group in groups)
        {
            Console.WriteLine($"Группа\t{group.CalculateAverageScore()}");
        }

        Console.ReadKey();
    }
}