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
            //PetDisplayVM.AnimalCollection = PostgreSQL.getOwnersPets(0);
            //PetDisplayVM vm = new PetDisplayVM();
           // vm.AnimalCollection = PostgreSQL.getOwnersPets(0);
            SellerHomeVM sellerHomeVM = new SellerHomeVM(LoggedInSeller);
            DataContext = sellerHomeVM;
          
        }
       
        private void LogOut(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
