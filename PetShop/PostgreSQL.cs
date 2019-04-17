using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Diagnostics;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace PetShop {
    class PostgreSQL {

        // defining things to connect to the database
        private static NpgsqlConnection conn;
        private static NpgsqlCommand npgCommand;
        private static string sql = null;
        static string host = "ec2-54-225-129-101.compute-1.amazonaws.com";
        static string server2 = "Server=127.0.0.1";
        static string id = "sqpdoquiqznjdl";
        static string password = "14ae486acc344b86f85908647d9c2fd3d4b3dbaaea06f5f4d1e2c38840be9ee6";
        static string db = "d77bi6lo5qhe6r";
        static string port = "5432";
        public static SqlConnection dbConnection;

        static string connString = "Server=127.0.0.1;" +
                "Port=5432;" +
                "Username=sqpdoquiqznjdl;" +
                "Password=14ae486acc344b86f85908647d9c2fd3d4b3dbaaea06f5f4d1e2c38840be9ee6;" +
                "Database=d77bi6lo5qhe6r;";

        static string connString2 = "psql " +
            "host=ec2-54-225-129-101.compute-1.amazonaws.com " +
            "port=5432 --username=sqpdoquiqznjdl " +
            "password=14ae486acc344b86f85908647d9c2fd3d4b3dbaaea06f5f4d1e2c38840be9ee6 " +
            "dbname=d77bi6lo5qhe6r";

        static string connString3 = //"psql " +
           // "Server=127.0.0.1" +
            "Port=5432" +
            "Username=sqpdoquiqznjdl" +
            "Password=14ae486acc344b86f85908647d9c2fd3d4b3dbaaea06f5f4d1e2c38840be9ee6" +
            "Database=d77bi6lo5qhe6r";

        public static ObservableCollection<Animal> readPetsFromDB()
        {
            /*
            conn = new NpgsqlConnection(connString3);
            conn.Open();
            sql = "SELECT * FROM pets;"; 
            npgCommand = new NpgsqlCommand(sql, conn);
            ObservableCollection<Animal> animalList = new ObservableCollection<Animal>();

            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(npgCommand);
            DataTable dataTable = new DataTable();

            npgsqlDataAdapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    Console.Write("{0} \n", row[i].ToString());
                }
            }
            */

            string connectionString = string.Format("Server = {0};Database = {1}; User ID = {2}; Password = {3};", host, db, id, password);
            dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            return null;//animalList;
        }

    }
}
