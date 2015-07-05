using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTRMTestProject;

namespace XTRMTestConsoleProject.FolderWithFolder.FolderWithWorkingClass
{
    [VersionControl("Developer_1", "03.07.2015", "Add new class to project", "ClassInFolder.cs")]
    class ClassInFolder
    {
        [VersionControl("Developer_1", "05.07.2015", "Add new Method", "ClassInFolder.cs")]
        public void Some()
        {
            int a = 6;
        }
    }
}
