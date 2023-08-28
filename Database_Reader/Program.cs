// See https://aka.ms/new-console-template for more information
using Database_Reader;
using System.Timers;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            // string connectionString = "datasource=localhost;port=3306;username=root;password=Susitha@1997;";
            //string output = "D:\ASP.net - Winforms\output.txt";
            //var sqlQueries = new string[] { "SHOW STATUS LIKE '%lock%';, SHOW STATUS LIKE '%current_waits%';, SHOW GLOBAL STATUS;" };
            string connectionString = ReadingDb.GetConnectionString();
            string outputLocation = ReadingDb.GetOutputLocation();
            string[] sqlQueries = ReadingDb.GetSqlQueries();

            var timer = new System.Timers.Timer(60000);
            timer.Elapsed += (sender,e) => OnEventExecution(sender, e, connectionString, outputLocation, sqlQueries);
            timer.Start();
            
            Console.ReadLine();
        }

        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }       
               
    }

    public static void OnEventExecution(Object? sender, ElapsedEventArgs eventArgs, string connectionString, string outputLocation, string[] sqlQueries)
    {
        try
        {          
            ReadingDb.MySqlReader(connectionString, outputLocation, sqlQueries);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

    }
}


        