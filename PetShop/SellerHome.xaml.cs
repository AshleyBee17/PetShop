using System;
using System.Collections.Generic;
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
    public partial class SellerHome : Window {

        Account LoggedInSeller;

        public SellerHome(Account acct)
        {
            InitializeComponent();
            this.LoggedInSeller = acct;
            WelcomeMessage.Header = $"Welcome to the Pet Shop, {acct.FirstName}!";
        }

        private void AddPetClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add pet window");
            SellerAddPet sellerAddPet = new SellerAddPet();
            sellerAddPet.Show();
            //this.Close();
        }

        private void RemovePetClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete pet window");
            SellerRemovePet sellerRemovePet = new SellerRemovePet();
            sellerRemovePet.Show();
            //this.Close();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
