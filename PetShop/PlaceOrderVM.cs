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
        

        public ShopperHomeVM Parent { get; set; }
        public Account LoggedInShopper;
        public string ReceiptText;

        public PlaceOrderVM(ShopperHomeVM parent, Account acct) {
            this.Parent = parent;
            this.LoggedInShopper = acct;
         
            AnimalCollection = PostgreSQL.getAllPets();
            AccountList = PostgreSQL.getAllAccounts();

            // Receipt Text
            string header = $"Receipt For {LoggedInShopper.FirstName} {LoggedInShopper.LastName}\n";
            this.ReceiptText = header;
            UpdateQuantities();
        }
        
        private void UpdateQuantities() {           
            foreach (Animal a in Parent.Cart) {
                foreach (Animal animal in AnimalCollection) {
                    if (a.PetID == animal.PetID) {
                        animal.Quantity = (int.Parse(animal.Quantity) - int.Parse(a.PurchasedAmount)).ToString();
                        PostgreSQL.editPet(animal);
                    }
                }
                
                foreach (Account acc in AccountList) {
                   if (acc.id == LoggedInShopper.id) { 
                       foreach (Animal o in LoggedInShopper.CartContent.ToList()) {
                           if(o.PetID == a.PetID) {
                                //LoggedInShopper.PreviousPurchases.Add(o);
                                LoggedInShopper.CartContent.Remove(o);
                           }
                       }
                        LoggedInShopper.CartTotal = "0";
                        LoggedInShopper.CartItems = "0";
                       
                   }
                }    
            }
            
        }
        

        private void SaveReceipt(object obj) {
            List<string> receipt = new List<string>();
            int cartTotal = 0;

            foreach (Account acc in AccountList)
            {
                if(acc.Username == LoggedInShopper.Username)
                {
                    foreach(Animal a in LoggedInShopper.PreviousPurchases) {
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
