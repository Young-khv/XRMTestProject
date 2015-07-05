using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using XTRMTestProject.Version_Control;

namespace XTRMTestProject.Test
{

    [TestFixture]
    class Test
    {
        [Test]
        public static void TestDiffMethod()
        {
            VersionViewer vv = new VersionViewer();

            List<string> Old = new List<string>(){"1","2","8","3","6", "7"};

            List<string> New = new List<string>() { "1", "2", "3", "4", "5", "6" };

            // To test this change method GetDifferenceBetweenTwoLists to public and uncomment next 2 code lines
            //var result = vv.GetDifferenceBetweenTwoLists(New, Old);

            //Assert.AreEqual(result, new List<String>() {"1", "2", "-8", "3", "+4", "+5", "6", "-7"});
        }
    }
}
