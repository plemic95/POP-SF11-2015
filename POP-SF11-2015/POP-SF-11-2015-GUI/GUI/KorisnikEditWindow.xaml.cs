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


        public KorisnikEditWindow(Korisnik korisnik, TipOperacije operacija)
        {
            InitializeComponent();


            this.korisnik = korisnik;
            this.operacija = operacija;

        }

    }
}
