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

namespace POP_SF11_2015.Model
{
    public class Akcija: INotifyPropertyChanged
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

        private string imeAkcije;

        public string ImeAkcije
        {
            get { return imeAkcije; }
            set
            {
                imeAkcije = value;
                OnPropertyChanged("ImeAkcije");
            }
        }

        private decimal popust;

        public decimal Popust
        {
            get { return popust; }
            set
            {
                popust = value;
                OnPropertyChanged("Popust");
            }
        }


        private DateTime datumPocetka;

        public DateTime DatumPocetka
        {
            get { return datumPocetka; }
            set
            {
                datumPocetka = value;
                OnPropertyChanged("DatumPocetka");
            }
        }

        private DateTime datumZavrsetka;

        public DateTime DatumZavrsetka
        {
            get { return datumZavrsetka; }
            set
            {
                datumZavrsetka = value;
                OnPropertyChanged("DatumZavrsteka");
            }
        }

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

        public object Clone()
        {
            Akcija kopija = new Akcija();
            kopija.Id = Id;
            kopija.ImeAkcije = ImeAkcije;
            kopija.Popust = Popust;
            kopija.DatumPocetka = DatumPocetka;
            kopija.DatumZavrsetka = DatumZavrsetka;
            return kopija;
        }

        public static Akcija GetById(int id)
        {
            foreach (var akcija in Projekat.Instance.Akcije)
            {
                if (akcija.Id == id)
                {
                    return akcija;
                }
            }
            return null;

            //return Projekat.Instance.TipoviNamestaja.SingleOrDefault(x => x.Id == id) ;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Database
        public static ObservableCollection<Akcija> GetAll()
        {
            var akcije = new ObservableCollection<Akcija>();

            //tu je ona referenca koju smo otkacili
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {

                SqlCommand cmd = con.CreateCommand();
                //obrisan true 1 false 0
                cmd.CommandText = "SELECT * FROM Akcije WHERE Obrisan = 0";


                //ds i adapter samo kada dobavljamo podatke
                SqlDataAdapter adapter = new SqlDataAdapter();

                //objekat koji moze u sebi da ima vise logickih tabela, objekat u memoriji koji reprezentuje bazu
                DataSet ds = new DataSet();


                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "Akcije"); // izvrsava se query nad bazom

                //za svaki red u data setu u tabelama tim i tim
                foreach (DataRow row in ds.Tables["Akcije"].Rows)
                {
                    var akc = new Akcija();
                    //svi su tipa object, zato se radi ToStrint()
                    akc.Id= int.Parse(row["Id"].ToString());
                    akc.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    akc.ImeAkcije = row["ImeAkcije"].ToString();
                    akc.Popust = Int32.Parse(row["Popust"].ToString());
                    akc.DatumPocetka = DateTime.Parse(row["DatumPocetka"].ToString());
                    akc.DatumZavrsetka = DateTime.Parse(row["DatumZavrsetka"].ToString());
                    //napuni u svakom prolazu puni kontejnersku kolekciju
                    akcije.Add(akc);
                }
            }

            //vrati kolekciju
            return akcije;
        }

        public static Akcija Create(Akcija akc)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                //otvori konekciju
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                //@naziv, @obrisan za sql injection, da se ne izvrsava kao poseban upit
                cmd.CommandText = $"INSERT INTO Akcije (Obrisan, ImeAkcije, Popust, DatumPocetka, DatumZavrsetka) VALUES (@Obrisan, @ImeAkcije, @Popust, @DatumPocetka, @DatumZavrsetka);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();"; // metoda koja vrati id poslednjeg upisanog elementa

                cmd.Parameters.AddWithValue("Obrisan", akc.Obrisan);
                cmd.Parameters.AddWithValue("ImeAkcije", akc.ImeAkcije);
                cmd.Parameters.AddWithValue("Popust", akc.Popust);
                cmd.Parameters.AddWithValue("DatumPocetka", akc.DatumPocetka);
                cmd.Parameters.AddWithValue("DatumZavrsetka", akc.DatumZavrsetka);


                //upises u bazu, ali ga vratis da dobijes id da mozes dalje da listas??? vidi to kako 
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); // ExecuteScalar izvrsava query nad bazom
                akc.Id = newId;
            }

            //da se doda odma i observable collection, da ne bi bilo drugo stanje, da se doda u bazu a ne prikaze i tako dalje
            Projekat.Instance.Akcije.Add(akc);// Azuriram i model!

            return akc;
        }

        public static void Update(Akcija akc)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Akcije " +
                    "SET ImeAkcije = @ImeAkcije, " +
                    "Obrisan = @Obrisan, " +
                    "Popust = @Popust,  " +
                    "DatumPocetka = @DatumPocetka,  " +
                    "DatumZavrsetka = @DatumZavrsetka  " +

                    "WHERE ID = @ID; ";

                cmd.Parameters.AddWithValue("ID", akc.Id);
                cmd.Parameters.AddWithValue("ImeAkcije", akc.ImeAkcije);
                cmd.Parameters.AddWithValue("Obrisan", akc.Obrisan);
                cmd.Parameters.AddWithValue("Popust", akc.Popust);
                cmd.Parameters.AddWithValue("DatumPocetka", akc.DatumPocetka);
                cmd.Parameters.AddWithValue("DatumZavrsetka", akc.DatumZavrsetka);

                cmd.ExecuteNonQuery();

                // Azuriram model, listu u modelu
                foreach (var akcija in Projekat.Instance.Akcije)
                {
                    if (akcija.Id == akc.id)
                    {
                        akcija.ImeAkcije = akc.ImeAkcije;
                        akcija.Obrisan = akc.Obrisan;
                        akcija.Popust = akc.Popust;
                        akcija.DatumPocetka = akc.DatumPocetka;
                        akcija.DatumZavrsetka = akc.DatumZavrsetka;
                        break;
                    }
                }
            }
        }

        public static void Delete(Akcija akc)
        {

            akc.Obrisan = true;
            Update(akc);
            //using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            //{
            //    con.Open();

            //    SqlCommand cmd1 = con.CreateCommand();
            //    SqlCommand cmd2 = con.CreateCommand();

            //    cmd1.CommandText = "UPDATE Namestaj SET AkcijaID = 1 WHERE AkcijaID = @AkcijaID;";
            //    cmd2.CommandText = "UPDATE Akcije SET Obrisan = 1 WHERE ID = @AkcijaID;";


            //    cmd1.Parameters.AddWithValue("AkcijaID", akc.Id);
            //    cmd2.Parameters.AddWithValue("AkcijaID", akc.Id);


            //    cmd1.ExecuteNonQuery();
            //    cmd2.ExecuteNonQuery();


            //    foreach (Namestaj nam in Projekat.Instance.Namestaji)
            //    {
            //        if (nam.IDAkcije == akc.Id)
            //        {
            //            nam.IDAkcije = 1;
            //        }
            //    }

            //    //azuriraj i model:
            //    foreach (Akcija akcija in Projekat.Instance.Akcije)
            //    {
            //        if (akcija.Id == akc.Id)
            //        {
            //            akc.Obrisan = true;
            //        }
            //    }








            //}
            #endregion
        }
    }
}
