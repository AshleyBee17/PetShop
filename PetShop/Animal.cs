using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop {

    public class Animal {

        public string Type { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Age { get; set; }
        public string Size { get; set; }
        public string Zipcode { get; set; }
        public string PurchasedAmount { get; set; }

        public Account Owner { get; set; }

        public Animal() {}

        public Animal(string type, string quantity, 
            string price, Account owner){
            Type = type;
            Quantity = quantity;
            Price = price;
            Owner = owner;
        }

        public Animal(string type, string age, string size, string quantity, string price, string zip, Account owner) { 
            Type = type;
            Age = age;
            Size = size;
            Quantity = quantity;
            Price = price;
            Zipcode = zip;
            Owner = owner;
        }

    }
}
