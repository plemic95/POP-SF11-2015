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
    public class Prodaja: INotifyPropertyChanged

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

        private DateTime datumProdaje;

        public DateTime DatumProdaje
        {
            get { return datumProdaje; }
            set
            {
                datumProdaje = value;
                OnPropertyChanged("DatumProdaje");
            }
        }

        private string brojRacuna;

        public string BrojRacuna
        {
            get { return brojRacuna; }
            set
            {
                brojRacuna = value;
                OnPropertyChanged("BrojRacuna");
            }
        }

        private string kupac;

        public string Kupac
        {
            get { return kupac; }
            set
            {
                kupac = value;
                OnPropertyChanged("Kupac");
            }
        }

        private double ukupnaCena;

        public double UkupnaCena
        {
            get { return ukupnaCena; }
            set
            {
                ukupnaCena = value;
                OnPropertyChanged("UkupnaCena");
            }
        }

        private List<Namestaj> namestajzaprodaju;


        public List<Namestaj> NamestajZaProdaju
        {
            get
            {
                namestajzaprodaju = new List<Namestaj>();
                return namestajzaprodaju;
            }
            set
            {
                namestajzaprodaju = value;
                OnPropertyChanged("NamestajZaProdaju");
            }
        }
        private List<DodatnaUsluga> dodatneusluge;
        public List<DodatnaUsluga> DodatnaUsluga
        {
            get
            {
                dodatneusluge = new List<DodatnaUsluga>();
                return dodatneusluge;
            }
            set
            {
                dodatneusluge = value;
                OnPropertyChanged("DodatneUsluge");
            }
        }


        public object Clone()
        {
            Prodaja kopija = new Prodaja();
            kopija.Id = Id;
            kopija.DatumProdaje = DatumProdaje;
            kopija.BrojRacuna = BrojRacuna;
            kopija.Kupac = Kupac;
            kopija.UkupnaCena = UkupnaCena;
            kopija.NamestajZaProdaju = NamestajZaProdaju;
            kopija.DodatnaUsluga = DodatnaUsluga;
            return kopija;
        }


        public const double PDV = 0.02;


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Database

        public static ObservableCollection<Prodaja> GetAll()
        {
            var prodaja = new ObservableCollection<Prodaja>();

            //tu je ona referenca koju smo otkacili
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {

                SqlCommand cmd = con.CreateCommand();
                //obrisan true 1 false 0
                cmd.CommandText = "SELECT * FROM Prodaja";


                //ds i adapter samo kada dobavljamo podatke
                SqlDataAdapter adapter = new SqlDataAdapter();

                //objekat koji moze u sebi da ima vise logickih tabela, objekat u memoriji koji reprezentuje bazu
                DataSet ds = new DataSet();

                //smesti u data set pod nazivom tabele tipNamestaja
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "Prodaja"); // izvrsava se query nad bazom

                //za svaki red u data setu u tabelama tim i tim
                foreach (DataRow row in ds.Tables["Prodaja"].Rows)
                {
                    var rac = new Prodaja();
                    //svi su tipa object, zato se radi ToStrint()
                    rac.Id = int.Parse(row["Id"].ToString());
                    rac.DatumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                    rac.BrojRacuna = row["BrojRacuna"].ToString();
                    rac.Kupac = row["Kupac"].ToString();
                    rac.UkupnaCena = double.Parse(row["Cena"].ToString());
                    //napuni u svakom prolazu puni kontejnersku kolekciju
                    prodaja.Add(rac);
                }
            }

            //vrati kolekciju
            return prodaja;
        }

        public static Prodaja Create(Prodaja prodaja)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                //otvori konekciju
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                //@naziv, @obrisan za sql injection, da se ne izvrsava kao poseban upit
                cmd.CommandText = $"INSERT INTO Prodaja (DatumProdaje, BrojRacuna, Kupac, Cena) VALUES (@DatumProdaje, @BrojRacuna, @Kupac, @Cena);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();"; // metoda koja vrati id poslednjeg upisanog elementa

                cmd.Parameters.AddWithValue("DatumProdaje", prodaja.DatumProdaje);
                cmd.Parameters.AddWithValue("BrojRacuna", prodaja.BrojRacuna);
                cmd.Parameters.AddWithValue("Kupac", prodaja.Kupac);
                cmd.Parameters.AddWithValue("Cena", prodaja.UkupnaCena);

                //upises u bazu, ali ga vratis da dobijes id da mozes dalje da listas??? vidi to kako 
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); // ExecuteScalar izvrsava query nad bazom
                prodaja.Id = newId;
            }

            //da se doda odma i observable collection, da ne bi bilo drugo stanje, da se doda u bazu a ne prikaze i tako dalje
            //Instance.Prodaja.Add(prodaja); // Azuriram i model!

            return prodaja;
        }


        #endregion
    }
}
