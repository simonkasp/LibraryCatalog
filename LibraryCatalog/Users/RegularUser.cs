using LibraryCatalog.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCatalog.Users
{
    public class RegularUser : BaseUser, IRegularUser
    {
        public RegularUser()
        {

        }

        public RegularUser(string username, string password)
            :base(username, password)
        {

        }

        public RegularUser (string name, string lastName, string username, string password)
        {
            Name = name;
            LastName = lastName;
            Username = username;
            Password = password;
        }

        public void ReserveBook(Book book, IRegularUser user)
        {
            throw new NotImplementedException();
        }

        public void ReturnBook(Book book, IRegularUser user)
        {
            throw new NotImplementedException();
        }

        public void ShowReservedBooks(IRegularUser user)
        {
            throw new NotImplementedException();
        }

        public void ShowTakenBooks(IRegularUser user)
        {
            throw new NotImplementedException();
        }

        public void TakeBook(Book book, IRegularUser user)
        {
            throw new NotImplementedException();
        }
    }
}
