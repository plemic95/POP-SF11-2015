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
    /// Interaction logic for ProdajaEditWindow.xaml
    /// </summary>
    public partial class ProdajaEditWindow : Window
    {

        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }

        private Prodaja prodaja;
        private TipOperacije operacija;

        public ProdajaEditWindow(Prodaja prodaja, TipOperacije operacija)
        {
            InitializeComponent();

            this.prodaja = prodaja;
            this.operacija = operacija;

            tbBrojRacuna.DataContext = prodaja;
            clnDatumProdaje.DataContext = prodaja;
            lbNamestajBox.SelectedItem = prodaja;
            lbUslugaBox.DataContext = prodaja;
            tbKupac.DataContext = prodaja;
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var listaProdaja = Projekat.Instance.Prodaja;

            switch (operacija)
            {
                case TipOperacije.DODAVANJE:
                    prodaja.Id = listaProdaja.Count + 1;
                    listaProdaja.Add(prodaja);
                    break;
                case TipOperacije.IZMENA:
                    break;
            }
            GenericSerializer.Serialize("prodaja.xml", listaProdaja);

            Close();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
