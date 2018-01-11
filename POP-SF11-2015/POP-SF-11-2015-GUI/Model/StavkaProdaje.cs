using POP_SF11_2015.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_11_2015_GUI.Model
{
    class StavkaProdaje
    {
        private Namestaj namestaj;
        private int kolicina;



        public Namestaj Namestaj
        {
            get { return namestaj; }
            set { namestaj = value; }
        }

        public int Kolicina
        {
            get { return kolicina; }
            set { kolicina = value; }
        }


    }
}
