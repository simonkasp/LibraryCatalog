using LibraryCatalog.Database;
using LibraryCatalog.Users;
using System;

namespace LibraryCatalog.LoginRegister
{
    public class Register : LoginRegistrationValidation, IRegister
    {
        public Register(IMySQL database)
            :base(database)
        {

        }

        public IRegularUser CreateUser(string name, string lastName, string username, string password, string confirmPassword)
        {
            RegularUser user = new RegularUser(name, lastName, username, password);

            var query = "SELECT username FROM librarycatalog.users WHERE username=@username";

            if (CheckIfUsernameExists(query, username) == false)
                user.Name = username;
            else
                throw new ArgumentException("This user already exists.");

            if (CheckPasswords(password, confirmPassword) == true)
                user.Password = password;
            else
                throw new ArgumentException("Passwords do not match.");

            return user;
        }
        public void SignUp(IRegularUser user)
        {
            _database.AddDataUser(user);
        }

        private bool CheckPasswords(string password1, string password2)
        {
            return password1 == password2 ? true : false;
        }

    }
}
