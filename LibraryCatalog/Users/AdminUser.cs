using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryCatalog.Books;
using LibraryCatalog.Database;

namespace LibraryCatalog.Users
{
    public class AdminUser : BaseUser, IAdminUser
    {

        public AdminUser(IMySQL database) :
            base(database)
        {

        }

        public AdminUser()
        {

        }

        public AdminUser(string username, string password)
            : base(username, password)
        {
            
        }

        public void AddBook(Book book)
        {
            _database.AddDataBook(book);
        }

        public void DeleteBook(Book book)
        {
            var query = "DELETE FROM librarycatalog.books WHERE id = @id;";

            _database.DeleteData(query, book.ID);
        }

        public void DeleteUser(int id)
        {
            var query = "DELETE FROM librarycatalog.users WHERE id = @id;";

            _database.DeleteData(query, id);
        }

        public void SelectAllUsers()
        {
            var query = "SELECT * FROM librarycatalog.users";
            _database.ShowDataAll(query);
        }

        public void SelectUser(int id)
        {
            var query = "SELECT * FROM librarycatalog.users WHERE id = @id;" +
                        "SELECT title from librarycatalog.users INNER " +
                        "JOIN librarycatalog.books " +
                        "ON librarycatalog.users.id = librarycatalog.books.takenByUserID;";

            _database.ShowDataOne(query, id);
        }
    }
}
