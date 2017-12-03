using POP_SF11_2015.Model;
using POP_SF11_2015.Utils;
using System;
using System.Collections;
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
    /// Interaction logic for DodatneUslugeWindow.xaml
    /// </summary>
    public partial class DodatneUslugeWindow : Window
    {
        private ICollectionView view;
        private Korisnik ulogovaniKorisnik;

        public DodatneUslugeWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatneUsluge);
            view.Filter = UslugeFilter;
            dgDodatneUsluge.ItemsSource = view;
            //dgDodatneUsluge.ItemsSource = Projekat.Instance.DodatneUsluge;
            dgDodatneUsluge.IsSynchronizedWithCurrentItem = true;
            dgDodatneUsluge.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private bool UslugeFilter(object item)
        {
            //return ((Namestaj)item).Obrisan == false;
            DodatnaUsluga du = item as DodatnaUsluga;
            return !du.Obrisan;
        }

        private void btnDodajUslugu_Click(object sender, RoutedEventArgs e)
        {
            var praznaUsluga = new DodatnaUsluga()
            {
                Naziv = "",
                Cena = 0,
            };

            var uslugaProzor = new DodatneUslugeEditWindow(praznaUsluga, DodatneUslugeEditWindow.TipOperacije.DODAVANJE);
            uslugaProzor.ShowDialog();
        }

        private void btnIzmeniUslugu_Click(object sender, RoutedEventArgs e)
        {

            DodatnaUsluga izabranaUsluga = view.CurrentItem as DodatnaUsluga;

            if (izabranaUsluga != null)
            {
                DodatnaUsluga old = (DodatnaUsluga)izabranaUsluga.Clone();
                DodatneUslugeEditWindow dw = new DodatneUslugeEditWindow(izabranaUsluga, DodatneUslugeEditWindow.TipOperacije.IZMENA);
                if (dw.ShowDialog() != true)
                {
                    int index = Projekat.Instance.DodatneUsluge.IndexOf(izabranaUsluga);
                    Projekat.Instance.DodatneUsluge[index] = old;
                }
            }
            //var izabranaUsluga = (DodatnaUsluga)dgDodatneUsluge.SelectedItem;

            //var uslugaProzor = new DodatneUslugeEditWindow(izabranaUsluga, DodatneUslugeEditWindow.TipOperacije.IZMENA);
            //uslugaProzor.ShowDialog();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(this.ulogovaniKorisnik);
            mw.Show();
            this.Close();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            {
                var izabranaUsluga = (DodatnaUsluga)dgDodatneUsluge.SelectedItem;
                if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: { izabranaUsluga.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    foreach (var n in Projekat.Instance.DodatneUsluge)
                    {
                        if (n.Id == izabranaUsluga.Id)
                        {
                            n.Obrisan = true;
                            view.Refresh();
                            break;
                        }
                    }

                    //OsveziPrikaz();
                }

                GenericSerializer.Serialize("dodatne_usluge.xml", Projekat.Instance.DodatneUsluge);
            }
        }

        private void dgDodatneUsluge_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Id")
            {
                e.Column.Width = 100;
            }
            if ((string)e.Column.Header == "Naziv")
            {
                e.Column.Width = 1000;
            }
            if ((string)e.Column.Header == "Cena")
            {
                e.Column.Width = 690;
            }
        }
    }
}
