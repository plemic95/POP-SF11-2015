using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF11_2015.Model
{
    public class Salon: INotifyPropertyChanged
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

        private string adresa;

        public string Adresa
        {
            get { return adresa; }
            set
            {
                adresa = value;
                OnPropertyChanged("Adresa");
            }
        }

        private string telefon;

        public string Telefon
        {
            get { return telefon; }
            set
            {
                telefon = value;
                OnPropertyChanged("Telefon");
            }
        }


        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }


        private string websajt;

        public string Websajt
        {
            get { return websajt; }
            set
            {
                websajt = value;
                OnPropertyChanged("Websajt");
            }
        }

        private int pib;

        public int PIB
        {
            get { return pib; }
            set
            {
                pib = value;
                OnPropertyChanged("PIB");
            }
        }

        private int maticniBroj;

        public int MaticniBroj
        {
            get { return maticniBroj; }
            set
            {
                maticniBroj = value;
                OnPropertyChanged("MaticniBroj");
            }
        }

        private string brojZiroRacuna;

        public string BrojZiroRacuna
        {
            get { return brojZiroRacuna; }
            set
            {
                brojZiroRacuna = value;
                OnPropertyChanged("BrojZiroRacuna");
            }
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
