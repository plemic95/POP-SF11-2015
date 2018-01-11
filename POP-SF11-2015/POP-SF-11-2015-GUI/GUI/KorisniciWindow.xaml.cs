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
using System.Windows.Shapes;
using POP_SF11_2015.Model;
using POP_SF11_2015.Utils;
using System.ComponentModel;

namespace POP_SF_11_2015_GUI.GUI
{
    /// <summary>
    /// Interaction logic for KorisniciWindow.xaml
    /// </summary>
    public partial class KorisniciWindow : Window
    {
        private ICollectionView view;
        private Korisnik ulogovaniKorisnik;
        private CollectionViewSource cvs;

        public KorisniciWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;
            cvs = new CollectionViewSource();
            cvs.Source = Projekat.Instance.Korisnici;

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Korisnici);
            cvs.View.Filter = KorisniciFilter;
            dgKorisnici.ItemsSource = cvs.View;
            dgKorisnici.IsSynchronizedWithCurrentItem = true;
            dgKorisnici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            if (ulogovaniKorisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                btnDodajKorisnika.Visibility = Visibility.Hidden;
                btnIzmeniKorisnika.Visibility = Visibility.Hidden;
                btnObrisi.Visibility = Visibility.Hidden;
            }

        }

        private bool KorisniciFilter(object item)
        {
            //return ((Namestaj)item).Obrisan == false;
            Korisnik kor = item as Korisnik;
            return !kor.Obrisan;
        }

        private void btnDodajKorisnika_Click(object sender, RoutedEventArgs e)
        {
            var prazanKorisnik = new Korisnik()
            {
                TipKorisnika = TipKorisnika.Administrator ,
            };

            var korisnikProzor = new KorisnikEditWindow(prazanKorisnik, KorisnikEditWindow.TipOperacije.DODAVANJE);
            korisnikProzor.ShowDialog();
        }

        private void btnIzmeniKorisnika_Click(object sender, RoutedEventArgs e)
        {

            Korisnik izabraniKorisnik = (Korisnik)dgKorisnici.SelectedItem;

            if (izabraniKorisnik != null)
            {
                Korisnik old = (Korisnik)izabraniKorisnik.Clone();
                KorisnikEditWindow kw = new KorisnikEditWindow(izabraniKorisnik, KorisnikEditWindow.TipOperacije.IZMENA);
                if (kw.ShowDialog() != true)
                {
                    int index = Projekat.Instance.Korisnici.IndexOf(izabraniKorisnik);
                    Projekat.Instance.Korisnici[index] = old;
                }
            }
         //   var izabraniKorisnik = (Korisnik)dgKorisnici.SelectedItem;

         //   var korisnikProzor = new KorisnikEditWindow(izabraniKorisnik, KorisnikEditWindow.TipOperacije.IZMENA);
         //   korisnikProzor.ShowDialog();

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
                var izabraniKorisnik = (Korisnik)dgKorisnici.SelectedItem;
                if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: { izabraniKorisnik.KorisnickoIme}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    foreach (var k in Projekat.Instance.Korisnici)
                    {
                        if (k.Id == izabraniKorisnik.Id)
                        {
                            Korisnik.Delete(k);
                            cvs.View.Refresh();
                            break;
                        }
                    }

                    //OsveziPrikaz();
                }

              //  GenericSerializer.Serialize("korisnici.xml", Projekat.Instance.Korisnici);
            }
        }

        private void dgKorisnici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Id")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Ime")
            {
                e.Column.Width = 300;
            }
            if ((string)e.Column.Header == "Prezime")
            {
                e.Column.Width = 300;
            }
            if ((string)e.Column.Header == "KorisnickoIme")
            {
                e.Column.Header = "Korisnicko Ime";
                e.Column.Width = 440;
            }
            if ((string)e.Column.Header == "Lozinka")
            {
                e.Column.Width = 440;
            }
            if ((string)e.Column.Header == "TipKorisnika")
            {
                e.Column.Header = "Tip Korisnika";
                e.Column.Width = 310;
            }
        }

        private void MyFilter(object sender, FilterEventArgs e)
        {
            Korisnik k = e.Item as Korisnik;
            if (k != null)
            {
                e.Accepted = k.Ime.ToLower().Contains(tbPretraga.Text.ToLower()) || k.Prezime.ToLower().Contains(tbPretraga.Text.ToLower()) || k.KorisnickoIme.ToLower().Contains(tbPretraga.Text.ToLower());
            }
        }

        private void tbPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            cvs.Filter += new FilterEventHandler(MyFilter);
        }
    }
}
