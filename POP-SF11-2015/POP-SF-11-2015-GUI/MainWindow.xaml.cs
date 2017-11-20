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

        public MainWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;

            //  if (ulogovaniKorisnik.TipKorisnika.Oznaka != 1)
            //  {
            //      bKorisnici.IsEnabled = false;
            //  }

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
            SalonEditWindow sew = new SalonEditWindow(this.ulogovaniKorisnik);
            sew.Show();
            this.Close();
        }
    }
}
