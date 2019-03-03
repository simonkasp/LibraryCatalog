using LibraryCatalog.Books;
using LibraryCatalog.LoginRegister;
using LibraryCatalog.Users;

namespace LibraryCatalog.Database
{
    public interface IMySQL
    {
        bool CheckDataIfExists(string query, string data);

        void AddDataUser<T>(T user) where T : IRegularUser;
        void AddDataBook(Book book);
        void DeleteData(string query, int id);
        void ShowDataOne<T>(string query, T item);
        void ShowDataAll(string query);
        BaseUser GetDataUser(string username);
    }
}