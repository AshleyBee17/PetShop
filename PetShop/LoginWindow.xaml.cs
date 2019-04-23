using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace PetShop {
    public partial class LoginWindow : Window {

        ObservableCollection<Account> AccountList { get; set; }
        ObservableCollection<Account> SellersList { get; set; }
        ObservableCollection<Account> ShoppersList { get; set; }
        Account Account = new Account();
        public static Account AccountToLogin = new Account();


        public LoginWindow() {
            InitializeComponent();
            /*
            try {
                ReadInAllaData();
            } catch {
                Console.WriteLine("Database could not be opened to be read.");
                MessageBox.Show("Database could not be opened to be read.");
            }
            
            */
        }

        public LoginWindow(Account acct) {
            InitializeComponent();
            this.Account = acct;
            if(AccountList == null){
                AccountList = new ObservableCollection<Account>();
            }
            this.AccountList.Add(Account);
        }
        /*
        private void ReadInAllaData()
        {

            AccountList = PostgreSQL.getAllAccounts();
            SellersList = PostgreSQL.getAllSellers();
            ShoppersList = PostgreSQL.getAllShoppers();

            if (AccountList == null)
            {
                AccountList = new ObservableCollection<Account>();
            }

            if (SellersList == null)
            {
                SellersList = new ObservableCollection<Account>();
            }

            if (ShoppersList == null)
            {
                ShoppersList = new ObservableCollection<Account>();
            }
        }
        */
        private void LogIn(object sender, RoutedEventArgs e) {
            if (CheckUsernamePassword()) {
                if (AccountToLogin.Type == "Seller") {
                    SellerHome sellerHome = new SellerHome(AccountToLogin);
                    sellerHome.Show();
                    this.Close();
                } else if (AccountToLogin.Type == "Shopper") {
                    ShopperHome shopperHome = new ShopperHome(AccountToLogin);
                    shopperHome.Show();
                    this.Close();
                }
            }
        }

        private void CreateAccount(object sender, RoutedEventArgs e) {
            CreateAccount createAccount = new CreateAccount();
            createAccount.Show();
            this.Close();
        }

        private bool CheckUsernamePassword() {

            ObservableCollection<Account> AccountListTemp1 = new ObservableCollection<Account>();
            ObservableCollection<Account> AccountListTemp2 = new ObservableCollection<Account>();

            if (ValidateAllEntries()) {
                AccountListTemp1 = PostgreSQL.searchByUsername(UsernameEntry.Text);

                if (AccountListTemp1 == null) {
                    Verify.Visibility = Visibility.Visible;
                    return false;
                } else {
                    AccountListTemp2 = PostgreSQL.validateUsernamePassword(UsernameEntry.Text, PasswordEntry.Password);
                    if (AccountListTemp2.Count == 0) {
                        Verify.Visibility = Visibility.Visible;
                        return false;
                        
                    } else {
                        AccountToLogin = AccountListTemp2.FirstOrDefault(a => a.Username == UsernameEntry.Text);
                        Verify.Visibility = Visibility.Hidden;
                        return true;
                    }
                }
            }     
            return false;
        }
         
        private bool ValidateAllEntries() {

            bool usernameValid, passwordValid;

            usernameValid = string.IsNullOrWhiteSpace(UsernameEntry.Text) ? false : true;
            UsernameEntry.BorderBrush = usernameValid ? UsernameEntry.BorderBrush = Brushes.Gray : UsernameEntry.BorderBrush = Brushes.Red;
            
            passwordValid = string.IsNullOrWhiteSpace(PasswordEntry.Password) ? false : true;
            PasswordEntry.BorderBrush = passwordValid ? PasswordEntry.BorderBrush = Brushes.Gray : PasswordEntry.BorderBrush = Brushes.Red;

            return (usernameValid && passwordValid);
        }

    }
}
