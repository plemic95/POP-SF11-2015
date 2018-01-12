using POP_SF11_2015.Model;
using POP_SF11_2015.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Namestaj namestaj;
        private DodatnaUsluga usluga;
        private TipOperacije operacija;
        private CollectionViewSource cvs;

        public ProdajaEditWindow(Prodaja prodaja, TipOperacije operacija)
        {
            InitializeComponent();

            this.prodaja = prodaja;
            this.operacija = operacija;
            cvs = new CollectionViewSource();
            cvs.Source = Projekat.Instance.Namestaji;
            dgNamestaj.ItemsSource = cvs.View;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            cvs = new CollectionViewSource();
            cvs.Source = Projekat.Instance.DodatneUsluge;
            dgDodatneUsluge.ItemsSource = cvs.View;
            dgDodatneUsluge.IsSynchronizedWithCurrentItem = true;
            dgDodatneUsluge.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            tbBrojRacuna.DataContext = prodaja;
            tbKupac.DataContext = prodaja;


            prodaja = new Prodaja();
        }

        public void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            Prodaja.Create(prodaja);
            Close();

            //var listaProdaja = Projekat.Instance.Prodaje;

            //switch (operacija)
            //{
            //    case TipOperacije.DODAVANJE:
            //        prodaja.Id = listaProdaja.Count + 1;
            //        listaProdaja.Add(prodaja);
            //        break;
            //    case TipOperacije.IZMENA:
            //        break;
            //}
            //GenericSerializer.Serialize("prodaja.xml", listaProdaja);

            //Close();
        }

        private void dgNamestaj_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;

            }
            if ((string)e.Column.Header == "TipNamestajaId")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "AkcijskaCena")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Id")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Naziv")
            {
                e.Column.Width = 110;
            }
            if ((string)e.Column.Header == "Cena")
            {
                e.Column.Width = 70;
            }
            if ((string)e.Column.Header == "Sifra")
            {
                e.Column.Width = 70;
            }
            if ((string)e.Column.Header == "KolicinaUMagacinu")
            {
                e.Column.Header = "Kolicina";
                e.Column.Width = 50;
            }
            if ((string)e.Column.Header == "TipNamestaja")
            {
                e.Column.Header = "Tip Namestaja";
                e.Column.Width = 90;
            }
        }

        private void dgDodatneUsluge_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Id")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Naziv")
            {
                e.Column.Width = 220;
            }
            if ((string)e.Column.Header == "Cena")
            {
                e.Column.Width = 170;
            }
        }



        //    namestaj = (Namestaj)dgNamestaj.SelectedItem;


        //    prodaja.NamestajZaProdaju.Add(namestaj);
        //    prodaja.UkupnaCena += namestaj.Cena * Int32.Parse(tbKolicina.Text);
        //    prodaja.DatumProdaje = DateTime.Now;
        //    prodaja.Kupac = tbKupac.Text;
        //    prodaja.BrojRacuna = tbBrojRacuna.Text;
        //    Projekat.Instance.Prodaje.Add(prodaja);

        //    foreach(Namestaj nam in Projekat.Instance.Namestaji)
        //    {
        //        if(nam.Id == namestaj.Id) {
        //            nam.KolicinaUMagacinu -= Int32.Parse(tbKolicina.Text);
        //            Namestaj.Update(nam);
        //        }

        //    }

        //    Prodaja.Create(prodaja);

        //    prodaja = new Prodaja();


        public void btnDodajStavku_Click(object sender, RoutedEventArgs e)
        {

               namestaj = (Namestaj)dgNamestaj.SelectedItem;
               usluga = (DodatnaUsluga)dgDodatneUsluge.SelectedItem;


               prodaja.NamestajZaProdaju.Add(namestaj);
               prodaja.DodatnaUsluga.Add(usluga);
               prodaja.UkupnaCena += namestaj.Cena * 1.20 * Int32.Parse(tbKolicina.Text) + usluga.Cena;

            // foreach (Namestaj nam in Projekat.Instance.Namestaji)
            // {
            if (namestaj.KolicinaUMagacinu <= 0)
            {
                
                //razdvojiti sad dva problema, da li je bas 0 ili je dao nulu validated on exeption
                //meh
                MessageBox.Show("Nema tog namestaja", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                Namestaj.Delete(namestaj);
                cvs.View.Refresh();
                Close();
            }

            if (namestaj.Id == namestaj.Id)
                {
                    namestaj.KolicinaUMagacinu -= Int32.Parse(tbKolicina.Text);
                    Namestaj.Update(namestaj);
                }

                prodaja.DatumProdaje = DateTime.Now;
                prodaja.Kupac = tbKupac.Text;
                prodaja.BrojRacuna = tbBrojRacuna.Text;

                Projekat.Instance.Prodaje.Add(prodaja);
                

              //  prodaja = new Prodaja();
            }
 //   }

    }
}

//{

//    Namestaj selektovaniNamestaj = (Namestaj)dgNamestaj.SelectedItem;
//    int kolicinaZaProdaju;

//    if (selektovaniNamestaj != null)
//    {
//        KolicinaWindow kw = new KolicinaWindow(selektovaniNamestaj);
//        kw.Owner = this;
//        kw.ShowDialog();
//        kolicinaZaProdaju = kw.Kolicina;
//        //MessageBox.Show(kw.Kolicina.ToString());
//        for (int i = 0; i < kolicinaZaProdaju; i++)
//        {
//            if (selektovaniNamestaj.KolicinaUMagacinu >= 1)
//            {
//                lbRacun.Items.Add(selektovaniNamestaj);
//                trenutniRacun.UkupnaCena = trenutniRacun.UkupnaCena + (selektovaniNamestaj.AkcijskaCena(selektovaniNamestaj.Cena, selektovaniNamestaj.Akcija.Popust));
//                selektovaniNamestaj.KolicinaUMagacinu = selektovaniNamestaj.KolicinaUMagacinu - 1;
//                NamestajDAL.Update(selektovaniNamestaj);
//                viewTrazeniNamestaj.Refresh();
//                viewNamestaj.Refresh();
//            }
//            else if (selektovaniNamestaj.KolicinaUMagacinu == 0)
//            {

//                MessageBox.Show("Nema ga na stanju!");
//            }
//        }

//    }
//    else
//    {
//        MessageBox.Show("Niste nista selektovali!");
//    }



//}

//private void DodajUsluguURacun(object sender, RoutedEventArgs e)
//{

//    DodatnaUsluga selektovanaUsluga = (DodatnaUsluga)dgDodatneUsluge.SelectedItem;

//    var dosadasnjeUsluge = new ObservableCollection<DodatnaUsluga>();

//    foreach (var i in lbRacun.Items)
//    {
//        if (i is DodatnaUsluga)
//        {
//            dosadasnjeUsluge.Add((DodatnaUsluga)i);
//        }
//    }

//    if (dosadasnjeUsluge.Contains(selektovanaUsluga))
//    {
//        MessageBox.Show("Vec ste dodali tu uslugu!");
//    }
//    else
//    {
//        lbRacun.Items.Add(selektovanaUsluga);
//        trenutniRacun.UkupnaCena = trenutniRacun.UkupnaCena + selektovanaUsluga.Cena;
//    }

//}

//private void ZavrsiProdaju(object sender, RoutedEventArgs e)
//{


//    if (lbRacun.Items.Count != 0 && trenutniRacun.BrojRacuna != null && trenutniRacun.Kupac != null)
//    {
//        var listaRacuna = Projekat.Instance.Prodaje;

//        trenutniRacun.DatumProdaje = DateTime.Now;
//        trenutniRacun.UkupnaCena = Racun.CenaSaPDV(trenutniRacun.UkupnaCena);


//        var lbRacunKolekcija = lbRacun.Items;



//        Prodaja.Create(trenutniRacun);


//        //Dodaj u dve kolekcije:

//        foreach (var i in lbRacunKolekcija)
//        {
//            if (i is Namestaj)
//            {
//                //trenutniRacun.NamestajZaProdaju.Add((Namestaj)i);
//                ProdatiNamestajDAL.Create((Namestaj)i, trenutniRacun);
//            }
//            else
//            {
//                //trenutniRacun.DodatneUsluge.Add((DodatneUsluge)i);
//                ProdateUslugeDAL.Create((DodatneUsluge)i, trenutniRacun);
//            }
//        }

//        viewRacuni.Refresh();


//        //ponovo ga setuj na prazan objekat:
//        trenutniRacun = new Racun();
//        //Racun noviRacun = new Racun();


//        MessageBox.Show("Prodaja izvrsena");

//        lblUkupnaCena.DataContext = trenutniRacun;
//        tbKupac.DataContext = trenutniRacun;
//        tbBrojRacuna.DataContext = trenutniRacun;
//        lbRacun.Items.Clear();


//    }
//    else
//    {
//        if (lbRacun.Items.Count == 0)
//        {
//            MessageBox.Show("Niste uneli nista u racun!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
//        }
//        if (trenutniRacun.BrojRacuna == null)
//        {
//            MessageBox.Show("Niste uneli broj racuna!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
//        }
//        if (trenutniRacun.Kupac == null)
//        {
//            MessageBox.Show("Niste uneli ime i prezime kupca!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
//        }
//    }




