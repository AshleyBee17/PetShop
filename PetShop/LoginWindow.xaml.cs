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
        Account Account = new Account();
        Account AccountToLogin = new Account();

        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Account>));
        string path = "userAccounts.xml";

        public LoginWindow() {
            InitializeComponent();
            
            PostgreSQL.readPetsFromDB();

            try {
                ReadAccountsFromMemory();
            } catch {
                Console.WriteLine("File could not be opened to be read.");
                MessageBox.Show($"{path} failed to open because it is empty or does not exist. The file has now been created for you.","File Failed to Open");
            }
        }

        public LoginWindow(Account acct) {
            InitializeComponent();
            this.Account = acct;
            if(AccountList == null){
                AccountList = new ObservableCollection<Account>();
            }
            this.AccountList.Add(Account);
            
            using (FileStream writeStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite)) {
                serializer.Serialize(writeStream, AccountList);
            }
        }

        private void ReadAccountsFromMemory() {
            using (FileStream readStream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                AccountList = serializer.Deserialize(readStream) as ObservableCollection<Account>;
            }
        }

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

            if (ValidateAllEntries()) {
                Account acct = AccountList.FirstOrDefault(a => a.Username == UsernameEntry.Text);

                if (acct == null) {
                    Verify.Visibility = Visibility.Visible;
                    return false;
                } else {
                    if (acct.Password == PasswordEntry.Password) {
                        AccountToLogin = acct;
                        Verify.Visibility = Visibility.Hidden;
                        return true;
                    } else {
                        Verify.Visibility = Visibility.Visible;
                        return false;
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
