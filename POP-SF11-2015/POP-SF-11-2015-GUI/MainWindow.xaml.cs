using POP_SF_11_2015_GUI.GUI;
using POP_SF11_2015.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP_SF_11_2015_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Korisnik ulogovaniKorisnik;
        private Salon salon;

        public MainWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;


            //  if (ulogovaniKorisnik.TipKorisnika.Administrator)
            //  {
            //      btnKorisnici.IsEnabled = false;
            //   }

            //public MainWindow()
            //{
            //    InitializeComponent();
            //
            //    OsveziPrikaz();
            //}
        }


        private void btnNamestaj_Click(object sender, RoutedEventArgs e)
        {
            NamestajWindow nw = new NamestajWindow(this.ulogovaniKorisnik);
            nw.Show();
            this.Close();
        }

        private void btnKorisnici_Click(object sender, RoutedEventArgs e)
        {
            KorisniciWindow kw = new KorisniciWindow(this.ulogovaniKorisnik);
            kw.Show();
            this.Close();

        }

        private void btnAkcije_Click(object sender, RoutedEventArgs e)
        {
            AkcijeWindow aw = new AkcijeWindow(this.ulogovaniKorisnik);
            aw.Show();
            this.Close();
        }

        private void btnDodatneUsluge_Click(object sender, RoutedEventArgs e)
        {
            DodatneUslugeWindow duw = new DodatneUslugeWindow(this.ulogovaniKorisnik);
            duw.Show();
            this.Close();
        }

        private void btnProdaja_Click(object sender, RoutedEventArgs e)
        {
            ProdajaWindow pw = new ProdajaWindow(this.ulogovaniKorisnik);
            pw.Show();
            this.Close();
        }

        private void btnSalon_Click(object sender, RoutedEventArgs e)
        {

            SalonWindow sw = new SalonWindow(this.ulogovaniKorisnik);
            sw.Show();
            this.Close();
            //var salon = new Salon();

            //var salonProzor = new SalonEditWindow(salon, SalonEditWindow.TipOperacije.IZMENA);
            //salonProzor.ShowDialog();
            //this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Close();
        }
    }
}
