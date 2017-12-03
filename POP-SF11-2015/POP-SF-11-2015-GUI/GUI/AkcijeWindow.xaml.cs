using POP_SF_11_2015_GUI.GUI;
using POP_SF11_2015.Model;
using POP_SF11_2015.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private ICollectionView view;
        private Korisnik ulogovaniKorisnik;

        public AkcijeWindow(Korisnik ulogovaniKorisnik)
        {
            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;

            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Akcije);
            view.Filter = AkcijeFilter;
            //dgAkcije.ItemsSource = Projekat.Instance.Akcije;
            dgAkcije.ItemsSource = view;
            dgAkcije.IsSynchronizedWithCurrentItem = true;
            dgAkcije.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            
        }

            private bool AkcijeFilter(object item)
            {
            //return ((Namestaj)item).Obrisan == false;
            Akcija ak = item as Akcija;
            return !ak.Obrisan;
            }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(this.ulogovaniKorisnik);
            mw.Show();
            this.Close();
        }

        private void btnDodajAkciju_Click(object sender, RoutedEventArgs e)
        {
            var praznaAkcija = new Akcija()
            {
            };

            var akcijaProzor = new AkcijeEditWindow(praznaAkcija, AkcijeEditWindow.TipOperacije.DODAVANJE);
            akcijaProzor.ShowDialog();

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            Akcija izabranaAkcija = view.CurrentItem as Akcija;

            if (izabranaAkcija != null)
            {
                Akcija old = (Akcija)izabranaAkcija.Clone();
                AkcijeEditWindow aw = new AkcijeEditWindow(izabranaAkcija, AkcijeEditWindow.TipOperacije.IZMENA);
                if (aw.ShowDialog() != true)
                {
                    int index = Projekat.Instance.Akcije.IndexOf(izabranaAkcija);
                    Projekat.Instance.Akcije[index] = old;
                }
            }
           // var izabranaAkcija = (Akcija)dgAkcije.SelectedItem;

           // var akcijeProzor = new AkcijeEditWindow(izabranaAkcija, AkcijeEditWindow.TipOperacije.IZMENA);
           //  akcijeProzor.ShowDialog();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            var izabranaAkcija = (Akcija)dgAkcije.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: { izabranaAkcija.ImeAkcije}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var a in Projekat.Instance.Akcije)
                {
                    if (a.Id == izabranaAkcija.Id)
                    {
                        a.Obrisan = true;
                        view.Refresh();
                        break;
                    }
                }

                //OsveziPrikaz();
            }

            GenericSerializer.Serialize("akcije.xml", Projekat.Instance.Akcije);
        }

        private void dgAkcije_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "ImeAkcije")
            {
                e.Column.Header = "Ime Akcije";
                e.Column.Width = 540;
            }
            if ((string)e.Column.Header == "Id")
            {
                e.Column.Width = 100;
            }
            if ((string)e.Column.Header == "DatumPocetka")
            {
                e.Column.Header = "Datum Pocetka";
                e.Column.Width = 450;

                if (e.PropertyType == typeof(DateTime))
                {
                    DataGridTextColumn dataGridTextColumn = e.Column as DataGridTextColumn;
                    if (dataGridTextColumn != null)
                    {
                        dataGridTextColumn.Binding.StringFormat = "{0:dd/MM/yyyy}";
                    }
                }

            }
            if ((string)e.Column.Header == "DatumZavrsetka")
            {
                e.Column.Header = "Datum Zavrsetka";
                e.Column.Width = 450;

                if (e.PropertyType == typeof(DateTime))
                {
                    DataGridTextColumn dataGridTextColumn = e.Column as DataGridTextColumn;
                    if (dataGridTextColumn != null)
                    {
                        dataGridTextColumn.Binding.StringFormat = "{0:dd/MM/yyyy}";
                    }
                }

            }


            if ((string)e.Column.Header == "Popust")
            {
                e.Column.Width = 250;
            }
        }

        private void btnMore_Click(object sender, RoutedEventArgs e)
        {
            var izabranaAkcija = (Akcija)dgAkcije.SelectedItem;

            foreach (var a in Projekat.Instance.Akcije)
            {
                if (a.Id == izabranaAkcija.Id)
                {
                    IzabranaAkcijaWindow ia = new IzabranaAkcijaWindow(this.ulogovaniKorisnik);
                    ia.Show();
                    this.Close();
                    break;
                }
            }
        }


    }
}
