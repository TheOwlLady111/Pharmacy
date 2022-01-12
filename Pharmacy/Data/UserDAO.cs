using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Models;

namespace Pharmacy.Data
{
    public class UserDAO
    {
        private DbUser _data;
        private List<User> _users;
        private User _user;

        public UserDAO()
        {
            _data = new DbUser();
            _users = new List<User>();
            _data = _data.GetInstance();
            _users = _data.GetUsers;

        }

        public void Save(List<User> list)
        {
            _data.SetDataUser(list);
        }
        public List<User> GetUsers
        {
            get
            {
                return _users;
            }
        }

        public User GetUser(string name)
        {
            foreach (User us in _users)
            {
                if (us.Name == name)
                {
                    return us;
                }
            }
            return null;
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Remove(User user)
        {
            _users.Remove(user);
        }

        public void AddMedItem(MedicineMeal item)
        {
            _user.PillsMeals.Add(item);
        }

        public void RemoveProduct(MedicineMeal item)
        {
            _user.PillsMeals.Remove(item);
        }
    }
}
