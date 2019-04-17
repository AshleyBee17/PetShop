using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PetShop
{
    class SellerHomeVM : INotifyPropertyChanged {

        Account LoggedInUser;
        public ListBox lb;

        public SellerHomeVM(Account acct) {
            this.LoggedInUser = acct;
        }

        private void AddPet(object o) {
            SellerAddPet sellerAddPet = new SellerAddPet();
            sellerAddPet.Show();
            //this.Close();
        }

        private void DeletePet(object o) {

            lb = o as ListBox;
            Animal selectedAnimal = lb.SelectedItem as Animal;

            if(selectedAnimal == null) {
                MessageBox.Show("Please select an animal before removing it", "Invalid Selection");
            } else {
                MessageBox.Show("Animal removed from the database");
                CollectionViewSource.GetDefaultView(lb.ItemsSource).Refresh();
                //ADD DB FUNCTIONALITY
            }
        }

        private void EditPet(object o) {

            lb = o as ListBox;
            Animal selectedAnimal = lb.SelectedItem as Animal;

            if (selectedAnimal == null)
            {
                MessageBox.Show("Please select an animal before editing it", "Invalid Selection");
            } else {
                SellerEditPet sellerEditPet = new SellerEditPet(selectedAnimal);
                sellerEditPet.Show();
                //this.Close();
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (_addEvent == null) _addEvent = new DelegateCommand(AddPet);
                return _addEvent;
            }
        }
        DelegateCommand _addEvent;

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteEvent == null) _deleteEvent = new DelegateCommand(DeletePet);
                return _deleteEvent;
            }
        }
        DelegateCommand _deleteEvent;

        public ICommand EditCommand
        {
            get
            {
                if (_editEvent == null) _editEvent = new DelegateCommand(EditPet);
                return _editEvent;
            }
        }
        DelegateCommand _editEvent;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
