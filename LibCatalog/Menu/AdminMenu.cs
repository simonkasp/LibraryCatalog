using LibraryCatalog.Books;
using LibraryCatalog.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.Menu
{
    public class AdminMenu
    {
        public string Choice { get; set; }
        private IAdminUser _adminUser, _admin;

        public AdminMenu(IAdminUser admin, IAdminUser adminUser)
        {
            _admin = admin;
            _adminUser = adminUser;
        }

        public void Menu()
        {
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
            MenuChoices();
        }

        private void MenuChoices()
        {
            do
            {
                switch (Choice)
                {
                    case "0":
                        Console.WriteLine("Thanks for using this application");
                        break;
                    case "1":
                        DisplayBooks();
                        break;
                    case "2":
                        DisplayAvailableBooks();
                        break;
                    case "3":
                        SearchBookByName();
                        break;
                    case "4":
                        AddBook();
                        break;
                    case "5":
                        DeleteBook();
                        break;
                    case "6":
                        ShowAllUsers();
                        break;
                    case "7":
                        ShowUser();
                        break;
                    case "8":
                        DeleteUser();
                        break;
                }
            }
            while (Choice != "0");

        }
        public void DisplayBooks()
        {
            _admin.ShowBooks();
            Menu();
        }
        public void DisplayAvailableBooks()
        {
            _admin.ShowAvailableBooks();
        }

        public void SearchBookByName()
        {
            Console.Write("Enter name of book: ");
            var bookName = Console.ReadLine();

            _admin.SelectBookByName();

            Menu();
        }

        public void AddBook()
        {
            Console.WriteLine("Book info");
            Console.Write("Enter book name: ");
            var title = Console.ReadLine();

            Console.Write("Enter number of pages: ");
            var numOfPages = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter ISBN: ");
            var ISBN = Console.ReadLine();

            var book = new Book(title, numOfPages, ISBN);

            _admin.AddBook(book);

            Menu();
        }
        public void DeleteBook()
        {
            Console.Write("Enter ID of book: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var book = new Book(id);
            _admin.DeleteBook(book);

            Menu();
        }
        public void ShowAllUsers()
        {
            _admin.SelectAllUsers();

            Menu();
        }
        public void ShowUser()
        {
            Console.Write("Enter ID of user: ");
            var id = Convert.ToInt32(Console.ReadLine());

            _admin.SelectUser(id);

            Menu();
        }
        public void DeleteUser()
        {
            Console.Write("Enter ID of user: ");
            int id = Convert.ToInt32(Console.ReadLine());

            _admin.DeleteUser(id);

            Menu();
        }
    }
}
