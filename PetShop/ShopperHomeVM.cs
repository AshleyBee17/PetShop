﻿using System;
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
    public class ShopperHomeVM : INotifyPropertyChanged {

        Account LoggedInUser;

        public static string stype;
        public static string stxt;

        private Account _accountCartTotal;
        public Account AccountCartTotal {
            get { return _accountCartTotal; }
            set {
                _accountCartTotal = value;
                PropertyChanged(this, new PropertyChangedEventArgs("AccountCartTotal"));
            }
        }

        private ObservableCollection<object> _cart = new ObservableCollection<object>();
        public ObservableCollection<object> Cart {
            get { return _cart; }
            set {
                _cart = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Cart"));
            }
        }

        private string _totalCostDisplay;
        public string TotalCostDisplay {
            get { return _totalCostDisplay; }
            set {
                _totalCostDisplay = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TotalCostDisplay"));
            }
        }

        private string _totalItemDisplay;
        public string TotalItemDisplay {
            get { return _totalItemDisplay; }
            set {
                _totalItemDisplay = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TotalItemDisplay"));
            }
        }

        private string _totalAvailableDisplay;
        public string TotalAvailableDisplay {
            get { return _totalAvailableDisplay; }
            set {
                _totalAvailableDisplay = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TotalAvailableDisplay"));
            }
        }

        private string _quantity;
        public string Quantity {
            get { return _quantity; }
            set {
                _quantity = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Quantity"));
            }
        }

        private  string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SearchText"));
            }
        }

        private  string _searchType;
        public  string SearchType
        {
            get { return _searchType; }
            set
            {
                _searchType = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SearchType"));
            }
        }

        private double _totalCost = 0;
        public double TotalCost {
            get { return _totalCost; }
            set {
                _totalCost = value;
                TotalCostDisplay = "$" + string.Format("{0:N2}", _totalCost);
                PropertyChanged(this, new PropertyChangedEventArgs("TotalCost"));
            }
        }

        private double _totalItem = 0;
        public double TotalItem {
            get { return _totalItem; }
            set {
                _totalItem = value;
                TotalItemDisplay = "The " + string.Format("{0}", _totalItem) + " item(s) in your cart total:";
                PropertyChanged(this, new PropertyChangedEventArgs("TotalItem"));
            }
        }

        private double _totalAvailable = 0;
        public double TotalAvailable {
            get { return _totalAvailable; }
            set {
                _totalAvailable = value;
                TotalAvailableDisplay = "$" + string.Format("{0}", _totalAvailable);
                PropertyChanged(this, new PropertyChangedEventArgs("TotalAvailable"));
            }
        }

        public ListBox lb;
        bool isCartEmpty;

        public ShopperHomeVM(Account acct) {
            this.LoggedInUser = acct;
            
   
            SearchType = "Age";
            if(LoggedInUser.CartContent != null) {
                foreach(object o in LoggedInUser.CartContent) {
                    Animal an = o as Animal;
                    Cart.Add(an);
                    TotalItem = TotalItem += int.Parse(an.PurchasedAmount);
                    TotalCost = TotalCost += int.Parse(an.PurchasedAmount) * int.Parse(an.Price);
                }
            }
        }
        ObservableCollection<Animal> AnimalCollection;

        private void AddToCartClicked(object obj) {

            ObservableCollection<Account> AccountList;
            AnimalCollection = PostgreSQL.getAllPets();
            AccountList = PostgreSQL.getAllAccounts();

            lb = obj as ListBox;
            Animal selectedAnimal = lb.SelectedItem as Animal;
            isCartEmpty = (Cart.Count == 0) ? true : false;

            if (Quantity == null) {
                MessageBox.Show("Please enter a valid quantity to add to your cart", "Invalid Quantity");
            } else {
             
                int purchAmt = int.Parse(Quantity);

                if (selectedAnimal == null) {
                    MessageBox.Show("Please select an animal before adding it to your cart", "Invalid Selection");
                } else if (selectedAnimal.Price == "Pricele$$") {
                    MessageBox.Show("This item cannot be purchased", "The Rock Says:");
                }  else {
                    if (isCartEmpty) { 
                        if ((int.Parse(selectedAnimal.Quantity) - purchAmt) < 0) {
                            MessageBox.Show($"Requested Quantity Exceeds the number of {selectedAnimal.Type}s in stock", "Invalid Quantity");
                        } else {
                            selectedAnimal.Quantity = (int.Parse(selectedAnimal.Quantity) - purchAmt).ToString();
                            selectedAnimal.PurchasedAmount = purchAmt.ToString();
                            Cart.Add(selectedAnimal as object);

                            TotalCost += double.Parse(selectedAnimal.Price.Replace("$", "")) * purchAmt;
                            TotalItem += double.Parse(selectedAnimal.PurchasedAmount);
                            CollectionViewSource.GetDefaultView(lb.ItemsSource).Refresh();

                            foreach (Account a in AccountList) {
                                if (a.id == LoggedInUser.id) { // CHANGE THIS TO ACCOUNT ID
                                    if (LoggedInUser.CartContent == null)
                                    {
                                        LoggedInUser.CartContent = new ObservableCollection<Animal>();
                                    }
                                    LoggedInUser.CartContent.Add(selectedAnimal);
                                    LoggedInUser.CartTotal = TotalCost.ToString();
                                    LoggedInUser.CartItems = TotalItem.ToString(); 
                                }
                            }
                            CollectionViewSource.GetDefaultView(selectedAnimal.Quantity).Refresh();
                            PostgreSQL.addShopper(LoggedInUser, selectedAnimal);
                        }
                    } else {
                        foreach(object o in Cart.ToList())
                        {
                            Animal amal = o as Animal;
                            if(amal.Type == selectedAnimal.Type)
                            {
                                MessageBox.Show("You already have that animal in your shopping cart. Click Review Order to change the quantity", "Oopsies");
                            } else
                            {
                                selectedAnimal.Quantity = (int.Parse(selectedAnimal.Quantity) - purchAmt).ToString();
                            selectedAnimal.PurchasedAmount = purchAmt.ToString();
                            Cart.Add(selectedAnimal as object);

                            TotalCost += double.Parse(selectedAnimal.Price.Replace("$", "")) * purchAmt;
                            TotalItem += double.Parse(selectedAnimal.PurchasedAmount);
                            CollectionViewSource.GetDefaultView(lb.ItemsSource).Refresh();

                            foreach (Account a in AccountList) {
                                if (a.id == LoggedInUser.id) { 
                                      
                                    LoggedInUser.CartContent.Add(selectedAnimal);
                                    LoggedInUser.CartTotal = TotalCost.ToString(); 
                                    LoggedInUser.CartItems = TotalItem.ToString(); 
                                }
                            }
                            CollectionViewSource.GetDefaultView(selectedAnimal.Quantity).Refresh();
                            PostgreSQL.addShopper(LoggedInUser, selectedAnimal);
                            }
                        }
                    }
                }
            }
        }

        private void ReviewOrder(object obj) {
            ShoppingCartVM scVM = new ShoppingCartVM(this, LoggedInUser);
            ShoppingCart sc = new ShoppingCart();
            sc.DataContext = scVM;
            sc.Show();
        }

        private void PlaceOrder(object obj) {
            PlaceOrderVM poVM = new PlaceOrderVM(this, LoggedInUser);
            PlaceOrder po = new PlaceOrder();
            po.DataContext = poVM;
            po.Show();
        }

        private void SearchPets(object o) {
            if(SearchText == null && SearchType != "All"){
                MessageBox.Show("Please enter a search critera");
            } else {

                stype = SearchType;
                stxt = SearchText;

                ShopperHome sh = new ShopperHome(LoggedInUser);
                closeWindows();
                sh.Show();
            }
        }

        private void closeWindows() {
            foreach (Window item in Application.Current.Windows) {
                if (item.DataContext == this) item.Close();
            }
        }

        public ICommand OpenCartCommand {
            get
            {
                if (_openCartEvent == null) _openCartEvent = new DelegateCommand(ReviewOrder);
                return _openCartEvent;
            }
        }
        DelegateCommand _openCartEvent;

        public ICommand AddToCartCommand {
            get {
                if (_addToCartEvent == null) _addToCartEvent = new DelegateCommand(AddToCartClicked);
                return _addToCartEvent;
            }
        }
        DelegateCommand _addToCartEvent;

        public ICommand PlaceOrderCommand {
            get {
                if (_placeOrderEvent == null) _placeOrderEvent = new DelegateCommand(PlaceOrder);
                return _placeOrderEvent;
            }
        }
        DelegateCommand _placeOrderEvent;

        public ICommand SearchCommand
        {
            get
            {
                if (_searchEvent == null) _searchEvent = new DelegateCommand(SearchPets);
                return _searchEvent;
            }
        }
        DelegateCommand _searchEvent;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
