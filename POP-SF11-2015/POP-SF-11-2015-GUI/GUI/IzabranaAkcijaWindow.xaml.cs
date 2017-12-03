using POP_SF11_2015.Model;
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
using System.Windows.Shapes;

namespace POP_SF_11_2015_GUI.GUI
{
    /// <summary>
    /// Interaction logic for IzabranaAkcijaWindow.xaml
    /// </summary>
    public partial class IzabranaAkcijaWindow : Window
    {
        private Korisnik ulogovaniKorisnik;
        private ICollectionView view;

        public IzabranaAkcijaWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            //OsveziPrikaz();
            this.ulogovaniKorisnik = ulogovaniKorisnik;

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
            view.Filter = NamestajFilter;
            //dgNamestaj.ItemsSource = Projekat.Instance.Namestaj;
            dgNamestajAkcija.ItemsSource = view;
            dgNamestajAkcija.IsSynchronizedWithCurrentItem = true;
            dgNamestajAkcija.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private bool NamestajFilter(object item)
        {
            //return ((Namestaj)item).Obrisan == false;
            Namestaj nam = item as Namestaj;
            return !nam.Obrisan;
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            AkcijeWindow aw = new AkcijeWindow(this.ulogovaniKorisnik);
            aw.Show();
            this.Close();
        }

        private void dgNamestajAkcija_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;

            }
            if ((string)e.Column.Header == "TipNamestajaId")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Cena")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Id")
            {
                e.Column.Width = 100;
            }
            if ((string)e.Column.Header == "Naziv")
            {
                e.Column.Width = 500;
            }
            if ((string)e.Column.Header == "AkcijskaCena")
            {
                e.Column.Header = "Akcijska Cena";
                e.Column.Width = 350;
            }
            if ((string)e.Column.Header == "Sifra")
            {
                e.Column.Width = 300;
            }
            if ((string)e.Column.Header == "KolicinaUMagacinu")
            {
                e.Column.Header = "Kolicina";
                e.Column.Width = 220;
            }
            if ((string)e.Column.Header == "TipNamestaja")
            {
                e.Column.Header = "Tip Namestaja";
                e.Column.Width = 320;
            }
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnShop_Click(object sender, RoutedEventArgs e)
        {

            var praznaProdaja = new Prodaja()
            {

            };

            var prodajaProzor = new ProdajaEditWindow(praznaProdaja, ProdajaEditWindow.TipOperacije.DODAVANJE);
            prodajaProzor.ShowDialog();

            //ProdajaEditWindow pw = new ProdajaEditWindow(Prodaja prodaja, this.operacija);
            //pw.Show();
            //this.Close();
        }
    }
}
