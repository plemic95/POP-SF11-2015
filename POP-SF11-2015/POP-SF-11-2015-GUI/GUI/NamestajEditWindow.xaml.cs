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

        public NamestajEditWindow(Namestaj namestaj, TipOperacije operacija)
        {
            InitializeComponent();

            InicijalizujPodatke(namestaj, operacija);
        }

        private void InicijalizujPodatke(Namestaj namestaj, TipOperacije operacija)
        {
            this.namestaj = namestaj;
            this.operacija = operacija;

            tbNaziv.Text = namestaj.Naziv;
            foreach (var tipNamestaja in Projekat.Instance.TipoviNamestaja)
            {
                cbTipNamestaja.Items.Add(tipNamestaja);
            }

            foreach (TipNamestaja tipNamestaja in cbTipNamestaja.Items)
            {
                if (tipNamestaja.Id == namestaj.TipNamestajaId)
                {
                    cbTipNamestaja.SelectedItem = tipNamestaja;
                }
            }
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {

            var listaNamestaja = Projekat.Instance.Namestaj;
            Namestaj namestajZaIzmenu = null;
            int tipNamestajaId = ((TipNamestaja)cbTipNamestaja.SelectedItem).Id;

            switch (operacija)
            {
                case TipOperacije.DODAVANJE:
                    namestaj = new Namestaj()
                    {
                        Id = listaNamestaja.Count + 1,
                        Naziv = tbNaziv.Text,
                        TipNamestajaId = tipNamestajaId
                    };
                    listaNamestaja.Add(namestaj);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var n in listaNamestaja)
                    {
                        if (n.Id == namestaj.Id)
                        {
                            namestajZaIzmenu = n;
                        }
                    }
                    //var namestajZaIzmenu = listaNamestaja.SingleOrDefault(x => x.Id == namestaj.Id);
                    // var namestajZaIzmenu = Namestaj.GetById(namestaj.Id);
                    namestajZaIzmenu.TipNamestajaId = tipNamestajaId;
                    namestajZaIzmenu.Naziv = tbNaziv.Text;
                    //namestajZaIzmenu.TipNamestajaId = ((TipNamestaja)cbTipNamestaja.SelectedItem).Id;
                    break;
            }
            string naziv = tbNaziv.Text;

        //    namestaj = new Namestaj()
        //    {
        //        Id = listaNamestaja.Count + 1,
        //        Naziv = naziv
        //    };

        //    listaNamestaja.Add(namestaj);
            Projekat.Instance.Namestaj = listaNamestaja;

            Close();
        }
    }
}
