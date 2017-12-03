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
    /// Interaction logic for AkcijeEditWindow.xaml
    /// </summary>
    public partial class AkcijeEditWindow : Window
    {

        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }

        private Akcija akcije;
        private TipOperacije operacija;

        public AkcijeEditWindow(Akcija akcije, TipOperacije operacija)
        {
            InitializeComponent();

            this.akcije = akcije;
            this.operacija = operacija;

            tbImeAkcije.DataContext = akcije;
            tbPopust.DataContext = akcije;
            clnDatumPocetka.DataContext = akcije;
            clnDatumZavrsetka.DataContext = akcije;
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNamestajLista_Click(object sender, RoutedEventArgs e)
        {

            var listaAkcija = Projekat.Instance.Akcije;

            this.DialogResult = true;
            if (operacija == TipOperacije.DODAVANJE)
            {
                akcije.Id = listaAkcija.Count + 1;
                NamestajNaAkcijiWindow naw = new NamestajNaAkcijiWindow();
                naw.Show();
                this.Close();
                listaAkcija.Add(akcije);
            }

            GenericSerializer.Serialize("akcije.xml", listaAkcija);


            Close();

            //var listaAkcija = Projekat.Instance.Akcije;

            //switch (operacija)
            //{
            //    case TipOperacije.DODAVANJE:
            //        akcije.Id = listaAkcija.Count + 1;
            //        NamestajNaAkcijiWindow naw = new NamestajNaAkcijiWindow();
            //        naw.Show();
            //        this.Close();
            //        listaAkcija.Add(akcije);
            //        break;
            //    case TipOperacije.IZMENA:
            //        foreach (var a in listaAkcija)
            //        {
            //            if (a.Id == akcije.Id)
            //            {
            //                a.ImeAkcije = akcije.ImeAkcije;
            //                a.Popust = akcije.Popust;
            //                a.DatumPocetka = akcije.DatumPocetka;
            //                a.DatumZavrsetka = akcije.DatumZavrsetka;
            //                break;
            //            }
            //        }
            //        break;

            //}
            //GenericSerializer.Serialize("akcije.xml", listaAkcija);

            //Close();
        }
    }
}
