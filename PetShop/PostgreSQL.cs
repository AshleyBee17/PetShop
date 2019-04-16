using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Diagnostics;
using System.Data;

namespace PetShop { 
    class PostgreSQL{

        static List<string> dataItems = new List<string>();

        // defining things to connect to the database
        private static NpgsqlConnection conn;
        //static string connString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", 
        //    "localhost", "5432", "user_a", "14ae486acc344b86f85908647d9c2fd3d4b3dbaaea06f5f4d1e2c38840be9ee6", "d77bi6lo5qhe6r");
        private static NpgsqlCommand npgCommand;
        private static string sql = null;
        
        static string connString = "Server=127.0.0.1;" +
                "Port=5432;" +
                "User ID=postgres;" +
                "Password=14ae486acc344b86f85908647d9c2fd3d4b3dbaaea06f5f4d1e2c38840be9ee6" +
                "Database=d77bi6lo5qhe6r;";

        public static void x()
        {
            conn = new NpgsqlConnection(connString);
            conn.Open();
            sql = "SELECT * FROM pets"; // CAN'T FIND PETS TABLE??
            npgCommand = new NpgsqlCommand(sql, conn);

            string user = conn.UserName;
            string idk = conn.DataSource;

            //DataTable dt = conn.GetSchema("Tables");

            //string res = (string)npgCommand.ExecuteScalar();
            //Debug.WriteLine("RESULTS: " +  npgCommand.ToString());

            NpgsqlDataReader reader = npgCommand.ExecuteReader();
            while (reader.Read())
            {
                string x = reader.GetInt32(0).ToString();
            }

            /*
            try
            {
                conn.Open();
                sql = "SELECT * FROM pets";
                npgCommand = new NpgsqlCommand(sql, conn);

                int result = (int)npgCommand.ExecuteScalar();
                if(result == 1)
                {
                    Debug.WriteLine("Query searched");
                } else Debug.WriteLine("Query not found");

            } catch (Exception e)
            {
                Debug.WriteLine("Error Logged Here: " + e + " END LOGGING");
                conn.Close();
            }*/
        }

        public static void xl()
        {
            string connectionString = 
                "Server=127.0.0.1;" +
                "Port=5433;" +
                "Database=d77bi6lo5qhe6r;" +
                "User ID=sqpdoquiqznjdl;" +
                "Password=14ae486acc344b86f85908647d9c2fd3d4b3dbaaea06f5f4d1e2c38840be9ee6";
            /*
            String connectionString =
                "Server=127.0.0.1;" +
                "Port=5433;" +
                "Database=ec2-54-225-129-101.compute-1.amazonaws.com;" +
                "User ID=sqpdoquiqznjdl;" +
                "Password=14ae486acc344b86f85908647d9c2fd3d4b3dbaaea06f5f4d1e2c38840be9ee6";*/

            NpgsqlConnectionStringBuilder key = new NpgsqlConnectionStringBuilder();
            key.ConnectionString = "";

            NpgsqlConnection   npgsqlConnection = new NpgsqlConnection(key.ToString());
            npgsqlConnection.Open();

         

            /*
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
            }*/
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
