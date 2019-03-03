using System.ComponentModel;

namespace LibraryCatalog.Users
{
    public interface IBaseUser
    {
        int ID { get; set; }
        string Name { get; set; }
        string LastName { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string DateRegistred { get; set; }
        int Role { get; set; }
        [DefaultValue(false)]
        bool IsLoggedIn { get; set; } 

        void ShowBooks();
        void ShowAvailableBooks();
        void SelectBookByName(string bookName);

    }
}