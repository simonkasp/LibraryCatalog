using LibraryCatalog.Users;

namespace LibraryCatalog.LoginRegister
{
    public interface IRegister
    {
        RegularUser CreateUser(string name, string lastName, string username, string password, string confirmPassword);
        void SignUp(RegularUser user);
    }
}