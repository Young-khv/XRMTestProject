using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTRMTestProject;

namespace XTRMTestConsoleProject
{
    [VersionControl("Developer_1", "07.07.2015", "Create MyClass", "MyAppClass.cs")]
    class MyClass
    {
        [VersionControl("Developer_2", "06.07.2015", "Rename Sum to Sum1", "MyAppClass.cs")]
        public int Sum1(int a, int b)
        {
            return a + b;
        }
    }
}
