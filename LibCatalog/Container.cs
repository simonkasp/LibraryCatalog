using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryCatalog.Users;
using LibraryCatalog.LoginRegister;
using LibraryCatalog.Database;

namespace ConsoleUI
{
    public static class Container
    {
        public static IContainer Builder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>();
            builder.RegisterType<BaseUser>().As<IBaseUser>();
            builder.RegisterType<RegularUser>().As<IRegularUser>();
            builder.RegisterType<AdminUser>().As<IAdminUser>();
            builder.RegisterType<Login>().As<ILogin>();
            builder.RegisterType<Register>().As<IRegister>();
            builder.RegisterType<MySQL>().As<IMySQL>();

            return builder.Build();
        }
    }
}
