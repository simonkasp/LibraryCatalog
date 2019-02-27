using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCatalog.LoginRegister
{
    public interface ILoginRegistrationValidation
    {
        bool CheckIfDataExists(string query, string data);

    }
}
