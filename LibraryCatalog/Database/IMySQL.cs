using LibraryCatalog.Books;
using LibraryCatalog.LoginRegister;
using LibraryCatalog.Users;

namespace LibraryCatalog.Database
{
    public interface IMySQL
    {
        bool CheckDataIfUsernameExists(string query, string data);
        bool CheckDataIfUserExists(string query, string username, string password);

        void AddDataUser<T>(T user) where T : IRegularUser;
        void AddDataBook(Book book);
        void DeleteData(string query, int id);
        void ShowDataUser(string query, int id);
        void ShowDataAll(string query);
        BaseUser GetDataUser(string username);

        void CheckInDataBook(int bookID, IRegularUser user);
        void ReserveDataBook(int bookId, IRegularUser user);
        void CheckOutDataBook(int bookID, IRegularUser user);
        void SelectDataRegularUserInfo(IRegularUser user);
        void SelectDataBooks(string query, string columnName, IRegularUser user);
        void SelectDataAvailableBooks(string query);
        void SelectDataBook(string bookName);
    }
}