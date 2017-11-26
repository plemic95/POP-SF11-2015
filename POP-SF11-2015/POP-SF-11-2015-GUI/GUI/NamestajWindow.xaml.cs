using POP_SF_11_2015_GUI.GUI;
using POP_SF11_2015.Model;
using POP_SF11_2015.Utils;
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
            //OsveziPrikaz();
            this.ulogovaniKorisnik = ulogovaniKorisnik;

            dgNamestaj.ItemsSource = Projekat.Instance.Namestaj;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;

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

        //private void OsveziPrikaz()
        //{

        //    dgNamestaj.Items.Clear();

        //    foreach (var namestaj in Projekat.Instance.Namestaj)
        //    {
        //        if(namestaj.Obrisan == false)
        //        {
        //            dgNamestaj.Items.Add(namestaj);
        //        }
        //    }

        //    dgNamestaj.SelectedIndex = 0;
        //}

        private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var prazanNamestaj = new Namestaj()
            {
                Naziv = ""
            };

            var namestajProzor = new NamestajEditWindow(prazanNamestaj, NamestajEditWindow.TipOperacije.DODAVANJE);
            namestajProzor.ShowDialog();

            //OsveziPrikaz();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(this.ulogovaniKorisnik);
            mw.Show();
            this.Close();
        }

        private void btnIzmeniNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var izabraniNamestaj = (Namestaj)dgNamestaj.SelectedItem;

            var namestajProzor = new NamestajEditWindow(izabraniNamestaj, NamestajEditWindow.TipOperacije.IZMENA);
            namestajProzor.ShowDialog();

            //OsveziPrikaz();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            var izabraniNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            //Namestaj izabraniNamestaj = (Namestaj)lbNamestaj.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: { izabraniNamestaj.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var n in Projekat.Instance.Namestaj)
                {
                    if (n.Id == izabraniNamestaj.Id)
                    {
                        n.Obrisan = true;
                        break;
                    }
                }

                //OsveziPrikaz();
            }

            GenericSerializer.Serialize("namestaj.xml", Projekat.Instance.Namestaj);
        }
    }
}
