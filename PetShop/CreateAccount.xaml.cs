using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PetShop { 
    public partial class CreateAccount : Window {

        //ObservableCollection<Account> AccountList;
        
        public CreateAccount(){
            InitializeComponent();
        }
        /*
        public CreateAccount() {
            this.AccountList = acctList;
            InitializeComponent();
        }*/

        private void Submit(object sender, RoutedEventArgs e) {

            ObservableCollection<Account> acct = PostgreSQL.searchByUsername(UsernameEntry.Text);
            if(acct.Count != 0) MessageBox.Show("Username already exists. Please choose another username!");
            if (ValidateEntries() && acct.Count ==0) {

                Account Account = new Account();

                Account.FirstName = FirstNameEntry.Text;
                Account.LastName = LastNameEntry.Text;
                Account.Telephone = PhoneEntry.Text;
                Account.Username = UsernameEntry.Text;
                Account.Password = PasswordEntry.Password;
                //Account.ZipCode = ZipEntry.Text;
                Account.CartItems = "0";
                Account.CartTotal = "0";
                Account.CartContent = new ObservableCollection<Animal>();
                Account.PreviousPurchases = new ObservableCollection<Animal>();
                

                if (Shopper.IsChecked == true) {
                    Account.Type = Shopper.Content.ToString();
                    PostgreSQL.addAccount(Account);
                    //PostgreSQL.addShopper(Account, null);
                    ZipCodePanel.Visibility = Visibility.Hidden;
                    openLogin(Account);
                } else if (Seller.IsChecked == true) {
                    Account.Type = Seller.Content.ToString();
                    PostgreSQL.addAccount(Account);
                    //PostgreSQL.addSeller(Account, null);
                    openLogin(Account);
                }

                
            }
        }

        private void openLogin(Account a) {
                LoginWindow loginWindow = new LoginWindow(a);
                loginWindow.Show();
                this.Close();
        }

        bool validateZip() {
            bool validateZip;

            validateZip = string.IsNullOrWhiteSpace(ZipEntry.Text) ? false : ValidateZip(ZipEntry.Text);
            ZipEntry.BorderBrush = validateZip ? ZipEntry.BorderBrush = Brushes.Gray : ZipEntry.BorderBrush = Brushes.Red;
                
            return validateZip;
        }

        bool ValidateEntries () {
            bool validateFirstName, validateLastName, validateUsername, validatePassword, validatePhone;
            bool validateAcctType;

            validateFirstName = string.IsNullOrWhiteSpace(FirstNameEntry.Text) ? false : ValidateLetters(FirstNameEntry.Text);
            FirstNameEntry.BorderBrush = validateFirstName ? FirstNameEntry.BorderBrush = Brushes.Gray : FirstNameEntry.BorderBrush = Brushes.Red;

            validateLastName = string.IsNullOrWhiteSpace(LastNameEntry.Text) ? false : ValidateLetters(LastNameEntry.Text);
            LastNameEntry.BorderBrush = validateLastName ? LastNameEntry.BorderBrush = Brushes.Gray : LastNameEntry.BorderBrush = Brushes.Red;

            validateUsername = string.IsNullOrWhiteSpace(UsernameEntry.Text) ? false : ValidateNoSpace(UsernameEntry.Text);
            UsernameEntry.BorderBrush = validateUsername ? UsernameEntry.BorderBrush = Brushes.Gray : UsernameEntry.BorderBrush = Brushes.Red;

            validatePassword = string.IsNullOrWhiteSpace(PasswordEntry.Password) ? false : ValidateNoSpace(PasswordEntry.Password);
            PasswordEntry.BorderBrush = validatePassword ? PasswordEntry.BorderBrush = Brushes.Gray : PasswordEntry.BorderBrush = Brushes.Red;

           // validateZip = string.IsNullOrWhiteSpace(ZipEntry.Text) ? false : ValidateNum(ZipEntry.Text);
           // ZipEntry.BorderBrush = validateZip ? ZipEntry.BorderBrush = Brushes.Gray : ZipEntry.BorderBrush = Brushes.Red;

            validatePhone = string.IsNullOrWhiteSpace(PhoneEntry.Text) ? false : ValidateEmailPhone(PhoneEntry.Text);
            PhoneEntry.BorderBrush = validatePhone ? PhoneEntry.BorderBrush = Brushes.Gray : PhoneEntry.BorderBrush = Brushes.Red;

            if(Shopper.IsChecked == true || Seller.IsChecked == true) {
                validateAcctType = true;
            } else {
                validateAcctType = false;
            }
            RadioLabel.BorderBrush = validateAcctType ? RadioLabel.Foreground = Brushes.Black : RadioLabel.Foreground = Brushes.Red;

            return (validateFirstName && validateLastName && validateUsername && validatePassword && validatePhone
                 && validateAcctType);
        }

        private bool ValidateLetters(string str){
            return str.Where(a => char.IsLetter(a)).Count() == str.Length;
        }

        private bool ValidateNoSpace(string str){
            return (str.Where(a => char.IsLetterOrDigit(a)).Count() == str.Length) || (str.Where(a => char.IsLetter(a)).Count() == str.Length);
        }

        private bool ValidateEmailPhone(string str) {
            return !(str.Where(a => char.IsLetterOrDigit(a)).Count() == str.Length);
        }

        private bool ValidateNum(string str) {
            return (str.Where(a => char.IsDigit(a)).Count() == str.Length);
        }

        private bool ValidateZip(string str) {
            return (str.Where(a => char.IsDigit(a)).Count() == str.Length && str.Length == 5);
        }
    }
}
