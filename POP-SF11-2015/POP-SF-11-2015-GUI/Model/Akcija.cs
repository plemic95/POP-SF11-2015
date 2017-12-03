using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF11_2015.Model
{
    public class Akcija: INotifyPropertyChanged
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

        private string imeAkcije;

        public string ImeAkcije
        {
            get { return imeAkcije; }
            set
            {
                imeAkcije = value;
                OnPropertyChanged("ImeAkcije");
            }
        }

        private string popust;

        public string Popust
        {
            get { return popust; }
            set
            {
                popust = value;
                OnPropertyChanged("Popust");
            }
        }


        private DateTime datumPocetka;

        public DateTime DatumPocetka
        {
            get { return datumPocetka; }
            set
            {
                datumPocetka = value;
                OnPropertyChanged("DatumPocetka");
            }
        }

        private DateTime datumZavrsetka;

        public DateTime DatumZavrsetka
        {
            get { return datumZavrsetka; }
            set
            {
                datumZavrsetka = value;
                OnPropertyChanged("DatumZavrsteka");
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

        public object Clone()
        {
            Akcija kopija = new Akcija();
            kopija.Id = Id;
            kopija.ImeAkcije = ImeAkcije;
            kopija.Popust = Popust;
            kopija.DatumPocetka = DatumPocetka;
            kopija.DatumZavrsetka = DatumZavrsetka;
            return kopija;
        }

        public static Akcija GetById(int id)
        {
            foreach (var akcija in Projekat.Instance.Akcije)
            {
                if (akcija.Id == id)
                {
                    return akcija;
                }
            }
            return null;

            //return Projekat.Instance.TipoviNamestaja.SingleOrDefault(x => x.Id == id) ;
        }

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
