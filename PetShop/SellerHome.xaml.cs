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
        public ListBox lb;

        public SellerHome(Account acct) {
            InitializeComponent();
            this.LoggedInSeller = acct;
            WelcomeMessage.Header = $"Welcome to the Pet Shop, {acct.FirstName}!";
        }

        private void AddPetClicked(object sender, RoutedEventArgs e) {
            MessageBox.Show("Add pet window");
            SellerAddPet sellerAddPet = new SellerAddPet();
            sellerAddPet.Show();
            //this.Close();
        }

        private void RemoveClicked(object obj)
        {

            lb = obj as ListBox;
            Animal selectedAnimal = lb.SelectedItem as Animal;

            //Remove Animal from list and from DB
        }

        private void EditClicked(object obj)
        {
            lb = obj as ListBox;
            Animal selectedAnimal = lb.SelectedItem as Animal;

            MessageBox.Show("Edit pet window");
            SellerEditPet sellerEditPet = new SellerEditPet();
            sellerEditPet.Show();
            //this.Close();
        }

        private void LogOut(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        public ICommand RemoveCommand
        {
            get
            {
                if (_removeEvent == null) _removeEvent = new DelegateCommand(RemoveClicked);
                return _removeEvent;
            }
        }
        DelegateCommand _removeEvent;

        public ICommand EditCommand
        {
            get
            {
                if (_editEvent == null) _editEvent = new DelegateCommand(EditClicked);
                return _editEvent;
            }
        }
        DelegateCommand _editEvent;
    }
}
