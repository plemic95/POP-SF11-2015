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
    /// Interaction logic for NamestajNaAkcijiWindow.xaml
    /// </summary>
    public partial class NamestajNaAkcijiWindow : Window
    {
        private Namestaj namestaj;
        private ICollectionView view;

        public NamestajNaAkcijiWindow()
        {
            InitializeComponent();

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaji);
            view.Filter = NamestajAkcijaFilter;
            //dgNamestaj.ItemsSource = Projekat.Instance.Namestaj;
            dgNamestajNaAkciji.ItemsSource = view;
            dgNamestajNaAkciji.IsSynchronizedWithCurrentItem = true;
            dgNamestajNaAkciji.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private bool NamestajAkcijaFilter(object item)
        {
            Namestaj nam = item as Namestaj;
            return !nam.Obrisan;
            
        }

        private void dgNamestajNaAkciji_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
                e.Column.Width = 300;
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

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnShop_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
