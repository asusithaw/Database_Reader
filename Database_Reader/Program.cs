// See https://aka.ms/new-console-template for more information
using Database_Reader;
using System.Timers;

public class Program
{
    public static void Main(string[] args)
    {
        var timer = new System.Timers.Timer(2000);
        timer.Elapsed += OnEventExecution;
        timer.Start();       
    }

    public static void OnEventExecution(Object? sender, ElapsedEventArgs eventArgs)
    {
        ReadingDb.MySqlReader();
        
    }
}


        