using LibraryCatalog.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCatalog.Users
{
    public interface IRegularUser : IBaseUser
    {
        void ReserveBook(int bookID, IRegularUser user);
        void TakeBook(int id, IRegularUser user);
        void ReturnBook(int id, IRegularUser user);
        void ShowTakenBooks(IRegularUser user);
        void ShowReservedBooks(IRegularUser user);
    }
}
