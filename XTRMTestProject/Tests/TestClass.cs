using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Roslyn.Compilers.CSharp;
using XTRMTestProject.Model;
using XTRMTestProject.Version_Control;

namespace XTRMTestProject.Tests
{
    [TestFixture]
    [VersionControl("Developer_1", "02.07.2015", "Create simple method", "TestClass.cs")]
    public class TestClass
    {
        [Test]
        public static void MONOTest()
        {
            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            wanted_path = Directory.GetParent(wanted_path).ToString();

            string[] oDirectories = Directory.GetFiles(wanted_path, "TestClass.cs", SearchOption.AllDirectories);

            var syntaxTree = SyntaxTree.ParseFile(oDirectories[0]);

            var root = syntaxTree.GetRoot();

            var method = root.GetText();

            string methodLines = string.Empty;

            foreach (var line in method.Lines)
            {
                methodLines += string.Format("{0}[~]",line.ToString());
            }

            methodLines.Remove(methodLines.Length - 4, 3);
            
        }

        [Test]
        [VersionControl("Developer_1", "03.07.2015", "Create simple method", "TestClass.cs")]
        public static void VCTests()
        {
            VersionController vc = new VersionController(Assembly.GetExecutingAssembly());

           vc.SearchAndAddNewVersions();
        }
    }
}
