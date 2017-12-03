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
        private Namestaj namestaj;
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

            //if (cbObrisan.IsChecked == true)
            //{
            //   namestaj.Obrisan = false;
            //   view.Refresh();
            //}
        }
        private bool NamestajFilter(object item)
            {
                Namestaj nam = item as Namestaj;
                return !nam.Obrisan;
            }
            //return ((Namestaj)item).Obrisan == false;

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
            Namestaj izabraniNamestaj = view.CurrentItem as Namestaj;

            if (izabraniNamestaj != null)
            {
                Namestaj old = (Namestaj)izabraniNamestaj.Clone();
                NamestajEditWindow nw = new NamestajEditWindow(izabraniNamestaj, NamestajEditWindow.TipOperacije.IZMENA);
                if (nw.ShowDialog() != true)
                {
                    int index = Projekat.Instance.Namestaj.IndexOf(izabraniNamestaj);
                    Projekat.Instance.Namestaj[index] = old;
                }
            }
            //var izabraniNamestaj = (Namestaj)dgNamestaj.SelectedItem;

            //var namestajProzor = new NamestajEditWindow(izabraniNamestaj, NamestajEditWindow.TipOperacije.IZMENA);
            //namestajProzor.ShowDialog();

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
            if ((string)e.Column.Header == "AkcijskaCena")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Id")
            {
                e.Column.Width = 100;
            }
            if ((string)e.Column.Header == "Naziv")
            {
                e.Column.Width = 550;
            }
            if ((string)e.Column.Header == "Cena")
            {
                e.Column.Width = 250;
            }
            if ((string)e.Column.Header == "Sifra")
            {
                e.Column.Width = 300;
            }
            if ((string)e.Column.Header == "KolicinaUMagacinu")
            {
                e.Column.Header = "Kolicina";
                e.Column.Width = 240;
            }
            if ((string)e.Column.Header == "TipNamestaja")
            {
                e.Column.Header = "Tip Namestaja";
                e.Column.Width = 350;
            }
        }

        private void btnTipNamestaja_Click(object sender, RoutedEventArgs e)
        {
            var prazanTipNamestaja = new TipNamestaja()
            {
                Naziv = ""
            };

            var tipNamestajaProzor = new TipNamestajaEditWindow(prazanTipNamestaja, TipNamestajaEditWindow.TipOperacije.DODAVANJE);
            tipNamestajaProzor.ShowDialog();
        }
    }
}
