﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF11_2015.Model
{
    public class TipNamestaja
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public bool Obrisan { get; set; }

        public static TipNamestaja GetById(int id)
        {
            foreach (var tipNamestaja in Projekat.Instance.TipoviNamestaja)
            {
                if(tipNamestaja.Id == id) 
                {
                    return tipNamestaja;
                }
            }
            return null;

            //return Projekat.Instance.TipoviNamestaja.SingleOrDefault(x => x.Id == id) ;
        }
    }
}
