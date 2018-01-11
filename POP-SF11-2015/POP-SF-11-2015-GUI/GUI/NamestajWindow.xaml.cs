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
        private CollectionViewSource cvs;
        private Korisnik ulogovaniKorisnik;

        public NamestajWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            //OsveziPrikaz();
            this.ulogovaniKorisnik = ulogovaniKorisnik;

            cvs = new CollectionViewSource();
            cvs.Source = Projekat.Instance.Namestaji;

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaji);

            cvs.View.Filter = NamestajFilter;
            //dgNamestaj.ItemsSource = Projekat.Instance.Namestaj;
            dgNamestaj.ItemsSource = cvs.View;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            if (ulogovaniKorisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                btnDodajNamestaj.Visibility = Visibility.Hidden;
                btnIzmeniNamestaj.Visibility = Visibility.Hidden;
                btnObrisi.Visibility = Visibility.Hidden;
            }



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
                TipNamestajaId = 1
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
            Namestaj izabraniNamestaj = (Namestaj)dgNamestaj.SelectedItem;

            if (izabraniNamestaj != null)
            {
                Namestaj old = (Namestaj)izabraniNamestaj.Clone();
                NamestajEditWindow nw = new NamestajEditWindow(izabraniNamestaj, NamestajEditWindow.TipOperacije.IZMENA);
                if (nw.ShowDialog() != true)
                {
                    int index = Projekat.Instance.Namestaji.IndexOf(izabraniNamestaj);
                    Projekat.Instance.Namestaji[index] = old;
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
                foreach (var n in Projekat.Instance.Namestaji)
                {
                    if (n.Id == izabraniNamestaj.Id)
                    {
                        Namestaj.Delete(n);
                        cvs.View.Refresh();
                        break;
                    }
                }

                //OsveziPrikaz();
            }

           // GenericSerializer.Serialize("namestaj.xml", Projekat.Instance.Namestaji);
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
            if ((string)e.Column.Header == "IDAkcije")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Id")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Akcija")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Naziv")
            {
                e.Column.Width = 550;
            }
            if ((string)e.Column.Header == "Cena")
            {
                e.Column.Width = 350;
            }
            if ((string)e.Column.Header == "Sifra")
            {
                e.Column.Width = 200;
            }
            if ((string)e.Column.Header == "KolicinaUMagacinu")
            {
                e.Column.Header = "Kolicina";
                e.Column.Width = 240;
            }
            if ((string)e.Column.Header == "TipNamestaja")
            {
                e.Column.Header = "Tip Namestaja";
                e.Column.Width = 450;
            }
        }

        private void btnTipNamestaja_Click(object sender, RoutedEventArgs e)
        {

            TipNamestajaWindow tnw = new TipNamestajaWindow(this.ulogovaniKorisnik);
            tnw.Show();
            this.Close();
            //var prazanTipNamestaja = new TipNamestaja()
            //{
            //    Naziv = ""
            //};

            //var tipNamestajaProzor = new TipNamestajaEditWindow(prazanTipNamestaja, TipNamestajaEditWindow.TipOperacije.DODAVANJE);
            //tipNamestajaProzor.ShowDialog();
        }


        private void MyFilter(object sender, FilterEventArgs e)
        {
            Namestaj n = e.Item as Namestaj;
            if (n != null)
            {
                e.Accepted = n.Naziv.ToLower().Contains(tbPretraga.Text.ToLower()) || n.Sifra.ToLower().Contains(tbPretraga.Text.ToLower()) || n.TipNamestaja.Naziv.ToLower().Contains(tbPretraga.Text.ToLower());
            }
        }

        private void tbPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            cvs.Filter += new FilterEventHandler(MyFilter);
        }

    }
}
