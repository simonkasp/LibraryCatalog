using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCatalog.Users
{
    public class BaseUser : IBaseUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public BaseUser()
        {

        }

        public BaseUser(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void SelectBookByName()
        {
            throw new NotImplementedException();
        }

        public void ShowAvailableBooks()
        {
            throw new NotImplementedException();
        }

        public void ShowBooks()
        {
            throw new NotImplementedException();
        }
    }
}
