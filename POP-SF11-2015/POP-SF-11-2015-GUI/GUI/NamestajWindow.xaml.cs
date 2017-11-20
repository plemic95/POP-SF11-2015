using POP_SF_11_2015_GUI.GUI;
using POP_SF11_2015.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP_SF_11_2015_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {

        private Korisnik ulogovaniKorisnik;

        public NamestajWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            OsveziPrikaz();
            this.ulogovaniKorisnik = ulogovaniKorisnik;

          //  if (ulogovaniKorisnik.TipKorisnika.Oznaka != 1)
          //  {
          //      bKorisnici.IsEnabled = false;
          //  }

        }

        //public MainWindow()
        //{
        //    InitializeComponent();
        //
        //    OsveziPrikaz();
        //}

        private void OsveziPrikaz()
        {

            lbNamestaj.Items.Clear();

            foreach (var namestaj in Projekat.Instance.Namestaj)
            {
                if(namestaj.Obrisan == false)
                {
                    lbNamestaj.Items.Add(namestaj);
                }
            }

            lbNamestaj.SelectedIndex = 0;
        }

        private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var prazanNamestaj = new Namestaj()
            {
                Naziv = ""
            };

            var namestajProzor = new NamestajEditWindow(prazanNamestaj, NamestajEditWindow.TipOperacije.DODAVANJE);
            namestajProzor.ShowDialog();

            OsveziPrikaz();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnIzmeniNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var izabraniNamestaj = (Namestaj)lbNamestaj.SelectedItem;

            var namestajProzor = new NamestajEditWindow(izabraniNamestaj, NamestajEditWindow.TipOperacije.IZMENA);
            namestajProzor.ShowDialog();

            OsveziPrikaz();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            var izabraniNamestaj = (Namestaj)lbNamestaj.SelectedItem;
            var listaNamestaja = Projekat.Instance.Namestaj;
            //Namestaj izabraniNamestaj = (Namestaj)lbNamestaj.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: { izabraniNamestaj.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Namestaj namestaj = null;
                foreach (var n in listaNamestaja)
                {
                    if (n.Id == izabraniNamestaj.Id)
                    {
                        namestaj = n;
                    }
                }
                namestaj.Obrisan = true;

                Projekat.Instance.Namestaj = listaNamestaja;

                OsveziPrikaz();
            }
        }
    }
}
