using POP_SF11_2015.Model;
using POP_SF11_2015.Utils;
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

namespace POP_SF_11_2015_GUI.GUI
{
    /// <summary>
    /// Interaction logic for KorisnikEditWindow.xaml
    /// </summary>
    public partial class KorisnikEditWindow : Window
    {

        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }

        private Korisnik korisnik;
        private TipOperacije operacija;


        public KorisnikEditWindow(Korisnik korisnik, TipOperacije operacija = TipOperacije.DODAVANJE)
        {
            InitializeComponent();


            this.korisnik = korisnik;
            this.operacija = operacija;

            // cbTipKorisnika.ItemsSource = Projekat.Instance.TipoviKorisnika;

            cbTipKorisnika.ItemsSource = Enum.GetValues(typeof(TipKorisnika)).Cast<TipKorisnika>();

            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnickoIme.DataContext = korisnik;
            tbLozinka.DataContext = korisnik;
            cbTipKorisnika.DataContext = korisnik;

        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {


            if (korisnik.Ime != null && korisnik.Prezime != null && korisnik.KorisnickoIme != null && korisnik.Lozinka != null && korisnik.TipKorisnika != 0)
            {
                var listaKorisnika = Projekat.Instance.Korisnici;


                this.DialogResult = true;

                switch (operacija)
                {
                    case TipOperacije.DODAVANJE:
                        //korisnik.ID = listaKorisnika.Count + 1;
                        //listaKorisnika.Add(korisnik);


                        Korisnik.Create(korisnik);



                        break;



                    case TipOperacije.IZMENA:

                        /*
                        //ovaj foreach doubt?
                        foreach (var k in listaKorisnika)
                        {
                            if (k.ID == korisnik.ID)
                            {

                                k.Ime = korisnik.Ime;
                                k.Prezime = korisnik.Prezime;
                                k.KorIme = korisnik.KorIme;
                                k.Lozinka = korisnik.Lozinka;
                                k.TipKorisnika = korisnik.TipKorisnika;
                                KorisnikDAL.Update(k);
                                break;
                            }
                        }
                        */

                        Korisnik.Update(korisnik);

                        break;

                }


                this.Close();
            }
            else
            {

                if (korisnik.Ime == null)
                {
                    MessageBox.Show("Niste uneli ime!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (korisnik.Prezime == null)
                {
                    MessageBox.Show("Niste uneli prezime!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (korisnik.KorisnickoIme == null)
                {
                    MessageBox.Show("Niste uneli korisnicko ime!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (korisnik.Lozinka == null)
                {
                    MessageBox.Show("Niste uneli lozinku!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (korisnik.TipKorisnika == 0)
                {
                    MessageBox.Show("Niste uneli tip korisnika!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
        //var listaKorisnika = Projekat.Instance.Korisnici;

        //this.DialogResult = true;
        //if (operacija == TipOperacije.DODAVANJE)
        //{
        //    korisnik.Id = listaKorisnika.Count + 1;
        //    listaKorisnika.Add(korisnik);
        //}

        //GenericSerializer.Serialize("korisnici.xml", listaKorisnika);


        //Close();

        //switch (operacija)
        //{
        //    case TipOperacije.DODAVANJE:
        //        korisnik.Id = listaKorisnika.Count + 1;
        //        listaKorisnika.Add(korisnik);
        //        break;
        //    case TipOperacije.IZMENA:
        //        foreach (var k in listaKorisnika)
        //        {
        //            if (k.Id == korisnik.Id)
        //            {
        //                k.Ime = korisnik.Ime;
        //                k.Prezime = korisnik.Prezime;
        //                k.KorisnickoIme = korisnik.KorisnickoIme;
        //                k.Lozinka = korisnik.Lozinka;
        //                k.TipKorisnika = korisnik.TipKorisnika;
        //                break;
        //            }
        //        }
        //        break;

        //}
        //GenericSerializer.Serialize("korisnici.xml", listaKorisnika);

        //Close();
    }
    }

