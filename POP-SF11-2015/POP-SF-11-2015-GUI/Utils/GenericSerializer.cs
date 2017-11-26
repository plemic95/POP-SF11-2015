using POP_SF11_2015.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_SF11_2015.Utils
{
    public class GenericSerializer
    {
        public static ObservableCollection<T> Deserialize<T>(string fileName) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<T>));
                using (var sr = new StreamReader($@"../../Data/{ fileName}"))
                {
                    return (ObservableCollection<T>)serializer.Deserialize(sr);
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Greska prilkom ucitavanja datoteke: {fileName} sa diska");
            }
        }

        public static void Serialize<T>(string fileName, ObservableCollection<T> listToSerialize) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<T>));
                using (var sr = new StreamWriter($@"../../Data/{ fileName}"))
                {
                    serializer.Serialize(sr, listToSerialize);
                };
            }
            catch (Exception)
            {
                throw new Exception($"Greska prilkom upisivanja datoteke: {fileName} na disk");
            }
        }
    }
}