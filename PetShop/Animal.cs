using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop {

    public class Animal {

        public int PetID { get; set; }
        public int OwnerID { get; set; }
        public string Type { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Age { get; set; }
        public string Size { get; set; }
        public string Zipcode { get; set; }
        public string PurchasedAmount { get; set; }

        

        //public Account Owner { get; set; }

        public Animal() {}

        public Animal(int oid, string type, string age, string size, string quantity, string price, string zip)
        {
            OwnerID = oid;
            Type = type;
            Age = age;
            Size = size;
            Quantity = quantity;
            Price = price;
            Zipcode = zip;
        }

        public Animal(int pid, int oid, string type, string age, string size, string quantity, string price, string zip) {
            PetID = pid;
            OwnerID = oid;
            Type = type;
            Age = age;
            Size = size;
            Quantity = quantity;
            Price = price;
            Zipcode = zip;
        }

    }
}
