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

        private RegularUser _user;

        public Application(IRegister register, ILogin login)
        {
            _register = register;
            _login = login;
        }

        public void Run()
        {
            Menu();
        }

        private void Menu()
        {
            string choice = "";

            do
            {
                LoginMenu();
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        Environment.Exit(0);
                        //DisplayPeople(_personProcessor.LoadPeople());
                        break;
                    case "1":
                        Register();
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        Console.WriteLine("Thanks for using this application");
                        break;
                    default:
                        Console.WriteLine("That was an invalid choice. Hit enter and try again.");
                        break;
                }

                Console.WriteLine("Hit return to continue...");
                Console.ReadLine();

            } while (choice != "q");
        }

        private void LoginMenu()
        {
            Console.Clear();
            Console.WriteLine("LOGIN MENU");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.Write("Please, enter your choice: ");
        }

        private void RegularMenu()
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
            
        }

        private void AdminMenu()
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
            Console.Write("Please, enter your choice: ");

        }

        private void Register()
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

            _user = _register.CreateUser(name, lastName, username, password, confirmPassword);
            _register.SignUp(_user);
        }

        private void Login()
        {
            Console.Write("Enter your username: ");
            var username = Console.ReadLine();

            Console.Write("Enter your password: ");
            var password = Console.ReadLine();

            var user = _login.SignIn(username, password);


            if (user.GetType() == typeof(RegularUser))
            {
                RegularMenu();
            }
            else
            {
                AdminMenu();
            }
        }

    }
}
