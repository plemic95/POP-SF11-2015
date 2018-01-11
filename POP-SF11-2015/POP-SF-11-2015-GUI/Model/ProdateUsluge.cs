using POP_SF11_2015.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_11_2015_GUI.Model
{
    class ProdateUsluge
    {
        public static DodatnaUsluga Create(DodatnaUsluga du, Prodaja rac)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                //otvori konekciju
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                //@naziv, @obrisan za sql injection, da se ne izvrsava kao poseban upit
                cmd.CommandText = $"INSERT INTO ProdateUsluge (IDProdaje, IDDodatneUsluge) VALUES (@IDProdaje, @IDDodatneUsluge);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();"; // metoda koja vrati id poslednjeg upisanog elementa

                cmd.Parameters.AddWithValue("IDRacuna", rac.Id);
                cmd.Parameters.AddWithValue("IDDodatneUsluge", du.Id);


                cmd.ExecuteScalar();
                /*
                //upises u bazu, ali ga vratis da dobijes id da mozes dalje da listas??? vidi to kako 
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); // ExecuteScalar izvrsava query nad bazom
                du.ID = newId;
                */
            }

            //da se doda odma i observable collection, da ne bi bilo drugo stanje, da se doda u bazu a ne prikaze i tako dalje
            //Projekat.Instance.dodatneusluge.Add(du);// Azuriram i model!
            rac.DodatnaUsluga.Add(du);
            return du;
        }

        //funkcija koja ce vaditi za odredjeni racun njene prodate namestaje
        //koristi ovaj upit:
        //SELECT * FROM DodatneUsluge WHERE ID IN (SELECT IDDodatneUsluge FROM ProdateUsluge WHERE IDRacuna = 8) AND Obrisan != 1;
        public static ObservableCollection<DodatnaUsluga> GetAll(Prodaja rac)
        {
            var usluge = new ObservableCollection<DodatnaUsluga>();

            //tu je ona referenca koju smo otkacili
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {

                SqlCommand cmd = con.CreateCommand();
                //obrisan true 1 false 0
                cmd.CommandText = "SELECT * FROM DodatnaUsluga WHERE Id IN (SELECT IDDodatneUsluge FROM ProdateUsluge WHERE IDProdaje = @IDProdaje);";
                cmd.Parameters.AddWithValue("IDProdaje", rac.Id);

                //ds i adapter samo kada dobavljamo podatke
                SqlDataAdapter adapter = new SqlDataAdapter();

                //objekat koji moze u sebi da ima vise logickih tabela, objekat u memoriji koji reprezentuje bazu
                DataSet ds = new DataSet();

                //smesti u data set pod nazivom tabele tipNamestaja
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "DodatnaUsluga"); // izvrsava se query nad bazom

                //za svaki red u data setu u tabelama tim i tim
                foreach (DataRow row in ds.Tables["DodatnaUsluga"].Rows)
                {
                    var du = new DodatnaUsluga();
                    //svi su tipa object, zato se radi ToStrint()
                    du.Id = int.Parse(row["Id"].ToString());
                    du.Naziv = row["Naziv"].ToString();
                    du.Cena = Double.Parse(row["Cena"].ToString());
                    du.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    //napuni u svakom prolazu puni kontejnersku kolekciju
                    usluge.Add(du);
                }
            }

            //vrati kolekciju
            return usluge;
        }
    }
}
