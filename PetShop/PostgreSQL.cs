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

        static string cs = "Server=127.0.0.1;" +
                            "Port=5432;" +
                            "Username=postgres;" +
                            "Password=14ae486acc344b86f85908647d9c2fd3d4b3dbaaea06f5f4d1e2c38840be9ee6;" +
                            "Database=postgres;";

        public static ObservableCollection<Animal> getAllPets() {
            conn = new NpgsqlConnection(cs);
            conn.Open();
            sql = "SELECT * FROM pets;"; 
            npgCommand = new NpgsqlCommand(sql, conn);

            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(npgCommand);
            DataTable dataTable = new DataTable();

            npgsqlDataAdapter.Fill(dataTable);

            conn.Close();
            return returnAnimals(dataTable);
        }

        public static ObservableCollection<Animal> getOwnersPets(int ownerID) {

            conn = new NpgsqlConnection(cs);
            conn.Open();
            sql = "SELECT * FROM pets WHERE \"OwnerID\"=" + ownerID +";";
            npgCommand = new NpgsqlCommand(sql, conn);

            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(npgCommand);
            DataTable dataTable = new DataTable();

            npgsqlDataAdapter.Fill(dataTable);

            conn.Close();
            return returnAnimals(dataTable);
        }

        public static ObservableCollection<Animal> searchByAge(string petAge) {

            conn = new NpgsqlConnection(cs);
            conn.Open();
            sql = "SELECT * FROM pets WHERE \"Age\"=\'"+ petAge +"\';";
            npgCommand = new NpgsqlCommand(sql, conn);

            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(npgCommand);
            DataTable dataTable = new DataTable();

            npgsqlDataAdapter.Fill(dataTable);

            conn.Close();
            return returnAnimals(dataTable);
        }

        public static ObservableCollection<Animal> searchByZip(string petZip)
        {

            conn = new NpgsqlConnection(cs);
            conn.Open();
            sql = "SELECT * FROM pets WHERE \"Location\"=\'" + petZip + "\';";
            npgCommand = new NpgsqlCommand(sql, conn);

            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(npgCommand);
            DataTable dataTable = new DataTable();

            npgsqlDataAdapter.Fill(dataTable);

            conn.Close();
            return returnAnimals(dataTable);
        }

        public static ObservableCollection<Animal> searchByPrice(string petPrice)
        {

            conn = new NpgsqlConnection(cs);
            conn.Open();
            sql = "SELECT * FROM pets WHERE \"Price\"=\'" + petPrice + "\';";
            npgCommand = new NpgsqlCommand(sql, conn);

            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(npgCommand);
            DataTable dataTable = new DataTable();

            npgsqlDataAdapter.Fill(dataTable);

            conn.Close();
            return returnAnimals(dataTable);
        }

        public static ObservableCollection<Animal> searchByType(string petType)
        {

            conn = new NpgsqlConnection(cs);
            conn.Open();
            sql = "SELECT * FROM pets WHERE \"Type\"=\'" + petType + "\';";
            npgCommand = new NpgsqlCommand(sql, conn);

            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(npgCommand);
            DataTable dataTable = new DataTable();

            npgsqlDataAdapter.Fill(dataTable);

            conn.Close();
            return returnAnimals(dataTable);
        }

        private static ObservableCollection<Animal> returnAnimals(DataTable d)
        {
            ObservableCollection<Animal> animalList = new ObservableCollection<Animal>();

            foreach (DataRow row in d.Rows)
            {
                Animal a = new Animal();
                a.PetID = row.Field<int>("PetID");
                a.OwnerID = row.Field<int>("OwnerID");
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

        public static void addPet(Animal a)
        {
            conn = new NpgsqlConnection(cs);
            conn.Open();

            npgCommand = new NpgsqlCommand();
            npgCommand.Connection = conn; 

            npgCommand.CommandText = "INSERT INTO public.pets(\"OwnerID\", \"Size\", \"Type\", \"Age\", \"Quantity\", \"Price\", \"Location\") " +
                "VALUES (@oid, @s, @t, @a, @q, @p, @l)";


            npgCommand.Parameters.AddWithValue("oid", 0);
            npgCommand.Parameters.AddWithValue("s", a.Size);
            npgCommand.Parameters.AddWithValue("t", a.Type);
            npgCommand.Parameters.AddWithValue("a", a.Age);
            npgCommand.Parameters.AddWithValue("q", a.Quantity);
            npgCommand.Parameters.AddWithValue("p", a.Price);
            npgCommand.Parameters.AddWithValue("l", a.Zipcode);

            npgCommand.ExecuteNonQuery();
            conn.Close();
        }

        public static void deletePet(int petID)
        {
            string id = petID.ToString();

            conn = new NpgsqlConnection(cs);
            conn.Open();

            npgCommand = new NpgsqlCommand();
            npgCommand.Connection = conn;

            npgCommand.CommandText = "DELETE FROM public.pets WHERE \"PetID\"=\'" + id + "\';";

            npgCommand.ExecuteNonQuery();
            conn.Close();
        }

        public static void editPet(Animal a)
        {
            string id = a.PetID.ToString();

            conn = new NpgsqlConnection(cs);
            conn.Open();

            npgCommand = new NpgsqlCommand();
            npgCommand.Connection = conn;

            npgCommand.CommandText = "UPDATE public.pets SET " +
                "\"Size\"=\'"+ a.Size +"\'," +
                "\"Type\"=\'" + a.Type + "\'," +
                "\"Age\"=\'" + a.Age + "\'," +
                "\"Quantity\"=\'" + a.Quantity + "\'," +
                "\"Price\"=\'" + a.Price + "\'," +
                "\"Location\"=\'" + a.Zipcode +
                "\' WHERE \"PetID\"=\'" + id + "\';";

            npgCommand.ExecuteNonQuery();
            conn.Close();
        }

    }
}
