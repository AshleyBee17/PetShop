using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Diagnostics;
using System.Data;

namespace PetShop {
    class PostgreSQL
    {

        static List<string> dataItems = new List<string>();

        // defining things to connect to the database
        private static NpgsqlConnection conn;
        private static NpgsqlCommand npgCommand;
        private static string sql = null;

        static string connString = "Server=127.0.0.1;" +
                "Port=5432;" +
                "User ID=postgres;" +
                "Password=14ae486acc344b86f85908647d9c2fd3d4b3dbaaea06f5f4d1e2c38840be9ee6" +
                "Database=postgres;";

        public static void readPetsFromDB()
        {
            conn = new NpgsqlConnection(connString);
            conn.Open();
            sql = "SELECT * FROM pets;"; 
            npgCommand = new NpgsqlCommand(sql, conn);

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
        }

    }
}
