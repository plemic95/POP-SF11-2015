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
    /// Interaction logic for NamestajWindow.xaml
    /// </summary>
    public partial class NamestajEditWindow : Window
    {

        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }

        private Namestaj namestaj;
        private TipOperacije operacija;

        public NamestajEditWindow(Namestaj namestaj, TipOperacije operacija = TipOperacije.DODAVANJE)
        {
            InitializeComponent();

            this.namestaj = namestaj;
            this.operacija = operacija;

            cbTipNamestaja.ItemsSource = Projekat.Instance.TipoviNamestaja;

            tbNaziv.DataContext = namestaj;
            cbTipNamestaja.DataContext = namestaj;
            tbSifra.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbKolicinaUMagacinu.DataContext = namestaj;
        }


        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {

            var listaNamestaja = Projekat.Instance.Namestaj;

            this.DialogResult = true;
            if (operacija == TipOperacije.DODAVANJE)
            {
                namestaj.Id = listaNamestaja.Count + 1;
                listaNamestaja.Add(namestaj);
            }

            GenericSerializer.Serialize("namestaj.xml", listaNamestaja);


            Close();
            //   this.Close();

            //   var listaNamestaja = Projekat.Instance.Namestaj;

            //  switch (operacija)
            //  {
            //      case TipOperacije.DODAVANJE:
            //          namestaj.Id = listaNamestaja.Count + 1;
            //          listaNamestaja.Add(namestaj);
            //          break;
            //      case TipOperacije.IZMENA:
            //          foreach (var n in listaNamestaja)
            //          {
            //              if (n.Id == namestaj.Id)
            //              {
            //                  n.TipNamestajaId = namestaj.TipNamestajaId;
            //                  n.Naziv = namestaj.Naziv;
            //                  n.Sifra = namestaj.Sifra;
            //                  n.Cena = namestaj.Cena;
            //                  n.KolicinaUMagacinu = namestaj.KolicinaUMagacinu;
            //                  listaNamestaja.Add(namestaj);
            //                  break;
            //              }
            //          }
            //          break;
            //var namestajZaIzmenu = listaNamestaja.SingleOrDefault(x => x.Id == namestaj.Id);
            // var namestajZaIzmenu = Namestaj.GetById(namestaj.Id);
            //namestajZaIzmenu.TipNamestajaId = tipNamestajaId;
            //namestajZaIzmenu.Naziv = tbNaziv.Text;
            ////namestajZaIzmenu.TipNamestajaId = ((TipNamestaja)cbTipNamestaja.SelectedItem).Id;
            //break;
            // }
            //string naziv = tbNaziv.Text;

            //    namestaj = new Namestaj()
            //    {
            //        Id = listaNamestaja.Count + 1,
            //        Naziv = naziv
            //    };

            //    listaNamestaja.Add(namestaj);
            //Projekat.Instance.Namestaj = listaNamestaja;

        }
    }
}
