using LibraryCatalog.Users;

namespace LibraryCatalog.LoginRegister
{
    public interface ILogin
    {
        BaseUser SignIn(string username, string password);
    }
}