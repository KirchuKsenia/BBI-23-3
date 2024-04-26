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


 
class Program
{
    static void Main()
    {

        string path = @"C:\Users\m2305490\Downalds";
        string folderName = "Control work";
        path = Path.Combine(path, folderName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string fileName1 = "task_1.json";
        string fileName2 = "task_2.json";

        fileName1 = Path.Combine(path, fileName1);
        fileName2 = Path.Combine(path, fileName2);


        if (!File.Exists(fileName1))
        {
            var filec = File.Create(fileName1);
            filec.Close();
        }
    }
}