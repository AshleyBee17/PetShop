using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class SellerHome : Window, INotifyPropertyChanged
    {

        Account LoggedInSeller;

        public SellerHome(Account acct) {
            InitializeComponent();
            this.LoggedInSeller = acct;
            WelcomeMessage.Header = $"Welcome to the Pet Shop, {acct.FirstName}!";
            SellerHomeVM sellerHomeVM = new SellerHomeVM(LoggedInSeller);
            DataContext = sellerHomeVM;
          
        }
        /*
        private void AddPetClicked(object sender, RoutedEventArgs e)
        {
            SellerAddPet sellerAddPet = new SellerAddPet();
            sellerAddPet.Show();
            //this.Close();
        }

        private void RemovePetClicked(object sender, RoutedEventArgs e)
        {
            SellerRemovePet sellerRemovePet = new SellerRemovePet();
            sellerRemovePet.Show();
            //this.Close();
        }

        private void EditPet_Click(object sender, RoutedEventArgs e)
        {
            SellerEditPet sellerEditPet = new SellerEditPet();
            sellerEditPet.Show();
            //this.Close();
        }
        */
        private void LogOut(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
