using System.Text.RegularExpressions;
using System;
using Laba_9.Serializers;
using System.Xml.Serialization;
using ProtoBuf;
[Serializable]
[ProtoContract]
[XmlInclude(typeof (Competition_100))]
[XmlInclude(typeof(Competition_500))]
[ProtoInclude (100, typeof(Competition_100))]
[ProtoInclude(101, typeof(Competition_500))]
public abstract class participant
{
    [ProtoMember(1)]
    public string _surname {  get;  set; }
    [ProtoMember(2)]
    public string _group { get; set; }
    [ProtoMember(3)]
    public string _teachersurname { get; set; }
    [ProtoMember(4)]
    public int _result { get; set; }
    [ProtoMember(5)]
    public string _normative { get; set; }





    public participant() { }
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
[ProtoContract]
public class Competition_100 : participant
{
    public static int c { get; private set; }
    public Competition_100() { }
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

[ProtoContract]
public class Competition_500 : participant
{
    public static int c { get; private set; }
    public Competition_500() { } 
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
public class program
{
    public static void Main(string[] args)
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
        

        string dirName = "Lab 9 1 - Solutions";
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
            "participant100.json",
            "participant100.xml",
            "participant100.bin",
            "participant500.json",
            "participant500.xml",
            "participant500.bin"

        };
        for(int i =0; i< Serializers.Length; i++)
        {
            Serializers[i].Write(participants, Path.Combine(path, FileNames[i]));
            Serializers[i].Write(participants1, Path.Combine(path, FileNames[i+3]));

        }
        for (int i = 0; i < Serializers.Length; i++)
        {
            var part100 = Serializers[i].Read<Competition_100[]>(Path.Combine(path, FileNames[i]));
            foreach (var p in part100)
            {
                p.print();
            }
            Console.WriteLine();
            var part500 = Serializers[i].Read<Competition_500[]>(Path.Combine(path, FileNames[i+3]));
            foreach (var p in part500)
            {
                p.print();
            }
            Console.WriteLine();
        }
    }

    static void sort(participant[] participants)
    {
        int i = 1;
        int j = i + 1;
        while (i < participants.Length)
        {
            if (participants[i]._result < participants[i - 1]._result)
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

