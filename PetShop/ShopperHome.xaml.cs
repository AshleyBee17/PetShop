using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PetShop {
    public partial class ShopperHome : Window, INotifyPropertyChanged {

        Account LoggedInShopper;
   
        public ShopperHome(Account acct) {
            InitializeComponent();
            this.LoggedInShopper = acct;
            WelcomeMessage.Header = $"Welcome to the Pet Shop, {acct.FirstName}!";
            ShopperHomeVM shopperHomeVM = new ShopperHomeVM(LoggedInShopper);
            DataContext = shopperHomeVM;
           // MessageBox.Show("Welcome to the Pet Shop! Enjoy your new furry friend!","Hey there!");
        }

        // When the user logs back in, their cart will still show its contents from when they last logged out
        private void SaveClick(object sender, RoutedEventArgs e) {
            MessageBox.Show("Cart Contents have been saved","Message");
        }

        // Saves the current contents in their cart to a file
        private void SaveAsClick(object sender, RoutedEventArgs e) {

            string header = $"Receipt For {LoggedInShopper.FirstName} {LoggedInShopper.LastName}\n";
            List<string> receipt = new List<string>();
            int cartTotal = 0;

            foreach(Animal a in LoggedInShopper.CartContent) {
                int petTotal = int.Parse(a.Price) * int.Parse(a.Quantity);
                cartTotal =+ petTotal;
                receipt.Add($"Pet: {a.Type} {Environment.NewLine}Price: {a.Price} {Environment.NewLine}Quantity: {a.Quantity} {Environment.NewLine}---------- Item Total: ${petTotal}");
            }

            SaveFileDialog dialog = new SaveFileDialog() {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (dialog.ShowDialog() == true) {
                File.AppendAllText(dialog.FileName, $"{header} {Environment.NewLine}");
                foreach (string s in receipt) {
                    File.AppendAllText(dialog.FileName, s);
                }
                File.AppendAllText(dialog.FileName, $"{Environment.NewLine} Your cart total: {cartTotal}");
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
