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
    /// Interaction logic for ProdajaWindow.xaml
    /// </summary>
    public partial class ProdajaWindow : Window
    {

        private ICollectionView view;
        private CollectionViewSource cvs;
        private Korisnik ulogovaniKorisnik;

        public ProdajaWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;

            cvs = new CollectionViewSource();
            cvs.Source = Projekat.Instance.Prodaje;

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Prodaje);
            //view.Filter = ProdajaFilter;
            //dgNamestaj.ItemsSource = Projekat.Instance.Namestaj;
            
            dgProdaja.ItemsSource = cvs.View;
            dgProdaja.IsSynchronizedWithCurrentItem = true;
            dgProdaja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            if (ulogovaniKorisnik.TipKorisnika == TipKorisnika.Administrator)
            {
                btnDetaljiProdaje.Visibility = Visibility.Hidden;
                btnDodajProdaju.Visibility = Visibility.Hidden;
            }

        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(this.ulogovaniKorisnik);
            mw.Show();
            this.Close();
        }

        private void btnDodajProdaju_Click(object sender, RoutedEventArgs e)
        {
            var praznaProdaja = new Prodaja()
            {
                
            };

            var namestajProzor = new ProdajaEditWindow(praznaProdaja, ProdajaEditWindow.TipOperacije.DODAVANJE);
            namestajProzor.ShowDialog();
        }

        private void dgProdaja_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            if ((string)e.Column.Header == "Id")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "DatumProdaje")
            {
                e.Column.Header = "Datum Prodaje";
                e.Column.Width = 640;

                if (e.PropertyType == typeof(DateTime))
                {
                    DataGridTextColumn dataGridTextColumn = e.Column as DataGridTextColumn;
                    if (dataGridTextColumn != null)
                    {
                        dataGridTextColumn.Binding.StringFormat = "{0:dd/MM/yyyy}";
                    }
                }
            }
            if ((string)e.Column.Header == "BrojRacuna")
            {
                e.Column.Header = "Broj Racuna";
                e.Column.Width = 350;
            }
            if ((string)e.Column.Header == "Kupac")
            {
                e.Column.Width = 400;
            }
            if ((string)e.Column.Header == "NamestajZaProdaju")
            {
                e.Cancel = true;
            }
                if ((string)e.Column.Header == "UkupnaCena")
            {
                e.Column.Header = "Ukupna Cena";
                e.Column.Width = 400;
            }
            if ((string)e.Column.Header == "DodatnaUsluga")
            {
                e.Cancel = true;
            }
        }

        private void btnDetaljiProdaje_Click(object sender, RoutedEventArgs e)
        {
            Prodaja selektovaniRacun = (Prodaja)dgProdaja.SelectedItem;
            DetaljiProdaje dpw = new DetaljiProdaje(ulogovaniKorisnik, selektovaniRacun);
            dpw.Show();
            this.Close();
        }

        private void MyFilter(object sender, FilterEventArgs e)
        {
            Prodaja p = e.Item as Prodaja;
            if (p != null)
            {
                e.Accepted = p.Kupac.ToLower().Contains(tbPretraga.Text.ToLower()) || p.BrojRacuna.ToLower().Contains(tbPretraga.Text.ToLower()) || p.DatumProdaje.ToString().Contains(tbPretraga.Text);
            }
        }

        private void tbPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            cvs.Filter += new FilterEventHandler(MyFilter);
        }
    }
}
