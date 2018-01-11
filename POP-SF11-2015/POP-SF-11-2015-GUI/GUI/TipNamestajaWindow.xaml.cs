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
    /// Interaction logic for TipNamestajaWindow.xaml
    /// </summary>
    public partial class TipNamestajaWindow : Window
    {
        private ICollectionView view;
        private Korisnik ulogovaniKorisnik;

        public TipNamestajaWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.TipoviNamestaja);
            view.Filter = TipNamestajaFilter;
            dgTipNamestaja.ItemsSource = view;
            //dgDodatneUsluge.ItemsSource = Projekat.Instance.DodatneUsluge;
            dgTipNamestaja.IsSynchronizedWithCurrentItem = true;
            dgTipNamestaja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            if (ulogovaniKorisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                btnDodajTipNamestaja.Visibility = Visibility.Hidden;
                btnObrisi.Visibility = Visibility.Hidden;
            }
        }

        private bool TipNamestajaFilter(object item)
        {
            //return ((Namestaj)item).Obrisan == false;
            TipNamestaja tn = item as TipNamestaja;
            return !tn.Obrisan;
        }

        private void dgTipNamestaja_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Id")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Naziv")
            {
                e.Column.Width = 1790;
            }
        }

        private void btnDodajTipNamestaja_Click(object sender, RoutedEventArgs e)
        {
            var prazanTip = new TipNamestaja()
            {
                Naziv = "",
            };

            var tipProzor = new TipNamestajaEditWindow(prazanTip, TipNamestajaEditWindow.TipOperacije.DODAVANJE);
            tipProzor.ShowDialog();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(this.ulogovaniKorisnik);
            mw.Show();
            this.Close();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            var izabraniTip = (TipNamestaja)dgTipNamestaja.SelectedItem;

            //Namestaj izabraniNamestaj = (Namestaj)lbNamestaj.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: { izabraniTip.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var t in Projekat.Instance.TipoviNamestaja)
                {
                    if (t.Id == izabraniTip.Id)
                    {
                        TipNamestaja.Delete(t);
                        view.Refresh();
                        foreach (var n in Projekat.Instance.Namestaji)
                        {
                            if (n.TipNamestajaId == izabraniTip.Id)
                            {
                                n.Obrisan = true;
                            }
                        }
                        break;
                    }
                }

                //OsveziPrikaz();
            }
        }
    }
}
