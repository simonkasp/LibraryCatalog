using LibraryCatalog.Users;

namespace LibraryCatalog.LoginRegister
{
    public interface IRegister
    {
        IRegularUser CreateUser(string name, string lastName, string username, string password, string confirmPassword);
        void SignUp(IRegularUser user);
    }
}