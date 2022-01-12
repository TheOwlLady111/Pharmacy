using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Pharmacy.Models;
using Pharmacy.Helpers;

namespace Pharmacy.Data
{
    public class DbUser
    {
        private List<User> users;
        private static DbUser data;
        private static string path1 = "Users.xml";
        private static XmlSerializer formatter = new XmlSerializer(typeof(List<User>));

        public List<User> GetUsers
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
            }
        }

        private DbUser(string path) : this()
        {
            if (File.Exists(path1))
            {
                using (FileStream fs = new FileStream(path1, FileMode.OpenOrCreate))
                {
                    users = (List<User>)formatter.Deserialize(fs);
                }
            }
        }

        public DbUser()
        {
            users = new List<User>();
        }

        public DbUser GetInstance()
        {
            if (data == null)
            {
                data = new DbUser(path1);
                return data;
            }
            else return data;
        }

        public void SetDataUser(List<User> users1)
        {
            

            if (File.Exists(path1))
            {
                using (FileStream fs = new FileStream(path1, FileMode.OpenOrCreate | FileMode.Truncate))
                {

                    formatter.Serialize(fs, users1);
                }
            }
            else
            {
                using (FileStream fs = new FileStream(path1, FileMode.OpenOrCreate))
                {

                    formatter.Serialize(fs, users1);
                }
            }
        }


    }
}
