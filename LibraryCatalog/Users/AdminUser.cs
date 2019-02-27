using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryCatalog.Books;

namespace LibraryCatalog.Users
{
    public class AdminUser : BaseUser, IAdminUser
    {

        public AdminUser(string username, string password)
            : base(username, password)
        {

        }

        public void AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(IRegularUser user)
        {
            throw new NotImplementedException();
        }

        public void SelectAllUsers()
        {
            throw new NotImplementedException();
        }

        public void SelectUser(IRegularUser user)
        {
            throw new NotImplementedException();
        }
    }
}
