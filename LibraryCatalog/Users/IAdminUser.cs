using LibraryCatalog.Books;

namespace LibraryCatalog.Users
{
    public interface IAdminUser : IBaseUser
    {
        void AddBook(Book book);
        void DeleteBook(Book book);
        void DeleteUser(int id);
        void SelectUser(int id);
        void SelectAllUsers();
    }
}