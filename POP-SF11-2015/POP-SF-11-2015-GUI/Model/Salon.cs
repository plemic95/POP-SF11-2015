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
    public class Salon: INotifyPropertyChanged
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

        private string adresa;

        public string Adresa
        {
            get { return adresa; }
            set
            {
                adresa = value;
                OnPropertyChanged("Adresa");
            }
        }

        private string telefon;

        public string Telefon
        {
            get { return telefon; }
            set
            {
                telefon = value;
                OnPropertyChanged("Telefon");
            }
        }


        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }


        private string webSajt;

        public string WebSajt
        {
            get { return webSajt; }
            set
            {
                webSajt = value;
                OnPropertyChanged("WebSajt");
            }
        }

        private int pib;

        public int PIB
        {
            get { return pib; }
            set
            {
                pib = value;
                OnPropertyChanged("PIB");
            }
        }

        private int maticniBroj;

        public int MaticniBroj
        {
            get { return maticniBroj; }
            set
            {
                maticniBroj = value;
                OnPropertyChanged("MaticniBroj");
            }
        }

        private string brojZiroRacuna;

        public string BrojZiroRacuna
        {
            get { return brojZiroRacuna; }
            set
            {
                brojZiroRacuna = value;
                OnPropertyChanged("BrojZiroRacuna");
            }
        }

        public object Clone()
        {
            Salon kopija = new Salon();
            kopija.Id = Id;
            kopija.Naziv = Naziv;
            kopija.Adresa = Adresa;
            kopija.Telefon = Telefon;
            kopija.Email = Email;
            kopija.WebSajt = WebSajt;
            kopija.PIB = PIB;
            kopija.MaticniBroj = MaticniBroj;
            kopija.BrojZiroRacuna = BrojZiroRacuna;
            return kopija;
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
        public static ObservableCollection<Salon> GetAll()
        {
            var salon = new ObservableCollection<Salon>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Salon";

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet ds = new DataSet();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "Salon"); // izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["Salon"].Rows)
                {

                    var s = new Salon();

                    s.Naziv = row["Naziv"].ToString();
                    s.Adresa = row["Adresa"].ToString();
                    s.Telefon = row["Telefon"].ToString();
                    s.Email = row["Email"].ToString();
                    s.WebSajt = row["WebSajt"].ToString();
                    s.PIB = int.Parse(row["PIB"].ToString());
                    s.MaticniBroj = int.Parse(row["MaticniBroj"].ToString());
                    s.BrojZiroRacuna = row["BrojZiroRacuna"].ToString();
                    salon.Add(s);
                }
            }

            return salon;
        }

        public static void Update(Salon s)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Salon " +
                    "SET Naziv = @Naziv, Adresa = @Adresa, Telefon = @Telefon, Email = @Email, WebSajt = @WebSajt, PIB = @PIB, MaticniBroj = @MaticniBroj, BrojZiroRacuna = @BrojZiroRacuna  "
                    ;

                cmd.Parameters.AddWithValue("Naziv", s.Naziv);
                cmd.Parameters.AddWithValue("Adresa", s.Adresa);
                cmd.Parameters.AddWithValue("Telefon", s.Telefon);
                cmd.Parameters.AddWithValue("Email", s.Email);
                cmd.Parameters.AddWithValue("WebSajt", s.WebSajt);
                cmd.Parameters.AddWithValue("PIB", s.PIB);
                cmd.Parameters.AddWithValue("MaticniBroj", s.MaticniBroj);
                cmd.Parameters.AddWithValue("BrojZiroRacuna", s.BrojZiroRacuna);

                cmd.ExecuteNonQuery();

                foreach (var salon in Projekat.Instance.SalonNamestaja)
                {
                    if (salon.Naziv == s.Naziv)
                    {
                        salon.Naziv = s.Naziv;
                        salon.Adresa = s.Adresa;
                        salon.Telefon = s.Telefon;
                        salon.Email = s.Email;
                        salon.WebSajt = s.WebSajt;
                        salon.PIB = s.PIB;
                        salon.MaticniBroj = s.MaticniBroj;
                        salon.BrojZiroRacuna = s.BrojZiroRacuna;
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
