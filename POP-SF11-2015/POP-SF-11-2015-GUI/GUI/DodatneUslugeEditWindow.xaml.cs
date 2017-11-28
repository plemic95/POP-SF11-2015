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
    /// Interaction logic for DodatneUslugeEditWindow.xaml
    /// </summary>
    public partial class DodatneUslugeEditWindow : Window
    {

        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }

        private DodatnaUsluga dodatnaUsluga;
        private TipOperacije operacija;


        public DodatneUslugeEditWindow(DodatnaUsluga dodatnaUsluga, TipOperacije operacija)
        {
            InitializeComponent();


            this.dodatnaUsluga = dodatnaUsluga;
            this.operacija = operacija;

            tbNaziv.DataContext = dodatnaUsluga;
            tbCena.DataContext = dodatnaUsluga;
            tbId.IsEnabled = false;

        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var listaUsluga = Projekat.Instance.DodatneUsluge;

            switch (operacija)
            {
                case TipOperacije.DODAVANJE:
                    dodatnaUsluga.Id = listaUsluga.Count + 1;
                    listaUsluga.Add(dodatnaUsluga);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var du in listaUsluga)
                    {
                        if (du.Id == dodatnaUsluga.Id)
                        {
                            du.Naziv = dodatnaUsluga.Naziv;
                            du.Cena = dodatnaUsluga.Cena;
                            break;
                        }
                    }
                    break;
            }
            GenericSerializer.Serialize("dodatne_usluge.xml", listaUsluga);


            Close();
        }


    private void btnIzlaz_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
    }
}
