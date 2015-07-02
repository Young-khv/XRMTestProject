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

namespace XTRMTestProject.Tests
{
   [TestFixture]
   public class TestClass
   {
       [Test]
       public static void MONOTest()
       {
           // Assembly assembly = Assembly.GetExecutingAssembly();
           
            

           string startPath = @"C:\Users\Arkadiy\Documents\Visual Studio 2013\Projects\XTRMTestProject\";

           string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

           wanted_path = Directory.GetParent(wanted_path).ToString();

           string[] oDirectories = Directory.GetFiles(wanted_path, "MyAppClass.cs", SearchOption.AllDirectories);

           var syntaxTree = SyntaxTree.ParseFile(oDirectories[0]);

           var root = syntaxTree.GetRoot();

           var method = root.GetText();
       }
   }
}
