using System.Text.RegularExpressions;
using System;


abstract class participant
{
    protected string _surname;
    protected string _group;
    protected string _teachersurname;
    protected int _result;
    protected string _normative;


    public int result => _result; // read-only property
    public string normative => _normative; // read-only property
    public participant(string surname, string group, string teachersurname, int result)
    {
        _surname = surname;
        _group = group;
        _teachersurname = teachersurname;
        _result = result;
        _normative = "Не сдал";

    }
    public virtual void NormativePr(int result)
    {
    }

    public virtual void print()
    {
        Console.WriteLine(_surname + "     " + _group + "          " + _teachersurname + "          " + _result + "         ");
    }
}
class Competition_100 : participant
{
    public static int c { get; private set; }
    static Competition_100()
    {
        c = 0;
    }
    public Competition_100(string surname, string group, string teachersurname, int result) : base(surname, group, teachersurname, result)
    {
        NormativePr(result);
    }
    public override void print()
    {
        Console.WriteLine(_surname + "     " + _group + "          " + _teachersurname + "          " + _result + "         " + _normative + "Бег 100");
    }
    public override void NormativePr(int result)
    {
        if (_result >= 100)
        {
            _normative = "Сдал";
            c++;
        }

    }
}


class Competition_500 : participant
{
    public static int c { get; private set; }
    static Competition_500()
    {
        c = 0;
    }
    public Competition_500(string surname, string group, string teachersurname, int result) : base(surname, group, teachersurname, result)
    {
        NormativePr(result);
    }
    public override void print()
    {
        Console.WriteLine(_surname + "     " + _group + "          " + _teachersurname + "          " + _result + "         " + _normative + "Бег 500");
    }
    public override void NormativePr(int result)
    {

        if (_result >= 500)
        {
            _normative = "Сдал";
            c++;
        }

    }
}
internal class program
{
    static void Main(string[] args)
    {
        Competition_100[] participants = new Competition_100[5]
        {
            new Competition_100("sudakov", "a", "smit",90),
            new Competition_100("leontev", "b", "kozlov",501),
            new Competition_100("kirilov", "a", "smit",499),
            new Competition_100("kotikov", "b", "kozlov",80),
            new Competition_100("frolov", "b", "kozlov",389)
        };

        Competition_500[] participants1 = new Competition_500[5]
        {
            new Competition_500("sudakov", "a", "smit",90),
            new Competition_500("leontev", "b", "kozlov",501),
            new Competition_500("kirilov", "a", "smit",499),
            new Competition_500("kotikov", "b", "kozlov",580),
            new Competition_500("frolov", "b", "kozlov",389)
        };
        sort(participants);
        sort(participants1);

        Console.WriteLine("результирующая таблица:");
        Console.WriteLine("фамилия\t группа\t преподаватель\t результат\t выполнение норматива");
        for (int i = 0; i < participants.Length; i++)
        {
            participants[i].print();
        }
        Console.WriteLine();
        Console.WriteLine(Competition_100.c);
        Console.WriteLine();
        for (int i = 0; i < participants1.Length; i++)
        {
            participants1[i].print();
        }
        Console.WriteLine();
        Console.WriteLine(Competition_500.c);
        Console.ReadKey();



    }
    static void sort(participant[] participants)
    {
        int i = 1;
        int j = i + 1;
        while (i < participants.Length)
        {
            if (participants[i].result < participants[i - 1].result)
            {
                i = j;
                j++;
            }
            else
            {
                participant temp = participants[i];
                participants[i] = participants[i - 1];
                participants[i - 1] = temp;
                i--;
                if (i == 0)
                {
                    i = j;
                    j++;
                }
            }
        }

    }
}

