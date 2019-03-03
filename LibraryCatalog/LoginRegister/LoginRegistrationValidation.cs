using LibraryCatalog.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibraryCatalog.LoginRegister
{
    public class LoginRegistrationValidation : ILoginRegistrationValidation
    {
        protected readonly IMySQL _database;

        public LoginRegistrationValidation(IMySQL database)
        {
            _database = database;
        }

        public bool CheckIfUsernameExists(string query, string data)
        {
            return _database.CheckDataIfUsernameExists(query, data);
        }

        public bool CheckIfUserExists(string query, string username, string password)
        {
            return _database.CheckDataIfUserExists(query, username, password);
        }
    }
}
