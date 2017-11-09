using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF11_2015.Model
{
    public class Namestaj
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Sifra { get; set; }

        public double Cena { get; set; }

        public int KolicinaUMagacinu { get; set; }

        public int TipNamestajaId { get; set; }

        public Akcija Akcija { get; set; }

        public bool Obrisan { get; set; }

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
    }
}
