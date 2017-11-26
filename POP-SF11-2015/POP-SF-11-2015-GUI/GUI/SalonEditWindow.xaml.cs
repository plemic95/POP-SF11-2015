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
    /// Interaction logic for SalonEditWindow.xaml
    /// </summary>
    public partial class SalonEditWindow : Window
    {
        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }

        private Salon salon;
        private TipOperacije operacija;
        private Korisnik ulogovaniKorisnik;
        private TipOperacije iZMENA;


        public SalonEditWindow(Korisnik ulogovaniKorisnik, TipOperacije iZMENA)
        {
            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;
            this.iZMENA = iZMENA;
        }

        public SalonEditWindow(Korisnik ulogovaniKorisnik, Salon salon, TipOperacije operacija)
        {
            this.ulogovaniKorisnik = ulogovaniKorisnik;
            this.salon = salon;
            this.operacija = operacija;

            tbNaziv.DataContext = salon;
            tbAdresa.DataContext = salon;
            tbTelefon.DataContext = salon;
            tbEmail.DataContext = salon;
            tbWebSajt.DataContext = salon;
            tbPIB.DataContext = salon;
            tbMaticniBroj.DataContext = salon;
            tbBrojZiroRacuna.DataContext = salon;
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(this.ulogovaniKorisnik);
            mw.Show();
            this.Close();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var listaSalon = Projekat.Instance.Salon;

            switch (operacija)
            {
                case TipOperacije.DODAVANJE:
                    break;
                case TipOperacije.IZMENA:
                    foreach (var s in listaSalon)
                    {
                        if (s.Id == salon.Id)
                        {
                            s.Naziv = salon.Naziv;
                            s.Adresa = salon.Adresa;
                            s.Telefon = salon.Telefon;
                            s.Email = salon.Email;
                            s.Websajt = salon.Websajt;
                            s.PIB = salon.PIB;
                            s.MaticniBroj = salon.MaticniBroj;
                            s.BrojZiroRacuna = salon.BrojZiroRacuna;
                            break;
                        }
                    }
                    break;
            }
            GenericSerializer.Serialize("salon.xml", listaSalon);

            Close();
        }
    }
    }
