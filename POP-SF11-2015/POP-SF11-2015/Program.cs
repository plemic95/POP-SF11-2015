using POP_SF11_2015.Model;
using POP_SF11_2015.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF11_2015
{
    class Program
    {
        private static List<Namestaj> Namestaj = new List<Namestaj>();
        private static List<TipNamestaja> TipoviNamestaja = new List<TipNamestaja>();

        static void Main(string[] args)
        {
            //A a = new A();

            //napraviti administraciju navedenih podataka pregled, unos i izmeu i brisanje podataka

            Salon s1 = new Salon()

            {
            Id = 1,
            Adresa = "Trg Dositeja Obradovica 6",
            BrojZiroRacuna = "840-00017666-45",
            Email = "milanplemicyahoo.com",
            Naziv = "Forma FTNale",
            PIB = 123415235,
            Telefon = "061/2737551",
            Websajt = "http://www.ftn.uns.ac.rs",
            };

            var tp1 = new TipNamestaja()
            {
                Id = 1,
                Naziv = "Krevet",
            };

            var tp2 = new TipNamestaja()
            {
                Id = 2,
                Naziv = "Sofa",
            };


            var n1 = new Namestaj()
            {
                Id = 1,
                Cena = 777,
                TipNamestaja = tp1,
                Naziv = "Ekstra krevet",
                KolicinaUMagacinu = 100,
                Sifra = "KRO1995SOC",
            };

            var n2 = new Namestaj()
            {
                Id = 2,
                Cena = 555,
                TipNamestaja = tp1,
                Naziv = "Stolica",
                KolicinaUMagacinu = 105,
                Sifra = "STO88GC",
            };

            var n3 = new Namestaj()
            {
                Id = 3,
                Cena = 200,
                TipNamestaja = tp2,
                Naziv = "Polica",
                KolicinaUMagacinu = 111,
                Sifra = "PIPCLU582",
            };


            Namestaj.Add(n1);
            Namestaj.Add(n2);
            Namestaj.Add(n3);
            TipoviNamestaja.Add(tp1);
            TipoviNamestaja.Add(tp2);

            Console.WriteLine($"=====Dobrodosli u salon namestaja { s1.Naziv}");

            IspisiGlavniMeni();
       }

        private static void IspisiGlavniMeni()
        {
            int izbor = 0;

            do
            {
                //ispitaj dok je razlicito od nule
                do
                {
                    Console.WriteLine("===== GLAVNI MENI=======");
                    Console.WriteLine("1.Rad sa namestajima");
                    Console.WriteLine("2.Rad sa tipom namestaja");
                    //...dovrsiti kod kuce
                    Console.WriteLine("0.Izlaz iz aplikacije");

                    izbor = int.Parse(Console.ReadLine());
                } while (izbor < 0 || izbor > 2);

                switch (izbor)
                {
                    case 1:
                        NamestajMeni();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);
        }

        private static void NamestajMeni()
        {
            int izbor = 0;

            do
            {
                do
                {
                    Console.WriteLine("=====RAD SA NAMESTAJEM========");
                    //Console.WriteLine("1.Dodaj novi namestaj");
                    //Console.WriteLine("2.Izmeni postojeci");
                    IspisCRUDMeni();

                    izbor = int.Parse(Console.ReadLine());

                } while (izbor < 0 || izbor > 4);

                switch (izbor)
                {
                    case 1:
                        PrikaziNamestaj();
                        break;
                    case 2:
                        DodajNamestaj();
                        break;
                    case 3:
                        IzmeniNamestaj();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);

        }

        private static void IzmeniNamestaj()
        {
            Console.WriteLine("Unesite id namestaja koji zelite da izmenite");
            int idNamestaja = int.Parse(Console.ReadLine());
        }

        private static void DodajNamestaj()
        {
            Console.WriteLine("=====DODAJ NAMESTAJ========");

            Console.WriteLine("Unesite naziv");
            string naziv = Console.ReadLine();

            Console.WriteLine("Unesite cenu");
            double cena = double.Parse(Console.ReadLine());

            Console.WriteLine("Unesite id tipa namestaja"); //Napomena u praksi se veze preko ID-a
            int idTipaNamestaja = int.Parse(Console.ReadLine());

            TipNamestaja trazeniTipNamestaja = null;
            foreach (var tipNamestaja in TipoviNamestaja)
            {
                if(tipNamestaja.Id == idTipaNamestaja) //praksa tip namestaja.id == trazeni id!!
                {
                    trazeniTipNamestaja = tipNamestaja;
                }
            }

            var noviNamestaj = new Namestaj()
            {
                Id = Namestaj.Count + 1,
                Naziv = naziv,
                Cena = cena,
                TipNamestaja = trazeniTipNamestaja,
            };

            Namestaj.Add(noviNamestaj);

        }

        private static void PrikaziNamestaj()
        {
            Console.WriteLine("=======LISTING NAMESTAJA=========");

            for (int i = 0; i < Namestaj.Count; i++)
            {
                Console.WriteLine($"{i + 1}. naziv: { Namestaj[i].Naziv}, cena: {Namestaj[i].Cena}, tip namestaja: {Namestaj[i].TipNamestaja.Naziv}");
            }
        }

        private static void IspisCRUDMeni()
        {
            Console.WriteLine("1. Prikazi listing");
            Console.WriteLine("2. Dodaj novi");
            Console.WriteLine("3. Izmeni postojeci");
            Console.WriteLine("4. Obrisi postojeci");
            Console.WriteLine("0. Povratak na glavni meni");
        }
    }
}