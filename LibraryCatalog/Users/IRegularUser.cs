using LibraryCatalog.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCatalog.Users
{
    public interface IRegularUser
    {
        void ReserveBook(Book book, IRegularUser user);
        void TakeBook(Book book, IRegularUser user);
        void ReturnBook(Book book, IRegularUser user);
        void ShowTakenBooks(IRegularUser user);
        void ShowReservedBooks(IRegularUser user);
    }
}
