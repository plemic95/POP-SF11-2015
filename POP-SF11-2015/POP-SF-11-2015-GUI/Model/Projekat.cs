﻿using POP_SF11_2015.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF11_2015.Model
{
    public class Projekat
    {
        public static Projekat Instance { get; private set; } = new Projekat();

        public ObservableCollection<TipNamestaja> TipoviNamestaja;
        public ObservableCollection<Namestaj> Namestaji;
        public ObservableCollection<Korisnik> Korisnici;
        public ObservableCollection<DodatnaUsluga> DodatneUsluge;
        public ObservableCollection<Salon> SalonNamestaja;
        public ObservableCollection<Prodaja> Prodaje;
        public ObservableCollection<Akcija> Akcije;
        //private List<Salon> salon;
        //public List<Korisnik> Korisnici;
        //public List<DodatnaUsluga> DodatneUsluge;

        //public List<Salon> Salon;

        private Projekat()
        {
            //TipoviNamestaja = new ObservableCollection<TipNamestaja>(GenericSerializer.Deserialize<TipNamestaja>("tipovi_namestaja.xml"));

            TipoviNamestaja = TipNamestaja.GetAll();
            Namestaji = Namestaj.GetAll();
            DodatneUsluge = DodatnaUsluga.GetAll();
            Korisnici = Korisnik.GetAll();
            SalonNamestaja = Salon.GetAll();
            Prodaje = Prodaja.GetAll();
            Akcije = Akcija.GetAll();
            //Namestaji = new ObservableCollection<Namestaj>(GenericSerializer.Deserialize<Namestaj>("namestaj.xml"));
        // Korisnici = new ObservableCollection<Korisnik>(GenericSerializer.Deserialize<Korisnik>("korisnici.xml"));
           // DodatneUsluge = new ObservableCollection<DodatnaUsluga>(GenericSerializer.Deserialize<DodatnaUsluga>("dodatne_usluge.xml"));
            //Salon = new ObservableCollection<Salon>(GenericSerializer.Deserialize<Salon>("salon.xml"));
            //Prodaja = new ObservableCollection<Prodaja>(GenericSerializer.Deserialize<Prodaja>("prodaja.xml"));
           // Akcije = new ObservableCollection<Akcija>(GenericSerializer.Deserialize<Akcija>("akcije.xml"));

        }


        public Korisnik login(string username, string password)
        {
            foreach (Korisnik k in Projekat.Instance.Korisnici)
            {

                if (username == k.KorisnickoIme && password == k.Lozinka)
                {
                    return k;
                }
            }
            return null;
        }
    }
}
