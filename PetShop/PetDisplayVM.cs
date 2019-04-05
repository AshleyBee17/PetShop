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
        string path = "animals.xml";

        private Animal _selectedAnimal;
        public Animal SelectedAnimal {
            get { return _selectedAnimal; }
            set {
                _selectedAnimal = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedAnimal"));
            }
        }
        
        public PetDisplayVM() {
            ReadInDataFromXML();
        }

        private void ReadInDataFromXML() {
            using (FileStream readStream = new FileStream(path,  FileMode.Open, FileAccess.Read)) {
                AnimalCollection = serializer.Deserialize(readStream) as ObservableCollection<Animal>;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
