using POP_SF11_2015.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF11_2015.Model
{
    public class Projekat
    {
        public static Projekat Instance { get; private set; } = new Projekat();

        private List<TipNamestaja> tipoviNamestaja;
        private List<Namestaj> namestaj;
        private List<Korisnik> korisnici;
        private List<DodatnaUsluga> dodatneUsluge;
        private List<Salon> salon;

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

        public List<TipNamestaja> TipoviNamestaja
        {
            get 
            { 
                tipoviNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tipovi_namestaja.xml");
                return tipoviNamestaja;
            }
            set 
            { 
                tipoviNamestaja = value;
                GenericSerializer.Serialize<TipNamestaja>("tipovi_namestaja.xml", tipoviNamestaja);
            }
        }

        public List<Namestaj> Namestaj
        {
            get
            {
                namestaj = GenericSerializer.Deserialize<Namestaj>("namestaj.xml"); 
                return namestaj;
            }
            set
            {
                namestaj = value;
                GenericSerializer.Serialize<Namestaj>("namestaj.xml", namestaj);
            }
        }

        public List<Korisnik> Korisnici
        {
            get
            {
                korisnici = GenericSerializer.Deserialize<Korisnik>("korisnici.xml");
                return korisnici;
            }
            set
            {
                korisnici = value;
                GenericSerializer.Serialize<Korisnik>("korisnici.xml", korisnici);
            }
        }

        public List<DodatnaUsluga> DodatneUsluge
        {
            get
            {
                dodatneUsluge = GenericSerializer.Deserialize<DodatnaUsluga>("dodatne_usluge.xml");
                return dodatneUsluge;
            }
            set
            {
                dodatneUsluge = value;
                GenericSerializer.Serialize<DodatnaUsluga>("dodatne_usluge.xml", dodatneUsluge);
            }
        }

        public List<Salon> Salon
        {
            get
            {
                salon = GenericSerializer.Deserialize<Salon>("salon.xml");
                return salon;
            }
            set
            {
                salon = value;
                GenericSerializer.Serialize<Salon>("salon.xml", salon);
            }
        }
    }
}
