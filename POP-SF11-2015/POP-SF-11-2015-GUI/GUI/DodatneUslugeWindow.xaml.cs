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
    /// Interaction logic for DodatneUslugeWindow.xaml
    /// </summary>
    public partial class DodatneUslugeWindow : Window
    {
        private Korisnik ulogovaniKorisnik;

        public DodatneUslugeWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;

            dgDodatneUsluge.ItemsSource = Projekat.Instance.DodatneUsluge;
            dgDodatneUsluge.IsSynchronizedWithCurrentItem = true;
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
            var izabranaUsluga = (DodatnaUsluga)dgDodatneUsluge.SelectedItem;

            var uslugaProzor = new DodatneUslugeEditWindow(izabranaUsluga, DodatneUslugeEditWindow.TipOperacije.IZMENA);
            uslugaProzor.ShowDialog();
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
                            break;
                        }
                    }

                    //OsveziPrikaz();
                }

                GenericSerializer.Serialize("dodatne_usluge.xml", Projekat.Instance.DodatneUsluge);
            }
        }
    }
}
