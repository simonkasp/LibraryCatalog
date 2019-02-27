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

        public IBaseUser SignIn(string username, string password)
        {
            IBaseUser user;           

            var query1 = "SELECT username FROM librarycatalog.users WHERE username=@username";
            var query2 = "SELECT password FROM librarycatalog.users WHERE username=@username";

            if (CheckIfDataExists(query1, username) == true && CheckIfDataExists(query2, password) == true)
            {
                var role = CheckUserRole(username);

                switch (role)
                {
                    case 0:
                        return user = new RegularUser(username, password);
                    default:
                        return user = new AdminUser(username, password);
                }

            }
            else
            {
                throw new ArgumentException("Username or password is incorrect.");
            }
        }

        private int CheckUserRole(string username)
        {
            return _database.CheckUserRole(username);
        }

    }
}
