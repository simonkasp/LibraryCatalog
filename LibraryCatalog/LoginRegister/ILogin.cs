using LibraryCatalog.Users;

namespace LibraryCatalog.LoginRegister
{
    public interface ILogin
    {
        IBaseUser SignIn(string username, string password);
    }
}