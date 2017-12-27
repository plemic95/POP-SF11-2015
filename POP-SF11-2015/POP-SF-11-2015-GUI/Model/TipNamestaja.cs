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
    public class TipNamestaja: INotifyPropertyChanged
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

        //public bool Obrisan { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Naziv;
        }

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

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Database

        public static ObservableCollection<TipNamestaja> GetAll()
        {
            var tipNamestaja = new ObservableCollection<TipNamestaja>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM TipNamestaja WHERE Obrisan = 0";

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet ds = new DataSet();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "TipNamestaja"); // izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["TipNamestaja"].Rows)
                {
                    var tn = new TipNamestaja();
                    tn.Id = int.Parse(row["Id"].ToString());
                    tn.Naziv = row["Naziv"].ToString();
                    tn.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    tipNamestaja.Add(tn);
                }
            }

            return tipNamestaja;
        }

        public static TipNamestaja Create(TipNamestaja tn)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO TipNamestaja (naziv, obrisan) VALUES (@Naziv, @Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();"; // metoda koja uzima identity poslednjeg upisanog elementa

                cmd.Parameters.AddWithValue("Naziv", tn.Naziv);
                cmd.Parameters.AddWithValue("Obrisan", tn.Obrisan);

                int newId = int.Parse(cmd.ExecuteScalar().ToString()); // ExecuteScalar izvrsava query nad bazom
                tn.Id = newId;
            }
            Projekat.Instance.TipoviNamestaja.Add(tn); // Azuriram i model!

            return tn;
        }

        public static void Update(TipNamestaja tn)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE TipNamestaja " +
                    "SET Naziv = @Naziv, Obrisan = @Obrisan  " +
                    "WHERE Id = @Id; ";

                cmd.Parameters.AddWithValue("Id", tn.Id);
                cmd.Parameters.AddWithValue("Naziv", tn.Naziv);
                cmd.Parameters.AddWithValue("Obrisan", tn.Obrisan);

                cmd.ExecuteNonQuery();

                // Azuriram model, listu u modelu
                // ISPRAVKA : Aj popravi imena tih varijabli, ne zna se ko koga ...
                foreach (var tipNamestaja in Projekat.Instance.TipoviNamestaja)
                {
                    if (tipNamestaja.Id == tn.Id)
                    {
                        tipNamestaja.Naziv = tn.Naziv;
                        tipNamestaja.Obrisan = tn.Obrisan;
                        break;
                    }
                }
            }
        }

        public static void Delete(TipNamestaja tn)
        {
            tn.Obrisan = true;
            Update(tn);
        }

        #endregion
    }
}
