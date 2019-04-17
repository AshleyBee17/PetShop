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
        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Animal>));
        //string path = "animals.xml";

        private Animal _selectedAnimal;
        public Animal SelectedAnimal {
            get { return _selectedAnimal; }
            set {
                _selectedAnimal = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedAnimal"));
            }
        }
        
        public PetDisplayVM() {
            ReadInData();
        }

        private void ReadInData() {

            // READ FROM DATABASE HERE
            AnimalCollection = PostgreSQL.readPetsFromDB();
            PostgreSQL.searchByAge("1");

            if(AnimalCollection == null){
                AnimalCollection = new ObservableCollection<Animal>();
            }

           //AnimalCollection.Add(new Animal("Dog", "1", "Small", "20", "50","33647", null));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
