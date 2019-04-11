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
    public partial class SellerEditPet : Window { 
        string petType;
        string petSize;   
        string petPrice;
        string petQuantity;
        string petAge;
    
        public SellerEditPet() {
            InitializeComponent();
        }

        private void SavePetToDatabase(object sender, RoutedEventArgs e) {
            petType = PetType.Text;
            petSize = PetSize.Text;
            petAge = PetAge.Text;
            petQuantity = PetQuantity.Text;
            petPrice = PetPrice.Text;

            Animal a = new Animal(petType, petAge, petSize, petQuantity, petPrice, null);

            // Save to database here?
        }
    }
}
