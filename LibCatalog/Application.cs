using ConsoleUI.Menu;
using LibraryCatalog.Books;
using LibraryCatalog.Database;
using LibraryCatalog.LoginRegister;
using LibraryCatalog.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class Application
    {
        private readonly IRegister _register;
        private readonly ILogin _login;

        private IRegularUser _regularUser, _regularFunction;
        private IAdminUser _adminUser, _adminFunction;
        public IBaseUser _baseUser { get; set; }
        private BaseUser _user;

        public string Choice { get; set; }

        public Application()
        {

        }

        public Application(IRegister register, ILogin login, IBaseUser baseUser, IAdminUser admin, IRegularUser regular)
        {
            _register = register;
            _login = login;
            _baseUser = baseUser;
            _adminFunction = admin;
            _regularFunction = regular;

        }

        public void Run()
        {
            Menu();
        }


        public void LoginMenu()
        {

            Console.Clear();
            Console.WriteLine("LOGIN MENU");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.Write("Please, enter your Choice: ");
            Choice = Console.ReadLine();
        }
        public void AdminMenu()
        {
            Console.Clear();
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Display books");
            Console.WriteLine("2. Display available books");
            Console.WriteLine("3. Search book by name");
            Console.WriteLine("4. Add book");
            Console.WriteLine("5. Delete book");
            Console.WriteLine("6. Show all users");
            Console.WriteLine("7. Show user");
            Console.WriteLine("8. Delete user");
            Console.Write("Please, enter your Choice: ");
            Choice = Console.ReadLine();
        }

        public void RegularMenu()
        {
            Console.Clear();
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Display books");
            Console.WriteLine("2. Display available books");
            Console.WriteLine("3. Search book by name");
            Console.WriteLine("4. Reserve book");
            Console.WriteLine("5. Take book");
            Console.WriteLine("6. Return book");
            Console.WriteLine("7. Show taken books");
            Console.WriteLine("8. Show reserved books");
            Console.Write("Please, enter your choice: ");
            Choice = Console.ReadLine();
        }

        private void Menu()
        {
            LoginMenu();

            do
            {

                if (Choice == "0")
                {
                    Console.WriteLine("Thanks for using this application");
                }
                else if(Choice == "1")
                {
                    Register();
                }

                else if(Choice == "2")
                {
                    Login();
                }

            }
            while (Choice != "0");

        }

        public void Register()
        {
            Console.Write("Enter your name: ");
            var name = Console.ReadLine();

            Console.Write("Enter your last name: ");
            var lastName = Console.ReadLine();

            Console.Write("Enter your username: ");
            var username = Console.ReadLine();

            Console.Write("Enter your password: ");
            var password = Console.ReadLine();

            Console.Write("Confirm password: ");
            var confirmPassword = Console.ReadLine();

            _regularUser = _register.CreateUser(name, lastName, username, password, confirmPassword);
            _register.SignUp(_regularUser);
        }

        public void Login()
        {
            Console.Write("Enter your username: ");
            var username = Console.ReadLine();

            Console.Write("Enter your password: ");
            var password = Console.ReadLine();

            _user = _login.SignIn(username, password);

            if (_user.Role == 0)
            {
                _regularUser = Authentication<RegularUser>(_user);
                var menu = new RegularMenu(_regularFunction, _regularUser);
                menu.Menu();
            }
            else
            {
                _adminUser = Authentication<AdminUser>(_user);
                var menu = new AdminMenu(_adminFunction, _adminUser);
                menu.Menu();
            }
        }

        private bool UserExists<T>(T user) where T : BaseUser
        {
            if (user != null && user.ID != 0 && user.Name != null && user.LastName != null &&
                user.Username != null && user.Password != null && user.DateRegistred != null)
            {
                return true;
            }             
            else
                return false;
        }

        private T Authentication<T>(BaseUser user) where T : BaseUser, new()
        {
            T newUser = new T();

            newUser.ID = _user.ID;
            newUser.Name = _user.Name;
            newUser.LastName = _user.LastName;
            newUser.Username = _user.Username;
            newUser.Password = _user.Password;
            newUser.DateRegistred = _user.DateRegistred;
            newUser.Role = _user.Role;
            newUser.IsLoggedIn = _user.IsLoggedIn;

            return newUser;
        }

    }
}
