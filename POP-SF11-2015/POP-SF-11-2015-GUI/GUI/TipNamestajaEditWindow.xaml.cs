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
    /// Interaction logic for TipNamestajaEditWindow.xaml
    /// </summary>
    public partial class TipNamestajaEditWindow : Window
    {

        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }

        private TipNamestaja tipNamestaja;
        private TipOperacije operacija;


        public TipNamestajaEditWindow(TipNamestaja tipNamestaja, TipOperacije operacija)
        {
            InitializeComponent();

            var listaTipovaNamestaja = Projekat.Instance.TipoviNamestaja;
            this.tipNamestaja = tipNamestaja;
            this.operacija = operacija;

            tipNamestaja.Id = listaTipovaNamestaja.Count + 1;
            tbId.DataContext = tipNamestaja;
            tbId.IsEnabled = false;
            tbNaziv.DataContext = tipNamestaja;
        }
 

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {

            var listaTipovaNamestaja = Projekat.Instance.TipoviNamestaja;

            switch (operacija)
            {
                case TipOperacije.DODAVANJE:
                    tipNamestaja.Id = listaTipovaNamestaja.Count + 1;
                    listaTipovaNamestaja.Add(tipNamestaja);
                    break;
                case TipOperacije.IZMENA:
                    break;

            }
            GenericSerializer.Serialize("tipovi_namestaja.xml", listaTipovaNamestaja);

            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
