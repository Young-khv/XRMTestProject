using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using XTRMTestProject;
using XTRMTestProject.Version_Control;

namespace XTRMTestConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
           // DO SOME STUFF IN MY APP
            

            VersionController vc = new VersionController(Assembly.GetExecutingAssembly());

            bool appHasChanges = vc.SearchAndAddNewVersions();

            string message = (appHasChanges) ? "We add all changes of your project to our DB" : "404: Changes no found";

            Console.WriteLine(message);

            Console.ReadLine();
        }
    }
}
