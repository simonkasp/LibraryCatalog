using LibraryCatalog.Books;
using LibraryCatalog.LoginRegister;
using LibraryCatalog.Users;

namespace LibraryCatalog.Database
{
    public interface IMySQL
    {
        void AddDataBook(Book book);
        bool CheckDataIfExists(string query, string data);
        void AddDataUser<T>(T user) where T : IBaseUser;
        int CheckUserRole(string username);

    }
}