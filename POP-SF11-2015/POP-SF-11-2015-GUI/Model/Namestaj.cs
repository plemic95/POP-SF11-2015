using POP_SF11_2015.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_SF11_2015.Model
{
    public class Namestaj: INotifyPropertyChanged
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

        private string sifra;

        public string Sifra
        {
            get { return sifra; }
            set
            {
                sifra = value;
                OnPropertyChanged("Sifra");
            }
        }

        private TipNamestaja tipNamestaja;

        [XmlIgnore]

        public TipNamestaja TipNamestaja
        {
            get
            {
                if (tipNamestaja == null)
                {
                    tipNamestaja = TipNamestaja.GetById(TipNamestajaId);
                }
                return tipNamestaja;
            }
            set
            {
                tipNamestaja = value;
                TipNamestajaId = tipNamestaja.Id;
                OnPropertyChanged("TipNamestaja");
            }
        }

        private int tipNamestajaId;

        public int TipNamestajaId
        {
            get { return tipNamestajaId; }
            set
            {
                tipNamestajaId = value;
                OnPropertyChanged("TipNamestaja");
            }
        }

        private double cena;

        public double Cena
        {
            get { return cena; }
            set
            {
                cena = value;
                OnPropertyChanged("Cena");
            }
        }

        private int kolicinaUMagacinu;

        public int KolicinaUMagacinu
        {
            get { return kolicinaUMagacinu; }
            set
            {
                kolicinaUMagacinu = value;
                OnPropertyChanged("KolicinaUMagacinu");
            }
        }


        //private double akcijskaCena;

        //    public double AkcijskaCena
        //   {
        //        get { return akcijskaCena; }
        //        set
        //        {
        //            akcijskaCena = value;
        //            OnPropertyChanged("AkcijskaCena");
        //        }
        //    }

        //public Akcija Akcija
        //{
        //    get
        //    {
        //        if (akcija == null)
        //        {
        //            return akcija = Akcija.GetById(idakcije);
        //        }
        //        return akcija;
        //    }
        //    set
        //    {
        //        akcija = value;

        //        if (akcija != null)
        //        {
        //            idakcije = akcija.Id;
        //        }
        //        OnPropertyChanged("Akcija");
        //    }

        //}

        //public int IDAkcije
        //{
        //    get
        //    {
        //        return idakcije;
        //    }
        //    set
        //    {
        //        idakcije = value;
        //        OnPropertyChanged("IDAkcije");
        //    }
        //}





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



   

        //public int Id { get; set; }

        //public string Naziv { get; set; }

        //public string Sifra { get; set; }

        //public double Cena { get; set; }

        //public int KolicinaUMagacinu { get; set; }

        //public int TipNamestajaId { get; set; }

        //public Akcija Akcija { get; set; }

        //public bool Obrisan { get; set; }

        public object Clone()
        {
            Namestaj kopija = new Namestaj();
            kopija.Id = Id;
            kopija.Naziv = Naziv;
          //  kopija.AkcijskaCena = AkcijskaCena;
            kopija.Sifra = Sifra;
            kopija.TipNamestajaId = TipNamestajaId;
            kopija.Cena = Cena;
            kopija.KolicinaUMagacinu = KolicinaUMagacinu;

            return kopija;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"{ Naziv}, { Cena}, { TipNamestaja.GetById(TipNamestajaId).Naziv}";
        }

        public static Namestaj GetById(int id)
        {
            foreach (var namestaj in Projekat.Instance.Namestaji)
            {
                if (namestaj.Id == id)
                {
                    return namestaj;
                }
            }
            return null;

            //return Projekat.Instance.TipoviNamestaja.SingleOrDefault(x => x.Id == id) ;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Database
        public static ObservableCollection<Namestaj> GetAll()
        {
            var namestaj = new ObservableCollection<Namestaj>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Namestaj WHERE Obrisan = 0";

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet ds = new DataSet();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "Namestaj"); // izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    var n = new Namestaj();
                    n.Id = int.Parse(row["Id"].ToString());
                    n.Naziv = row["Naziv"].ToString();
                    n.Sifra = row["Sifra"].ToString();
                  //  n.AkcijskaCena = int.Parse(row["AkcijskaCena"].ToString());
                    n.TipNamestajaId = int.Parse(row["TipNamestajaId"].ToString());
                    n.Cena =  Double.Parse(row["Cena"].ToString());
                    n.KolicinaUMagacinu = int.Parse(row["KolicinaUMagacinu"].ToString());
                    //n.AkcijskaCena = int.Parse(row["AkcijskaCena"].ToString());
                    n.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    namestaj.Add(n);
                }
            }

            return namestaj;
        }

        public static Namestaj Create(Namestaj n)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO Namestaj (naziv, sifra, tipNamestajaId, cena, kolicinaUMagacinu, obrisan) VALUES (@Naziv, @Sifra, @TipNamestajaId, @Cena, @KolicinaUMagacinu, @Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();"; // metoda koja uzima identity poslednjeg upisanog elementa

                cmd.Parameters.AddWithValue("Naziv", n.Naziv);
               // cmd.Parameters.AddWithValue("AkcijaID", n.Akcija.Id);
                cmd.Parameters.AddWithValue("Sifra", n.Sifra);
                cmd.Parameters.AddWithValue("TipNamestajaId", n.TipNamestajaId);
                cmd.Parameters.AddWithValue("Cena", n.Cena);
                cmd.Parameters.AddWithValue("KolicinaUMagacinu", n.KolicinaUMagacinu);
                //cmd.Parameters.AddWithValue("AkcijskaCena", n.AkcijskaCena);
                cmd.Parameters.AddWithValue("Obrisan", n.Obrisan);

                int newId = int.Parse(cmd.ExecuteScalar().ToString()); // ExecuteScalar izvrsava query nad bazom
                n.Id = newId;
            }
            Projekat.Instance.Namestaji.Add(n); // Azuriram i model!

            return n;
        }

        public static void Update(Namestaj n)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Namestaj " +
                    "SET Naziv = @Naziv, Sifra = @Sifra, TipNamestajaId = @TipNamestajaId, Cena = @Cena, KolicinaUMagacinu = @KolicinaUMagacinu, Obrisan = @Obrisan  " +
                    "WHERE Id = @Id; ";

                cmd.Parameters.AddWithValue("Id", n.Id);
                cmd.Parameters.AddWithValue("Naziv", n.Naziv);
              //  cmd.Parameters.AddWithValue("AkcijaID", n.Akcija.Id);
                cmd.Parameters.AddWithValue("Sifra", n.Sifra);
                cmd.Parameters.AddWithValue("TipNamestajaId", n.TipNamestajaId);
                cmd.Parameters.AddWithValue("Cena", n.Cena);
                cmd.Parameters.AddWithValue("KolicinaUMagacinu", n.KolicinaUMagacinu);
                cmd.Parameters.AddWithValue("Obrisan", n.Obrisan);

                cmd.ExecuteNonQuery();

                // Azuriram model, listu u modelu
                // ISPRAVKA : Aj popravi imena tih varijabli, ne zna se ko koga ...
                foreach (var namestaj in Projekat.Instance.Namestaji)
                {
                    if (namestaj.Id == n.Id)
                    {
                        namestaj.Naziv = n.Naziv;
                      //  namestaj.IDAkcije = n.IDAkcije;

                        //mozda?
                      //  namestaj.Akcija = n.Akcija;
                        namestaj.Sifra = n.Sifra;
                        namestaj.TipNamestajaId = n.TipNamestajaId;
                        namestaj.Cena = n.Cena;
                        namestaj.KolicinaUMagacinu = n.KolicinaUMagacinu;
                        namestaj.Obrisan = n.Obrisan;
                        break;
                    }
                }
            }
        }

        public static void Delete(Namestaj n)
        {
            n.Obrisan = true;
            Update(n);
        }

        #endregion
    }
}
