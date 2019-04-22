using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop {
    public class Account {

        public int id { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ZipCode { get; set; }
        public string Telephone { get; set; }
        public string CartItems { get; set; }
        public string CartTotal { get; set; }
        public string Type { get; set; }

        public ObservableCollection<Animal> CartContent { get; set; }
        public ObservableCollection<Animal> PreviousPurchases { get; set; }

        public Account() {}

        public Account(string firstName, string lastName, string username, string password, 
            string zipCode, string telephone, string cart, string type,
            string cartItems, string cartTotal) {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Telephone = telephone;
            ZipCode = zipCode;
            Type = type;

            CartItems = cartItems;
            CartTotal = cartTotal;
            
        }

        public Account(int Id, string firstName, string lastName, string username, string password,
            string type, string telephone, string zipcode, string cartItems, string cartTotal)
        {
            id = Id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Telephone = telephone;
            Type = type;
            ZipCode = zipcode;
            CartItems = cartItems;
            CartTotal = CartTotal;

        }

        public Account(int Id, string firstName, string lastName, string username, string password,
            string type, string telephone)
        {
            id = Id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Telephone = telephone;
            Type = type;
        }

        public Account(int Id, string zipcode)
        {
            id = id;
            ZipCode = zipcode;
        }

        public Account(int Id, string cartItems, string cartTotal)
        {
            id = id;
            CartItems = cartItems;
            CartTotal = cartTotal;
        }


    }
}
