using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PetShop {
    class PlaceOrderVM : INotifyPropertyChanged {

        public static ObservableCollection<Animal> AnimalCollection { get; set; }
        public static ObservableCollection<Account> AccountList { get; set; } 
        XmlSerializer PetSerializer = new XmlSerializer(typeof(ObservableCollection<Animal>));
        XmlSerializer AcctSerializer = new XmlSerializer(typeof(ObservableCollection<Account>));
        string animalPath = "animals.xml";
        string userPath = "userAccounts.xml";

        public ShopperHomeVM Parent { get; set; }
        public Account LoggedInShopper;
        public string ReceiptText;

        public PlaceOrderVM(ShopperHomeVM parent, Account acct) {
            this.Parent = parent;
            this.LoggedInShopper = acct;
           
            try {
                ReadInDataFromXML();
            } catch {
                Console.WriteLine("File could not be opened to be read.");
                MessageBox.Show($"File failed to open because it is empty or does not exist. The file has now been created for you.","File Failed to Open");
            }

            // Receipt Text
            string header = $"Receipt For {LoggedInShopper.FirstName} {LoggedInShopper.LastName}\n";
            this.ReceiptText = header;
            UpdateQuantities();
        }

        private void ReadInDataFromXML() {
            using (FileStream readStream = new FileStream(animalPath,  FileMode.Open, FileAccess.Read)) {
                AnimalCollection = PetSerializer.Deserialize(readStream) as ObservableCollection<Animal>;
            }
            using (FileStream readStream = new FileStream(userPath, FileMode.Open, FileAccess.Read)) {
                AccountList = AcctSerializer.Deserialize(readStream) as ObservableCollection<Account>; 
            }
        }

        private void UpdateQuantities() {           
            foreach (Animal a in Parent.Cart) {
                foreach (Animal animal in AnimalCollection) {
                    if (a.Type == animal.Type) {
                        animal.Quantity = (int.Parse(animal.Quantity) - int.Parse(a.PurchasedAmount)).ToString();
                    }
                }
                
                foreach (Account acc in AccountList) {
                   if (acc.id == LoggedInShopper.id) { 
                       foreach (Animal o in acc.CartContent.ToList()) {
                           if(o.PetID == a.PetID) { 
                               acc.PreviousPurchases.Add(o);                        
                               acc.CartContent.Remove(o);
                           }
                       } 
                       acc.CartTotal = "0";
                       acc.CartItems = "0";
                       //Parent.TotalItem = 0;
                       //Parent.TotalCost = 0;
                   }
                }    
            }
            SaveDataToXML();
        }

        private void SaveDataToXML() {
            using (FileStream writeStream = new FileStream(userPath, FileMode.Create, FileAccess.ReadWrite)) {
                AcctSerializer.Serialize(writeStream, AccountList);
            }
            using (FileStream writeStream = new FileStream(animalPath, FileMode.Create, FileAccess.ReadWrite)) {
                PetSerializer.Serialize(writeStream, AnimalCollection);
            }
        }

        private void SaveReceipt(object obj) {
            List<string> receipt = new List<string>();
            int cartTotal = 0;

            foreach (Account acc in AccountList)
            {
                if(acc.Username == LoggedInShopper.Username)
                {
                    foreach(Animal a in acc.PreviousPurchases) {
                        int petTotal = int.Parse(a.Price) * int.Parse(a.PurchasedAmount);
                        cartTotal += petTotal;
                        receipt.Add($"{Environment.NewLine}Pet: {a.Type} {Environment.NewLine}Price: {a.Price} {Environment.NewLine}Amt Purchased: {a.PurchasedAmount} {Environment.NewLine}---------- Item Total: ${petTotal}");
                    }

                    SaveFileDialog dialog = new SaveFileDialog() {
                        Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
                    };

                    if (dialog.ShowDialog() == true) {
                        File.AppendAllText(dialog.FileName, $"{ReceiptText} {Environment.NewLine}");
                        foreach (string s in receipt) {
                            File.AppendAllText(dialog.FileName, s);
                        }
                        File.AppendAllText(dialog.FileName, $"{Environment.NewLine}{Environment.NewLine} Your cart total: ${cartTotal}");
                    }
                }
            }
        }

        public ICommand SaveReceiptCommand {
            get {
                if (_saveFileEvent == null) _saveFileEvent = new DelegateCommand(SaveReceipt);
                return _saveFileEvent;
            }
        }
        DelegateCommand _saveFileEvent;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
