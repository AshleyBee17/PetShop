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

        static string cs = "Server=127.0.0.1;" +
                "Port=5432;" +
                "Username=postgres;" +
                "Password=14ae486acc344b86f85908647d9c2fd3d4b3dbaaea06f5f4d1e2c38840be9ee6;" +
                "Database=postgres;";

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
            
            conn = new NpgsqlConnection(cs);
            conn.Open();
            sql = "SELECT * FROM pets;"; 
            npgCommand = new NpgsqlCommand(sql, conn);
            ObservableCollection<Animal> animalList = new ObservableCollection<Animal>();

            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(npgCommand);
            DataTable dataTable = new DataTable();

            npgsqlDataAdapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows) { 
                Animal a = new Animal();
                a.Size = row.Field<string>("Size");
                a.Type = row.Field<string>("Type");
                a.Age = row.Field<string>("Age");
                a.Quantity = row.Field<string>("Quantity");
                a.Price = row.Field<string>("Price");
                a.Zipcode = row.Field<string>("Location");

                animalList.Add(a);
                /*
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    Console.Write("{0} \n", row[i].ToString());
                }
                */
            }

            conn.Close();
            return animalList;
        }

        public static ObservableCollection<Animal> getOwnersPets(int ownerID) {
            //ownerID = 1;

            conn = new NpgsqlConnection(cs);
            conn.Open();
            sql = "SELECT * FROM pets WHERE \"OwnerID\"=" + ownerID +";";
            npgCommand = new NpgsqlCommand(sql, conn);
            ObservableCollection<Animal> animalList = new ObservableCollection<Animal>();

            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(npgCommand);
            DataTable dataTable = new DataTable();

            npgsqlDataAdapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                Animal a = new Animal();
                a.Size = row.Field<string>("Size");
                a.Type = row.Field<string>("Type");
                a.Age = row.Field<string>("Age");
                a.Quantity = row.Field<string>("Quantity");
                a.Price = row.Field<string>("Price");
                a.Zipcode = row.Field<string>("Location");

                animalList.Add(a);
            }
            conn.Close();
            return animalList;
        }

        public static ObservableCollection<Animal> searchByAge(int petAge) {

            conn = new NpgsqlConnection(cs);
            conn.Open();
            sql = "SELECT * FROM pets WHERE \"Age\"=" + petAge + ";";
            npgCommand = new NpgsqlCommand(sql, conn);
            ObservableCollection<Animal> animalList = new ObservableCollection<Animal>();

            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(npgCommand);
            DataTable dataTable = new DataTable();

            npgsqlDataAdapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                Animal a = new Animal();
                a.Size = row.Field<string>("Size");
                a.Type = row.Field<string>("Type");
                a.Age = row.Field<string>("Age");
                a.Quantity = row.Field<string>("Quantity");
                a.Price = row.Field<string>("Price");
                a.Zipcode = row.Field<string>("Location");

                animalList.Add(a);
            }
            conn.Close();
            return animalList;
        }
    }
}
