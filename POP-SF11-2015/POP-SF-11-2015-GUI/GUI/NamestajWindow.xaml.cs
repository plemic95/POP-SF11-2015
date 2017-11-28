using POP_SF_11_2015_GUI.GUI;
using POP_SF11_2015.Model;
using POP_SF11_2015.Utils;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP_SF_11_2015_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {
        private ICollectionView view;
        private Korisnik ulogovaniKorisnik;

        public NamestajWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            //OsveziPrikaz();
            this.ulogovaniKorisnik = ulogovaniKorisnik;

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
            view.Filter = NamestajFilter;
            //dgNamestaj.ItemsSource = Projekat.Instance.Namestaj;
            dgNamestaj.ItemsSource = view;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
            private bool NamestajFilter(object item)
            {
            //return ((Namestaj)item).Obrisan == false;
                Namestaj nam = item as Namestaj;
                return !nam.Obrisan;
            }

          //  if (ulogovaniKorisnik.TipKorisnika.Oznaka != 1)
          //  {
          //      bKorisnici.IsEnabled = false;
          //  }


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
                        view.Refresh();
                        break;
                    }
                }

                //OsveziPrikaz();
            }

            GenericSerializer.Serialize("namestaj.xml", Projekat.Instance.Namestaj);
        }

        private void dgNamestaj_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "TipNamestajaId")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "KolicinaUMagacinu")
            {
                e.Column.Header = "Kolicina";
            }
            if ((string)e.Column.Header == "TipNamestaja")
            {
                e.Column.Header = "Tip Namestaja";
            }
        }
    }
}
