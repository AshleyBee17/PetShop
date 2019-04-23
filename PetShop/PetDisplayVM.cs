using System;
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
    public class PetDisplayVM : INotifyPropertyChanged {

        public static ObservableCollection<Animal> AnimalCollection { get; set; }

        private Animal _selectedAnimal;
        public Animal SelectedAnimal {
            get { return _selectedAnimal; }
            set {
                _selectedAnimal = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedAnimal"));
            }
        }
        
        public PetDisplayVM() {
            if (LoginWindow.AccountToLogin.Type == "Seller") {
                ReadInDataFromSeller(LoginWindow.AccountToLogin.id);
            } else if (ShopperHomeVM.stype == "Age") {
                AnimalCollection = PostgreSQL.searchByAge(ShopperHomeVM.stxt);
            } else if (ShopperHomeVM.stype == "Type") {
                AnimalCollection = PostgreSQL.searchByType(ShopperHomeVM.stxt);
            } else if (ShopperHomeVM.stype == "Price") {
                AnimalCollection = PostgreSQL.searchByPrice(ShopperHomeVM.stxt);
            } else if (ShopperHomeVM.stype == "Zipcode") {
                AnimalCollection = PostgreSQL.searchByZip(ShopperHomeVM.stxt);
            }
            else ReadInAllData();
        }

      

        private void ReadInDataFromSeller(int id) {

            AnimalCollection = PostgreSQL.getOwnersPets(id);

            if (AnimalCollection == null){
                AnimalCollection = new ObservableCollection<Animal>();
            }
        }

        private void ReadInAllData() {

            AnimalCollection = PostgreSQL.getAllPets();

            if(AnimalCollection == null){
                AnimalCollection = new ObservableCollection<Animal>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
