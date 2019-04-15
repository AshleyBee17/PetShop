using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Diagnostics;

namespace PetShop { 
    class PostgreSQL{

        static List<string> dataItems = new List<string>();

        public static void x()
        {
            String connectionString = 
                "Server=127.0.0.1;" +
                "Port=5432;" +
                "Database=Database_Design_Group3;" +
                "User ID=postgres;" +
                "Password=14ae486acc344b86f85908647d9c2fd3d4b3dbaaea06f5f4d1e2c38840be9ee6";
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString);

            try {
                 npgsqlConnection.Open(); 
                // Define a query
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM pets", npgsqlConnection);

                // Execute a query
                NpgsqlDataReader dr = cmd.ExecuteReader();

                // Read all rows and output the first column in each row
                while (dr.Read())
                    Console.Write("{0}\n", dr[0]);

                // Close connection
                npgsqlConnection.Close();
            } catch (Exception e){
                Debug.WriteLine("Error Logged Here: " + e +" END LOGGING");
            }
        }

        public static List<string> PostgreSQLtest1(){
            try
            {
                string connstring = "Server=ec2-54-225-129-101.compute-1.amazonaws.com; Port=5433; User Id=postgres; Password=14ae486acc344b86f85908647d9c2fd3d4b3dbaaea06f5f4d1e2c38840be9ee6; Database=Database_Design_Group3;";
                NpgsqlConnection connection = new NpgsqlConnection(connstring);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM pets", connection);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                for (int i = 0; dataReader.Read(); i++)
                {
                    dataItems.Add(dataReader[0].ToString() + "," + dataReader[1].ToString() + "," + dataReader[2].ToString() + "\r\n");
                }
                connection.Close();
                return dataItems;
            }
            catch (Exception msg)
            {
                
                //MessageBox.Show(msg.ToString());
                throw;
            }
        }

    }
}
