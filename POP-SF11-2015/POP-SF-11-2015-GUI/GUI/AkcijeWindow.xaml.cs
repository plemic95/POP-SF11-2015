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
    /// Interaction logic for AkcijeWindow.xaml
    /// </summary>
    public partial class AkcijeWindow : Window
    {
        private Korisnik ulogovaniKorisnik;

        public AkcijeWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;
        }
    }
}
