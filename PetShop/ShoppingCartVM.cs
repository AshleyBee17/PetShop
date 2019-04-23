using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PetShop {
    public class ShoppingCartVM : INotifyPropertyChanged {

        public ShopperHomeVM Parent { get; set; }
        XmlSerializer AcctSerializer = new XmlSerializer(typeof(ObservableCollection<Account>));
        XmlSerializer PetSerializer = new XmlSerializer(typeof(ObservableCollection<Animal>));
        ObservableCollection<Animal> AnimalCollection;
        ObservableCollection<Account> AccountList;
        string userPath = "userAccounts.xml";
        string animalPath = "animals.xml";
        Account LoggedInUser;

        public ShoppingCartVM(ShopperHomeVM parent, Account acct) {
            Parent = parent;
            LoggedInUser = acct;
            AccountList = PostgreSQL.getAllAccounts();
            AnimalCollection = PostgreSQL.getAllPets();
        }

        private void UpdateCartClicked(object obj) {
            // Update the cart contents of the logged in user based on their entries on the VM. 
            double sumTotal = 0;
            int sumItem = 0;
            List<object> deletedItems = new List<object>();

            // Looking through each item in the logged in user's cart
            foreach (Animal a in Parent.Cart.ToList()) {

                if ((double.Parse(a.Quantity) - double.Parse(a.PurchasedAmount)) < 0) {
                    MessageBox.Show($"Requested Quantity Exceeds the number of {a.Type}s in stock", "Invalid Quantity");
                } else if (double.Parse(a.PurchasedAmount) == 0) {
                    deletedItems.Add(a);
                } else {
                    // Update the total number of items and price in the user's cart
                    sumTotal += double.Parse(a.Price.Replace("$", "0")) * double.Parse(a.PurchasedAmount);
                    sumItem += int.Parse(a.PurchasedAmount);

                    foreach (Animal animal in AnimalCollection) {

                        if (animal.Type == a.Type) {
                            // Update the quantity as shown in the shopping cart and shopper home window
                            a.Quantity = (int.Parse(animal.Quantity) - int.Parse(a.PurchasedAmount)).ToString();

                            foreach(Account acc in AccountList) {

                                if (acc.id == LoggedInUser.id) { 

                                    foreach (Animal o in LoggedInUser.CartContent.ToList()) {

                                        if(o.PetID == animal.PetID) { 
                                            // Update cart total and item total in user account
                                            LoggedInUser.CartTotal = sumTotal.ToString();
                                            LoggedInUser.CartItems = sumItem.ToString();

                                            foreach(Animal ani in LoggedInUser.CartContent) {

                                                if(ani.Type == a.Type) {
                                                    // Update amount purchased in user account
                                                    ani.PurchasedAmount = a.PurchasedAmount;
                                                }
                                            }
                                        }
                                    }                        
                                }
                            }
                        }
                    }
                }

                // If an item is deleted, update all attributes related to it
                foreach (object animalToDelete in deletedItems) {
                    Parent.Cart.Remove(animalToDelete);
                    
                    foreach (Animal animal in AnimalCollection) {
                        Animal an = animalToDelete as Animal;
                        if (animal.Type == an.Type) {
                            a.Quantity = animal.Quantity;
                        }
                        foreach(Account acc in AccountList) {
                            if (acc.id == LoggedInUser.id) { 
                                foreach (Animal o in acc.CartContent.ToList()) {
                                    if(o.PetID == an.PetID) { 
                                        acc.CartContent.Remove(o);
                                        acc.CartTotal = sumTotal.ToString();
                                        acc.CartItems = sumItem.ToString();
                                    }
                                }                        
                            }
                        }
                    }
                }
                // Save all updated data to the XML file and refresh the displayed items in the cart and list box
                //SaveDataToXML(); // DELETE THIS 
                CollectionViewSource.GetDefaultView(Parent.Cart).Refresh();
                CollectionViewSource.GetDefaultView(Parent.lb.ItemsSource).Refresh();
            }
            Parent.TotalCost = sumTotal;
            Parent.TotalItem = sumItem;
        }

        private void DeleteClicked(object obj) {
            // set selected animal to be the object that was selected in the command parameter
            double difTotal = 0;
            int difItem = 0;
            Animal selectedAnimal = obj as Animal;

            // If no animal is selected, a reminder is shown
            if(selectedAnimal == null) { 
                MessageBox.Show("Please select a pet to remove","Pet not selected");
            } else {
                foreach(Animal a in Parent.Cart) {
                    if (a.Type == selectedAnimal.Type) {
                        // Update quantities accordingly
                        difTotal += double.Parse(a.Price.Replace("$", "0")) * double.Parse(a.PurchasedAmount);
                        difItem += int.Parse(a.PurchasedAmount);
                        a.Quantity = (int.Parse(a.Quantity) + difItem).ToString();
                    }                  
                }

                //Remove the item from the cart
                Parent.Cart.Remove(selectedAnimal);
                Parent.TotalCost = Parent.TotalCost - difTotal;
                Parent.TotalItem = Parent.TotalItem - difItem;

                // Remove the item from the user's account
                foreach(Account a in AccountList) {
                    if (a.id == LoggedInUser.id) { 
                        foreach (Animal o in LoggedInUser.CartContent.ToList()) {
                            if(o.PetID == selectedAnimal.PetID) { 
                                LoggedInUser.CartContent.Remove(o);
                                LoggedInUser.CartTotal = (int.Parse(LoggedInUser.CartTotal) - (int.Parse(selectedAnimal.PurchasedAmount) * int.Parse(selectedAnimal.Price))).ToString();
                                LoggedInUser.CartItems = (int.Parse(LoggedInUser.CartItems) - int.Parse(selectedAnimal.PurchasedAmount)).ToString();
                            }
                        }                        
                    }
                }
            }
            try {
                CollectionViewSource.GetDefaultView(Parent.Cart).Refresh();
                CollectionViewSource.GetDefaultView(Parent.lb.ItemsSource).Refresh();
            } catch (Exception e) {
                MessageBox.Show("Error, please reload the program. " + e);
            }
            //SaveDataToXML(); // DELETE THIS 
        }

        // DELETE THESE TWO METHODS
        private void SaveDataToXML() {
            using (FileStream writeStream = new FileStream(userPath, FileMode.Create, FileAccess.ReadWrite)) {
                AcctSerializer.Serialize(writeStream, AccountList);
            }
            using (FileStream writeStream = new FileStream(animalPath, FileMode.Create, FileAccess.ReadWrite)) {
                PetSerializer.Serialize(writeStream, AnimalCollection);
            }
        }

        private void ReadInDataFromXML() {
            using (FileStream readStream = new FileStream(animalPath,  FileMode.Open, FileAccess.Read)) {
                AnimalCollection = PetSerializer.Deserialize(readStream) as ObservableCollection<Animal>;
            }
            using (FileStream readStream = new FileStream(userPath, FileMode.Open, FileAccess.Read)) {
                AccountList = AcctSerializer.Deserialize(readStream) as ObservableCollection<Account>; 
            }
        }

        public ICommand UpdateCartCommand {
            get {
                if (_updateCartEvent == null) _updateCartEvent = new DelegateCommand(UpdateCartClicked);
                return _updateCartEvent;
            }
        }
        DelegateCommand _updateCartEvent;

        public ICommand DeleteItemCommand {
            get {
                if (_deleteItemEvent == null) _deleteItemEvent = new DelegateCommand(DeleteClicked);
                return _deleteItemEvent;
            }
        }
        DelegateCommand _deleteItemEvent;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
