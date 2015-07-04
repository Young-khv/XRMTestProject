using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTRMTestProject;

namespace XTRMTestConsoleProject
{
    [VersionControl("Developer_1", "30.06.2015", "Create simple class", "MyAppClass.cs")]
    class MyClass
    {
        [VersionControl("Developer_2", "02.07.2015", "Modifying method", "MyAppClass.cs")]
        public void SomeStuf()
        {
            int a = 2;
            int b = 3;
            int c = a + b;
        }
    }
}
