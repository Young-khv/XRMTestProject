using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTRMTestProject
{
    public interface IVersionController
    {
        // Find and add new version of classes and methods if exist
        // Return TRUE if find some changes; FALSE if there is no any changes
        bool AddVersionChanges();


    }
}
