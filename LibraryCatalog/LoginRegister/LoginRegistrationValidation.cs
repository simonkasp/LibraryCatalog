using LibraryCatalog.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCatalog.LoginRegister
{
    public class LoginRegistrationValidation
    {
        protected readonly IMySQL _database;
        protected enum DataType { username, password };

        public LoginRegistrationValidation(IMySQL database)
        {
            _database = database;
        }

        protected bool CheckIfDataExists(string query, string data)
        {          
            return _database.CheckDataIfExists(query, data);
        }
    }
}
