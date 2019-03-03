using LibraryCatalog.Database;
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
        public string DateRegistred { get; set; }
        public int Role { get; set; }
        public bool IsLoggedIn { get; set; }

        protected readonly IMySQL _database;

        public BaseUser(IMySQL database)
        {
            _database = database;
        }

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
            var query = "";
            _database.ShowDataAll(query);
        }

        public void ShowBooks()
        {
            var query = "SELECT id, title, numberOfPages, ISBN, isCheckedOut, isReserved FROM librarycatalog.books";

            _database.ShowDataAll(query);
        }
    }
}
