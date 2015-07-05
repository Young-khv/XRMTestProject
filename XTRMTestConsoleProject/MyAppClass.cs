using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTRMTestProject;

namespace XTRMTestConsoleProject
{
    [VersionControl("Developer_1", "07.07.2015", "Change MyClass", "MyAppClass.cs")]
    class MyClass
    {
        [VersionControl("Developer_2", "08.07.2015", "Create method to sum 2 strings", "MyAppClass.cs")]
        public string StringSum(string a, string b)
        {
            return String.Format("{0}+{1} = {2}", a, b, a + b);
        }
    }
}
