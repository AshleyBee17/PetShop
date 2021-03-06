﻿using System;
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
    public partial class SellerAddPet : Window {

        string petType;
        string petSize;   
        string petPrice;
        string petQuantity;
        string petAge;
        string petZip;

        Account LoggedInSeller;

        public SellerAddPet(Account acct) { 
            InitializeComponent();
            this.LoggedInSeller = acct;
        }

        private void AddPetToDatabase(object sender, RoutedEventArgs e) {
            petType = PetTypeEntry.Text;
            petSize = PetSizeEntry.Text;
            petAge =  PetAgeEntry.Text;
            petQuantity =  PetQuantityEntry.Text;
            petPrice =  PetPriceEntry.Text;
            petZip = PetZipEntry.Text;

            if (CheckEntries()) {
                MessageBox.Show("Adding to the database...");
                Animal a = new Animal(LoggedInSeller.id, petType, petAge, petSize, petQuantity, petPrice, petZip);
                PostgreSQL.addPet(a);
                PostgreSQL.addSeller(LoggedInSeller, a);
                SellerHome sh = new SellerHome(LoggedInSeller);
                sh.Show();
                this.Close();
            }    
        }

        private bool CheckEntries() {

            bool ageValid, quanValid, zipValid, priceValid;

            ageValid = string.IsNullOrWhiteSpace(PetAgeEntry.Text) || int.Parse(PetAgeEntry.Text) == 0 ? false : true;
            PetAgeEntry.BorderBrush = ageValid ? PetAgeEntry.BorderBrush = Brushes.Gray : PetAgeEntry.BorderBrush = Brushes.Red;

            quanValid = string.IsNullOrWhiteSpace(PetQuantityEntry.Text) || int.Parse(PetQuantityEntry.Text) == 0 ? false : true;
            PetQuantityEntry.BorderBrush = quanValid ? PetQuantityEntry.BorderBrush = Brushes.Gray : PetQuantityEntry.BorderBrush = Brushes.Red;

            priceValid = string.IsNullOrWhiteSpace(PetPriceEntry.Text) || int.Parse(PetPriceEntry.Text) == 0 ? false : true;
            PetPriceEntry.BorderBrush = priceValid ? PetPriceEntry.BorderBrush = Brushes.Gray : PetPriceEntry.BorderBrush = Brushes.Red;

            zipValid = string.IsNullOrWhiteSpace(PetZipEntry.Text) || int.Parse(PetZipEntry.Text) == 0 ? false : ValidateZip(PetZipEntry.Text);
            PetZipEntry.BorderBrush = zipValid ? PetZipEntry.BorderBrush = Brushes.Gray : PetZipEntry.BorderBrush = Brushes.Red;

            return ageValid && quanValid && zipValid && priceValid;
        }

        private bool ValidateZip(string str) {
            return (str.Where(a => char.IsDigit(a)).Count() == str.Length && str.Length == 5);
        }
    }
}
