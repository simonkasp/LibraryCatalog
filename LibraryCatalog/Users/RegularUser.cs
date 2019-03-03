using LibraryCatalog.Books;
using LibraryCatalog.Database;
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

        public RegularUser(IMySQL database)
            :base(database)
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

        public void ReserveBook(int bookID, IRegularUser user)
        {
            _database.ReserveDataBook(bookID, user);
        }

        public void ReturnBook(int bookID, IRegularUser user)
        {
            _database.CheckInDataBook(bookID, user);
        }

        public void ShowReservedBooks(IRegularUser user)
        {
            
            var query = "SELECT id, title, numberOfPages, ISBN FROM librarycatalog.books WHERE reservedByUserID = @reservedByUserID;";

            _database.SelectDataBooks(query, "reservedByUserID", user);
        }

        public void ShowTakenBooks(IRegularUser user)
        {
            var query = "SELECT id, title, numberOfPages, ISBN FROM librarycatalog.books WHERE takenByUserID = @takenByUserID;";

            _database.SelectDataBooks(query, "takenByUserID", user);
        }

        public void TakeBook(int bookID, IRegularUser user)
        {
            _database.CheckOutDataBook(bookID, user);
        }
    }
}
