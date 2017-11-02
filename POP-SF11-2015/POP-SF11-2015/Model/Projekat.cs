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
                GenericSerializer.Serialize<TipNamestaja>("namestaj.xml", tipoviNamestaja);
            }
        }
    }
}
