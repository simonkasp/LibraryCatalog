using LibraryCatalog.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.Menu
{
    public class RegularMenu
    {
        public string Choice { get; set; }
        private IRegularUser _user, _regular;

        public RegularMenu(IRegularUser regular, IRegularUser user)
        {
            _regular = regular;
            _user = user;
        }

        public void Menu()
        {
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
                        Environment.Exit(0);
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
                        ReserveBook();
                        break;
                    case "5":
                        TakeBook();
                        break;
                    case "6":
                        ReturnBook();
                        break;
                    case "7":
                        ShowTakenBooks();
                        break;
                    case "8":
                        ShowReservedBooks();
                        break;
                }
            }
            while (Choice != "0");

        }
        public void DisplayBooks()
        {
            _regular.ShowBooks();
            Menu();
        }
        public void DisplayAvailableBooks()
        {
            _regular.ShowAvailableBooks();
            Menu();
        }

        public void SearchBookByName()
        {
            Console.Write("Enter name of book: ");
            var bookName = Console.ReadLine();

            _regular.SelectBookByName(bookName);
            Menu();
        }
        private void ReserveBook()
        {
            Console.Write("Enter book id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            _regular.ReserveBook(id, _user);
            Menu();
        }

        private void TakeBook()
        {
            Console.Write("Enter book id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            _regular.TakeBook(id, _user);
            Menu();
        }

        private void ReturnBook()
        {
            Console.Write("Enter book id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            _regular.ReturnBook(id, _user);
            Menu();
        }
        private void ShowTakenBooks()
        {
            _regular.ShowTakenBooks(_user);
            Menu();
        }
        private void ShowReservedBooks()
        {
            _regular.ShowReservedBooks(_user);
            Menu();
        }
    }
}
