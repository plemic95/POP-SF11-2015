using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_SF11_2015.Model
{
    public class Namestaj: INotifyPropertyChanged
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

        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        private string sifra;

        public string Sifra
        {
            get { return sifra; }
            set
            {
                sifra = value;
                OnPropertyChanged("Sifra");
            }
        }

        private TipNamestaja tipNamestaja;

        [XmlIgnore]

        public TipNamestaja TipNamestaja
        {
            get
            {
                if (tipNamestaja == null)
                {
                    tipNamestaja = TipNamestaja.GetById(TipNamestajaId);
                }
                return tipNamestaja;
            }
            set
            {
                tipNamestaja = value;
                TipNamestajaId = tipNamestaja.Id;
                OnPropertyChanged("TipNamestaja");
            }
        }

        private int tipNamestajaId;

        public int TipNamestajaId
        {
            get { return tipNamestajaId; }
            set
            {
                tipNamestajaId = value;
                OnPropertyChanged("TipNamestaja");
            }
        }

        private double cena;

        public double Cena
        {
            get { return cena; }
            set
            {
                cena = value;
                OnPropertyChanged("Cena");
            }
        }

        private int kolicinaUMagacinu;

        public int KolicinaUMagacinu
        {
            get { return kolicinaUMagacinu; }
            set
            {
                kolicinaUMagacinu = value;
                OnPropertyChanged("KolicinaUMagacinu");
            }
        }

        private double akcijskaCena;

        public double AkcijskaCena
        {
            get { return akcijskaCena; }
            set
            {
                akcijskaCena = value;
                OnPropertyChanged("AkcijskaCena");
            }
        }

        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }





        //public int Id { get; set; }

        //public string Naziv { get; set; }

        //public string Sifra { get; set; }

        //public double Cena { get; set; }

        //public int KolicinaUMagacinu { get; set; }

        //public int TipNamestajaId { get; set; }

        //public Akcija Akcija { get; set; }

        //public bool Obrisan { get; set; }

        public object Clone()
        {
            Namestaj kopija = new Namestaj();
            kopija.Id = Id;
            kopija.Naziv = Naziv;
            kopija.Sifra = Sifra;
            kopija.TipNamestajaId = TipNamestajaId;
            kopija.Cena = Cena;
            kopija.KolicinaUMagacinu = KolicinaUMagacinu;
            kopija.AkcijskaCena = AkcijskaCena;
            return kopija;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"{ Naziv}, { Cena}, { TipNamestaja.GetById(TipNamestajaId).Naziv}";
        }

        public static Namestaj GetById(int id)
        {
            foreach (var namestaj in Projekat.Instance.Namestaj)
            {
                if (namestaj.Id == id)
                {
                    return namestaj;
                }
            }
            return null;

            //return Projekat.Instance.TipoviNamestaja.SingleOrDefault(x => x.Id == id) ;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
