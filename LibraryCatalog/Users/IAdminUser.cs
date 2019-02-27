using LibraryCatalog.Books;

namespace LibraryCatalog.Users
{
    public interface IAdminUser
    {
        void AddBook(Book book);
        void DeleteBook(Book book);
        void DeleteUser(IRegularUser user);
        void SelectUser(IRegularUser user);
        void SelectAllUsers();
    }
}