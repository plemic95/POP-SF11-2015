using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_SF11_2015.Model
{
    public class Prodaja: INotifyPropertyChanged

    {
        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private DateTime datumProdaje;

        public DateTime DatumProdaje
        {
            get { return datumProdaje; }
            set
            {
                datumProdaje = value;
                OnPropertyChanged("DatumProdaje");
            }
        }

        private string brojRacuna;

        public string BrojRacuna
        {
            get { return brojRacuna; }
            set
            {
                brojRacuna = value;
                OnPropertyChanged("BrojRacuna");
            }
        }

        private string kupac;

        public string Kupac
        {
            get { return kupac; }
            set
            {
                kupac = value;
                OnPropertyChanged("Kupac");
            }
        }

        private double ukupnaCena;

        public double UkupnaCena
        {
            get { return ukupnaCena; }
            set
            {
                ukupnaCena = value;
                OnPropertyChanged("UkupnaCena");
            }
        }



        private ObservableCollection<Namestaj> namestajZaProdaju = new ObservableCollection<Namestaj>();

        public ObservableCollection<Namestaj> NamestajZaProdaju
        {
            get { return namestajZaProdaju; }
            set
            {
                namestajZaProdaju = value;
                OnPropertyChanged("NamestajZaProdaju");
            }
        }

        private ObservableCollection<DodatnaUsluga> dodatnaUsluga = new ObservableCollection<DodatnaUsluga>();

        public ObservableCollection<DodatnaUsluga> DodatnaUsluga
        {
            get { return dodatnaUsluga; }
            set
            {
                dodatnaUsluga = value;
                OnPropertyChanged("DodatnaUsluga");
            }
        }


        public object Clone()
        {
            Prodaja kopija = new Prodaja();
            kopija.Id = Id;
            kopija.DatumProdaje = DatumProdaje;
            kopija.BrojRacuna = BrojRacuna;
            kopija.Kupac = Kupac;
            kopija.UkupnaCena = UkupnaCena;
            kopija.NamestajZaProdaju = NamestajZaProdaju;
            kopija.DodatnaUsluga = DodatnaUsluga;
            return kopija;
        }


        //  public const double PDV = 0.02;


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
