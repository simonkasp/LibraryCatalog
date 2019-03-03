using LibraryCatalog.Database;
using LibraryCatalog.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCatalog.LoginRegister
{
    public class Login : LoginRegistrationValidation, ILogin
    {
        

        public Login(IMySQL database)
            : base(database)
        {

        }

        public BaseUser SignIn(string username, string password)
        {
            BaseUser user = new BaseUser();

            var query1 = "SELECT username FROM librarycatalog.users WHERE username=@username";
            var query2 = "SELECT password FROM librarycatalog.users WHERE username=@username";

            if (CheckIfDataExists(query1, username) == true && CheckIfDataExists(query2, password) == true)
            {
                return user = _database.GetDataUser(username);
            }
            else
            {
                throw new ArgumentException("Username or password is incorrect.");
            }
        }

    }
}
