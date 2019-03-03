
namespace LibraryCatalog.LoginRegister
{
    public interface ILoginRegistrationValidation
    {
        bool CheckIfUsernameExists(string query, string data);
        bool CheckIfUserExists(string query, string username, string password);
    }
}