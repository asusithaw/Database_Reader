using MySql.Data.MySqlClient;
namespace Database_Reader
{
    public class ReadingDb
    {        
        public static void MySqlReader()
        {
            string connectionString = "datasource=localhost;port=3306;username=root;password=Susitha@1997;";
            string output = @"D:\\ASP.net - Winforms\\output.txt";
            string currentDateTime = DateTime.Now.ToString();
            var sqlQueries = new string[] { "SHOW STATUS LIKE '%lock%';", "SHOW STATUS LIKE '%current_waits%';", "SHOW GLOBAL STATUS;" };

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
                            File.AppendAllText(output, allData);
                        }
                    }
                }
            }
            Console.WriteLine($"Data has been written to the file at {currentDateTime}");
        }
    }
}
