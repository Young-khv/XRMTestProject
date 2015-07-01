using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTRMTestProject;

namespace XTRMTestConsoleProject
{
    [VersionControl("Developer_1","30.06.2015","Create simple class")]
    class MyClass
    {
        [VersionControl("Developer_1", "01.07.2015", "Create simple method")]
        public void SomeStuf()
        {
            int a = 2;
            int b = 3;
            int c = a + b;
        }
    }
}
