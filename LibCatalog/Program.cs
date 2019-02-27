﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Container.Builder();

            using(var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<Application>();
                app.Run();
            }
        }
    }
}
