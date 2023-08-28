// See https://aka.ms/new-console-template for more information
using Database_Reader;
using System.Timers;

public class Program
{
    public static void Main(string[] args)
    {
        var timer = new System.Timers.Timer(60*1000);
        timer.Elapsed += OnEventExecution;
        timer.Start();
        
        Console.ReadLine();
    }

    public static void OnEventExecution(Object? sender, ElapsedEventArgs eventArgs)
    {
        try
        {
            ReadingDb.MySqlReader();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

    }
}


        