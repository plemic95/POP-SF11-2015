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

namespace POP_SF_11_2015_GUI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {

            //Salon s1 = new Salon()

            //{
            //    Id = 1,
            //    Naziv = "Forma FTNale",
            //    Adresa = "Trg Dositeja Obradovica 6",
            //    Telefon = "061/2737551",
            //    Email = "milanplemicyahoo.com",
            //    Websajt = "http://www.ftn.uns.ac.rs",
            //    PIB = 123415235,
            //    MaticniBroj = 23706,
            //    BrojZiroRacuna = "840-00017666-45",
            //};

            //var listaSalona = new List<Salon>();
            //listaSalona.Add(s1);

            //GenericSerializer.Serialize<Salon>("salon.xml", listaSalona);

            //listaSalona = GenericSerializer.Deserialize<Salon>("salon.xml");

            //Console.WriteLine("Finished serialization...");

            //var du1 = new DodatnaUsluga()
            //{
            //    Id = 1,
            //    Naziv = "Transport",
            //    Cena = 555,
            //    Obrisan = false
            //};

            //var du2 = new DodatnaUsluga()
            //{
            //    Id = 2,
            //    Naziv = "Servis",
            //    Cena = 555,
            //    Obrisan = false
            //};

            //var listaDodatnihUsluga = new List<DodatnaUsluga>();
            //listaDodatnihUsluga.Add(du1);
            //listaDodatnihUsluga.Add(du2);

            //GenericSerializer.Serialize<DodatnaUsluga>("dodatne_usluge.xml", listaDodatnihUsluga);

            //listaDodatnihUsluga = GenericSerializer.Deserialize<DodatnaUsluga>("dodatne_usluge.xml");

            //Console.WriteLine("Finished serialization...");

            InitializeComponent();
            lblErrorMessage.Visibility = Visibility.Hidden;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            Korisnik ulogovaniKorisnik = Projekat.Instance.login(tbUsername.Text, pbPassword.Password);
            
            if (ulogovaniKorisnik != null)
            {
                MainWindow mw = new MainWindow(ulogovaniKorisnik);
                mw.Show();
                this.Close();
            }
            else
            {
                lblErrorMessage.Visibility = Visibility.Visible;
                lblErrorMessage.Content = $"Uneli ste pogresno korisnicko ime i/ili lozinku! Preostalo je jos 3 pokusaja!";
                lblErrorMessage.FontSize = 16;
                lblErrorMessage.Width = Double.NaN;
            }
        }
    }
}
