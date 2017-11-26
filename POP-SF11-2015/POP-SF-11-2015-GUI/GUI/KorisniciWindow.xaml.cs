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

namespace POP_SF_11_2015_GUI.GUI
{
    /// <summary>
    /// Interaction logic for KorisniciWindow.xaml
    /// </summary>
    public partial class KorisniciWindow : Window
    {
        private Korisnik ulogovaniKorisnik;

        public KorisniciWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;

            dgKorisnici.ItemsSource = Projekat.Instance.Korisnici;
            dgKorisnici.IsSynchronizedWithCurrentItem = true;
        }

        private void btnDodajKorisnika_Click(object sender, RoutedEventArgs e)
        {
            var prazanKorisnik = new Korisnik()
            {
                Ime = "",
                Prezime = "",
                KorisnickoIme = "",
                Lozinka = ""
            };

            var korisnikProzor = new KorisnikEditWindow(prazanKorisnik, KorisnikEditWindow.TipOperacije.DODAVANJE);
            korisnikProzor.ShowDialog();
        }

        private void btnIzmeniKorisnika_Click(object sender, RoutedEventArgs e)
        {
            var izabraniKorisnik = (Korisnik)dgKorisnici.SelectedItem;

            var korisnikProzor = new KorisnikEditWindow(izabraniKorisnik, KorisnikEditWindow.TipOperacije.IZMENA);
            korisnikProzor.ShowDialog();

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
                    foreach (var n in Projekat.Instance.Korisnici)
                    {
                        if (n.Id == izabraniKorisnik.Id)
                        {
                            n.Obrisan = true;
                            break;
                        }
                    }

                    //OsveziPrikaz();
                }

                GenericSerializer.Serialize("korisnici.xml", Projekat.Instance.Korisnici);
            }
        }
    }
}
