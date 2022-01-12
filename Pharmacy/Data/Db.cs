using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Pharmacy.Models;

namespace Pharmacy.Data
{
    public class Db
    {
        private List<Category> categories;
        private static Db data;
        private static string path1 = "HomePharmacy.xml";
        private static XmlSerializer formatter = new XmlSerializer(typeof(List<Category>));

        public List<Category> GetCategories
        {
            get
            {
                return categories;
            }
        }

        private Db(string path)
        {
            using (FileStream fs = new FileStream(path1, FileMode.OpenOrCreate))
            {
                categories = (List<Category>)formatter.Deserialize(fs);
            }
        }

        public Db()
        {
            categories = new List<Category>();
        }

        public Db GetInstance()
        {
            if (data == null)
            {
                data = new Db(path1);
                return data;
            }
            else return data;
        }
        public void SetData(List<Category> list)
        {

            if (File.Exists(path1))
            {
                using (FileStream fs = new FileStream(path1, FileMode.OpenOrCreate | FileMode.Truncate))
                {

                    formatter.Serialize(fs, list);
                }
            }
            else
            {
                using (FileStream fs = new FileStream(path1, FileMode.OpenOrCreate))
                {

                    formatter.Serialize(fs, list);
                }
            }
        }

    }
}
