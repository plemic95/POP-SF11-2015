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
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace POP_SF_11_2015_GUI.GUI
{
    /// <summary>
    /// Interaction logic for SalonWindow.xaml
    /// </summary>
    public partial class SalonWindow : Window
    {

        private Korisnik ulogovaniKorisnik;
        private ICollectionView view;
        private Salon salon;

        public SalonWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Salon);
            //dgSalon.ItemsSource = Projekat.Instance.Salon;
            dgSalon.ItemsSource = view;
            dgSalon.IsSynchronizedWithCurrentItem = true;
            dgSalon.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

        }

        private void btnIzmeniSalon_Click(object sender, RoutedEventArgs e)
        {
            Salon izabraniSalon = view.CurrentItem as Salon;

            if (izabraniSalon != null)
            {
                Salon old = (Salon)izabraniSalon.Clone();
                SalonEditWindow sw = new SalonEditWindow(izabraniSalon, SalonEditWindow.TipOperacije.IZMENA);
                if (sw.ShowDialog() != true)
                {
                    int index = Projekat.Instance.Salon.IndexOf(izabraniSalon);
                    Projekat.Instance.Salon[index] = old;
                }
            }


            //var izabraniSalon = (Salon)dgSalon.SelectedItem;

            //var salonProzor = new SalonEditWindow(izabraniSalon, SalonEditWindow.TipOperacije.IZMENA);
            //salonProzor.ShowDialog();

            //OsveziPrikaz();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(this.ulogovaniKorisnik);
            mw.Show();
            this.Close();
        }

    }
}
