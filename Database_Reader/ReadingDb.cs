using MySql.Data.MySqlClient;
namespace Database_Reader
{
    public class ReadingDb
    {
        public static string GetConnectionString()
        {
            try
            {
                Console.WriteLine("Enter the connection String");
                string? input = Console.ReadLine()?.Trim(); // Use null conditional operator to handle possible null input
                if (input != null)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Connection string was not provided.");
                    return ""; // Or handle the absence of connection string differently
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetConnectionString: " + ex.Message);
                throw;
            }
        }


        public static string GetOutputLocation()
        {
            try
            {
                Console.WriteLine("Enter the output location with txt file(Text file should be included in the path, like output.txt)");
                string? input = Console.ReadLine(); // input might be null

                string output = input ?? string.Empty; // If input is null, use an empty string

                return output;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetOutputLocation: " + ex.Message);
                throw;
            }
        }


        public static string[] GetSqlQueries()
        {
            try
            {
                Console.WriteLine("Enter the SQL queries comma separated");
                string? sqlInput = Console.ReadLine(); // sqlInput might be null

                if (sqlInput != null)
                {
                    string[] sqlQueries = sqlInput.Split(",");
                    return sqlQueries;
                }
                else
                {
                    Console.WriteLine("No SQL queries provided.");
                    return Array.Empty<string>(); // Return an empty array or handle it differently
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetSqlQueries: " + ex.Message);
                throw;
            }
        }



        public static void MySqlReader(string connectionString, string outputLocation, string[] sqlQueries)
        {
            string currentDateTime = DateTime.Now.ToString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                foreach (string sql in sqlQueries)
                {
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            string allData = "";
                            string caption = $"The output for the sql query: {sql} & Time Stamp is {currentDateTime}";

                            while (reader.Read())
                            {
                                string value = reader.GetString("Value");
                                string variableName = reader.GetString("Variable_name");

                                string line = $"{value}          {variableName}";
                                allData += line + Environment.NewLine;
                            }
                            allData = caption + Environment.NewLine + "Value   " + "Variable Name" + Environment.NewLine + allData;
                            File.AppendAllText(outputLocation, allData);
                        }
                    }
                }
            }
            Console.WriteLine($"Data has been written to the file at {currentDateTime}");
        }
    }
}
