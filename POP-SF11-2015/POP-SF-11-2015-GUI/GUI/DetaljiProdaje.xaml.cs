using POP_SF_11_2015_GUI.Model;
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
    /// Interaction logic for DetaljiProdaje.xaml
    /// </summary>
    public partial class DetaljiProdaje : Window
    {

        private Prodaja racun;
        private Korisnik ulogovaniKorisnik;
        ICollectionView viewStavkeNamestaja;
        ICollectionView viewUsluge;

        public DetaljiProdaje(Korisnik ulogovaniKorisnik, Prodaja racun)
        {

            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;
            this.racun = racun;
            //Napuni();
            viewStavkeNamestaja = CollectionViewSource.GetDefaultView(ProdatiNamestaj.StavkeNamestajaPoRacunu(racun));
            dgStavkeNamestaja.ItemsSource = viewStavkeNamestaja;
            viewUsluge = CollectionViewSource.GetDefaultView(ProdateUsluge.GetAll(racun));
            dgUsluge.ItemsSource = viewUsluge;
            dgStavkeNamestaja.IsSynchronizedWithCurrentItem = true;
            dgUsluge.IsSynchronizedWithCurrentItem = true;
            dgStavkeNamestaja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgUsluge.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }



        //public void Napuni()
        //{


        //    foreach (DodatnaUsluga du in ProdateUsluge.GetAll(racun))
        //    {
        //        lbUsluge.Items.Add(du);
        //    }
        //}

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mw = new MainWindow(this.ulogovaniKorisnik);
            mw.Show();
            this.Close();
        }

        private void dgStavkeNamestaja_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Namestaj")
            {
                e.Column.Width = 750;
            }
        }

        private void dgUsluge_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Naziv")
            {
                e.Column.Width = 450;
            }
        
        }


        //private void dgStavkeNamestaja_LoadingRow(object sender, DataGridRowEventArgs e)
        //{
        //    e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        //}

    }
}
