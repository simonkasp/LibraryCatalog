using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCatalog.Books
{
    public class Book
    {
        private string title;
        private int numberOfPages;

        public int ID { get; set; }
        public string ISBN { get; set; }
        public bool IsCheckedOut { get; set; }
        public bool IsReserved { get; set; }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value == "")
                {
                    throw new ArgumentNullException("Title can't be empty.");
                }

                title = value;
            }
        }

        public int NumberOfPages
        {
            get
            {
                return numberOfPages;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Number of pages must be higher than 0.");
                }
                numberOfPages = value;
            }
        }
        public Book(int id)
        {
            ID = id;
        }
        public Book(string title, int numberOfPages, string isbn)
        {
            Title = title;
            NumberOfPages = numberOfPages;
            ISBN = isbn;
        }

        public override string ToString()
        {
            return
                $"Title: {Title} \n" +
                $"Number of pages: {NumberOfPages} \n" +
                $"ISBN: {ISBN} \n" +
                $"Available: {IsCheckedOut} \n" +
                $"Reserved: {IsReserved}";
        }
    }
}
