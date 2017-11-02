using POP_SF11_2015.Model;
using POP_SF11_2015.Tests;
using POP_SF11_2015.Utils;
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
        private static List<Korisnik> Korisnik = new List<Korisnik>();
        private static List<DodatnaUsluga> DodatnaUsluga = new List<DodatnaUsluga>();

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

            var k1 = new Korisnik()
            {
                Id = 1,
                Ime = "Milan",
                Prezime = "Plemic",
                KorisnickoIme = "plemic95",
                Lozinka = "12345",
            };

            var k2 = new Korisnik()
            {
                Id = 2,
                Ime = "Marko",
                Prezime = "Markovic",
                KorisnickoIme = "marko1",
                Lozinka = "555333",
            };

            var du1 = new DodatnaUsluga()
            {
                Id = 1,
                Naziv = "Transport",
                Cena = 555,
            };

            var du2 = new DodatnaUsluga()
            {
                Id = 2,
                Naziv = "Servis",
                Cena = 555,
            };

            //var listaTipovaNamestaja = new List<TipNamestaja>();
            //listaTipovaNamestaja.Add(tp1);
            //listaTipovaNamestaja.Add(tp2);

            //GenericSerializer.Serialize<TipNamestaja>("tipovi_namestaja.xml", listaTipovaNamestaja);

            var listaTipovaNamestaja = Projekat.Instance.TipoviNamestaja;

            var noviTipNamestaja = new TipNamestaja()
            {
                Id = listaTipovaNamestaja.Count + 1,
                Naziv = "Ugaona "
            };

            listaTipovaNamestaja.Add(noviTipNamestaja);
            Projekat.Instance.TipoviNamestaja = listaTipovaNamestaja;

            Console.WriteLine("Finished serialization...");
            //DELETE
            //var listaTipovaNamestaja = Projekat.Instance.TipoviNamestaja;


            //listaTipovaNamestaja.RemoveAt();
            //Projekat.Instance.TipoviNamestaja = listaTipovaNamestaja;

            //Console.WriteLine("Finished serialization...");


            //var listaNamestaja = new List<Namestaj>();
            //listaNamestaja.Add(n1);
            //listaNamestaja.Add(n2);

            //GenericSerializer.Serialize<Namestaj>("namestaj.xml", listaNamestaja);

            //listaNamestaja = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");

            //Console.WriteLine("Finished serialization...");

            //Console.ReadLine();

            //Namestaj.Add(n1);
            //Namestaj.Add(n2);
            //Namestaj.Add(n3);


            ///// ISPIS Namestaja i TIPA Tipa Namestaja
           // var listaNamestaja = Projekat.Instance.Namestaj;
           // var prviNamestaj = listaNamestaja[0];

           // var listaTipovaNamestaja = Projekat.Instance.TipoviNamestaja;
          //  TipNamestaja = trazeniTipNamestaja = null;
           // foreach (var tipNamestaja in listaTipovaNamestaja)
          //  {
            //    if(tipNamestaja.Id == prviNamestaj.TipNamestajaId)
             //   {
              //      trazeniTipNamestaja = tipNamestaja;
               //     break;

              //  }
           // }


            TipoviNamestaja.Add(tp1);
            TipoviNamestaja.Add(tp2);
            Korisnik.Add(k1);
            Korisnik.Add(k2);
            DodatnaUsluga.Add(du1);
            DodatnaUsluga.Add(du2);

            Console.WriteLine("");
            Console.WriteLine($"Dobrodosli u salon namestaja { s1.Naziv}");
            Console.WriteLine("");

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
                    Console.WriteLine("");
                    Console.WriteLine("===== GLAVNI MENI=====");
                    Console.WriteLine("");
                    Console.WriteLine("1. Rad sa namestajem");
                    Console.WriteLine("2. Rad sa tipom namestaja");
                    Console.WriteLine("3. Rad sa dodatnim uslugama");
                    Console.WriteLine("4. Rad sa korisnicima");
                    Console.WriteLine("5. Rad sa akcijama");
                    Console.WriteLine("");
                    //...dovrsiti kod kuce
                    Console.WriteLine("0. Izlaz iz aplikacije");

                    izbor = int.Parse(Console.ReadLine());
                } while (izbor < 0 || izbor > 5);

                switch (izbor)
                {
                    case 1:
                        NamestajMeni();
                        break;
                    case 2:
                        TipNamestajaMeni();
                        break;
                    case 3:
                        DodatnaUslugaMeni();
                        break;
                    case 4:
                        KorisniciMeni();
                        break;
                    case 5:
                        AkcijaMeni();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);
        }

        private static void KorisniciMeni()
        {
            int izbor = 0;

            do
            {
                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("=====RAD SA KORISNICIMA=====");
                    Console.WriteLine("");
                    IspisCRUDMeni();

                    izbor = int.Parse(Console.ReadLine());

                } while (izbor < 0 || izbor > 4);

                switch (izbor)
                {
                    case 1:
                        PrikaziKorisnika();
                        break;
                    case 2:
                        DodajKorisnika();
                        break;
                    case 3:
                        IzmeniKorisnika();
                        break;
                    case 4:
                        ObrisiKorisnika();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);

        }

        private static void ObrisiKorisnika()
        {
            Console.WriteLine("");
            Console.WriteLine("=====BRISANJE KORISNIKA=====");
            Console.WriteLine("");

            for (int i = 0; i < Korisnik.Count; i++)
            {
                Console.WriteLine($"{i + 1}. ime: { Korisnik[i].Ime}, prezime: {Korisnik[i].Prezime}, korisnicko ime: {Korisnik[i].KorisnickoIme}");
            }

            Console.WriteLine("Unesite id korisnika kojeg zelite da izbrisete");
            int idKorisnika = int.Parse(Console.ReadLine());

            Console.WriteLine("Korisnik uspesno obrisan");
        }

        private static void IzmeniKorisnika()
        {
            Console.WriteLine("");
            Console.WriteLine("=====IZMENA KORISNIKA=====");
            Console.WriteLine("");

            for (int i = 0; i < Korisnik.Count; i++)
            {
                Console.WriteLine($"{i + 1}. ime: { Korisnik[i].Ime}, prezime: {Korisnik[i].Prezime}, korisnicko ime: {Korisnik[i].KorisnickoIme}");
            }

            Console.WriteLine("Unesite id korisnika kojeg zelite da izmenite");
            int idKorisnika = int.Parse(Console.ReadLine());

            foreach (Korisnik kor in Korisnik)
            {
                if (idKorisnika == kor.Id)
                {
                    Console.Write("Ime: ");
                    kor.Ime = Console.ReadLine();
                    Console.Write("Prezime: ");
                    kor.Prezime = Console.ReadLine();
                    Console.Write("Korisnicko ime: ");
                    kor.KorisnickoIme = Console.ReadLine();
                    Console.Write("Lozinka: ");
                    kor.Lozinka = Console.ReadLine();
                    Console.WriteLine("Korisnik uspesno izmenjen");
                }
            }

        }

        private static void DodajKorisnika()
        {
            Console.WriteLine("");
            Console.WriteLine("=====DODAJ KORISNIKA=====");
            Console.WriteLine("");

            Console.WriteLine("Unesite ime korisnika: ");
            string ime = Console.ReadLine();

            Console.WriteLine("Unesite prezime korisnika: ");
            string prezime = Console.ReadLine();

            Console.WriteLine("Unesite korisnicko ime: ");
            string korisnickoIme = Console.ReadLine();

            Console.WriteLine("Unesite lozinku: ");
            string lozinka = Console.ReadLine();

            var noviKorisnik= new Korisnik()
            {
                Id = Korisnik.Count + 1,
                Ime = ime,
                Prezime = prezime,
                KorisnickoIme = korisnickoIme,
                Lozinka = lozinka,
            };

            Korisnik.Add(noviKorisnik);
        }

        private static void PrikaziKorisnika()
        {
            Console.WriteLine("");
            Console.WriteLine("=====PRIKAZ KORISNIKA=====");
            Console.WriteLine("");

            for (int i = 0; i < Korisnik.Count; i++)
            {
                Console.WriteLine($"{i + 1}. ime: { Korisnik[i].Ime}, prezime: {Korisnik[i].Prezime}, korisnicko ime: {Korisnik[i].KorisnickoIme}");
            }
        }

        private static void AkcijaMeni()
        {
            int izbor = 0;

            do
            {
                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("=====RAD SA AKCIJAMA====");
                    Console.WriteLine("");
                    IspisCRUDMeni();

                    izbor = int.Parse(Console.ReadLine());

                } while (izbor < 0 || izbor > 4);

                switch (izbor)
                {
                    case 1:
                        PrikaziAkciju();
                        break;
                    case 2:
                        DodajAkciju();
                        break;
                    case 3:
                        IzmeniAkciju();
                        break;
                    case 4:
                        ObrisiAkciju();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);
        }

        private static void ObrisiAkciju()
        {

        }

        private static void IzmeniAkciju()
        {

        }

        private static void DodajAkciju()
        {

        }

        private static void PrikaziAkciju()
        {

        }

        private static void DodatnaUslugaMeni()
        {
            int izbor = 0;

            do
            {
                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("=====RAD SA DODATNIM USLUGAMA====");
                    Console.WriteLine("");
                    IspisCRUDMeni();

                    izbor = int.Parse(Console.ReadLine());

                } while (izbor < 0 || izbor > 4);

                switch (izbor)
                {
                    case 1:
                        PrikaziDodatnuUslugu();
                        break;
                    case 2:
                        DodajDodatnuUslugu();
                        break;
                    case 3:
                        IzmeniDodatnuUslugu();
                        break;
                    case 4:
                        ObrisiDodatnuUslugu();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);
        }

        private static void ObrisiDodatnuUslugu()
        {
            Console.WriteLine("");

            for (int i = 0; i < DodatnaUsluga.Count; i++)
            {
                Console.WriteLine($"{i + 1}. naziv: { DodatnaUsluga[i].Naziv}, cena: {DodatnaUsluga[i].Cena}");
            }

            Console.WriteLine("Unesite id dodatne usluge kojuzelite da izbrisete: ");
            int idDodatneUsluge = int.Parse(Console.ReadLine());

            Console.WriteLine("Dodatna usluga uspesno izbrisana!");
        }

        private static void IzmeniDodatnuUslugu()
        {
            Console.WriteLine("");

            for (int i = 0; i < DodatnaUsluga.Count; i++)
            {
                Console.WriteLine($"{i + 1}. naziv: { DodatnaUsluga[i].Naziv}, cena: {DodatnaUsluga[i].Cena}");
            }

            Console.WriteLine("");
            Console.WriteLine("Unesite id dodatne usluge koju zelite da izmenite: ");
            int idDodatneUsluge = int.Parse(Console.ReadLine());

            foreach (DodatnaUsluga du in DodatnaUsluga)
            {
                if (idDodatneUsluge == du.Id)
                {
                    Console.Write("Naziv: ");
                    du.Naziv = Console.ReadLine();
                    Console.Write("Cena: ");
                    du.Cena = double.Parse(Console.ReadLine());
                    Console.WriteLine("Dodatna usluga uspesno izmenjena!");
                }
            }
        }

        private static void DodajDodatnuUslugu()
        {
            Console.WriteLine("");
            Console.WriteLine("=====DODAJ DODATNU USLUGU=====");
            Console.WriteLine("");

            Console.WriteLine("Unesite naziv dodatne usluge: ");
            string naziv = Console.ReadLine();

            Console.WriteLine("Unesite cenu dodatne usluge: ");
            double cena = double.Parse(Console.ReadLine());

            var novaDodatnaUsluga = new DodatnaUsluga()
            {
                Id = DodatnaUsluga.Count + 1,
                Naziv = naziv,
                Cena = cena,
            };

            DodatnaUsluga.Add(novaDodatnaUsluga);

        }

        private static void PrikaziDodatnuUslugu()
        {
            Console.WriteLine("");
            Console.WriteLine("=====PRIKAZ DODATNIH USLUGA=====");
            Console.WriteLine("");

            for (int i = 0; i < DodatnaUsluga.Count; i++)
            {
                Console.WriteLine($"{i + 1}. naziv: { DodatnaUsluga[i].Naziv}, cena: {DodatnaUsluga[i].Cena}");
            }
        }

        private static void TipNamestajaMeni()
        {
            int izbor = 0;

            do
            {
                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("=====RAD SA TIPOM NAMESTAJA=====");
                    Console.WriteLine("");
                    IspisCRUDMeni();

                    izbor = int.Parse(Console.ReadLine());

                } while (izbor < 0 || izbor > 4);

                switch (izbor)
                {
                    case 1:
                        PrikaziTipNamestaja();
                        break;
                    case 2:
                        DodajTipNamestaja();
                        break;
                    case 3:
                        IzmeniTipNamestaja();
                        break;
                    case 4:
                        ObrisiTipNamestaja();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);


        }

        private static void ObrisiTipNamestaja()
        {

        }

        private static void IzmeniTipNamestaja()
        {

        }

        private static void DodajTipNamestaja()
        {

        }

        private static void PrikaziTipNamestaja()
        {

        }

        private static void NamestajMeni()
        {
            int izbor = 0;

            do
            {
                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("=====RAD SA NAMESTAJEM=====");
                    Console.WriteLine("");
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
                    case 4:
                        ObrisiNamestaj();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);

        }

        private static void ObrisiNamestaj()
        {
            Console.WriteLine("");

            for (int i = 0; i < Namestaj.Count; i++)
            {
                Console.WriteLine($"{i + 1}. naziv: { Namestaj[i].Naziv}, cena: {Namestaj[i].Cena}, tip namestaja: {Namestaj[i].TipNamestaja.Naziv}");
            }

            Console.WriteLine("Unesite id namestaja kojeg zelite da izbrisete: ");
            int idNamestaja = int.Parse(Console.ReadLine());
            Console.WriteLine("Namestaj uspesno obrisan");
        }

        private static void IzmeniNamestaj()
        {

            Console.WriteLine("");

            for (int i = 0; i < Namestaj.Count; i++)
            {
                Console.WriteLine($"{i + 1}. naziv: { Namestaj[i].Naziv}, cena: {Namestaj[i].Cena}, tip namestaja: {Namestaj[i].TipNamestaja.Naziv}");
            }

            Console.WriteLine("Unesite id namestaja koji zelite da izmenite: ");
            int idNamestaja = int.Parse(Console.ReadLine());

            foreach (Namestaj nam in Namestaj)
            {
                if (idNamestaja == nam.Id)
                {
                    Console.Write("Naziv: ");
                    nam.Naziv = Console.ReadLine();
                    Console.Write("Sifra: ");
                    nam.Sifra = Console.ReadLine();
                    Console.Write("Cena: ");
                    nam.Cena = double.Parse(Console.ReadLine());
                    Console.WriteLine("Namestaj uspesno izmenjen");
                }
                else
                {
                    Console.WriteLine("Niste uneli dobar naziv namestaja");
                }
                    }
        }

        private static void DodajNamestaj()
        {
            Console.WriteLine("");
            Console.WriteLine("=====DODAJ NAMESTAJ=====");
            Console.WriteLine("");

            Console.WriteLine("Unesite naziv: ");
            string naziv = Console.ReadLine();

            Console.WriteLine("Unesite cenu: ");
            double cena = double.Parse(Console.ReadLine());

            Console.WriteLine("Unesite id tipa namestaja: "); //Napomena u praksi se veze preko ID-a
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
            Console.WriteLine("");
            Console.WriteLine("=====ISPIS NAMESTAJA=====");
            Console.WriteLine("");

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