using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using XTRMTestProject;

namespace XTRMTestConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Type t = typeof (MyClass);
            var attrs = t.GetCustomAttributes(false);
            VersionControlAttribute attr = null;
            foreach (var item in attrs)
            {
                attr = item as VersionControlAttribute;
                Console.WriteLine("Login:{0} | date:{1} | comment:{2}", attr.userLogin, attr.commitDateTime.ToString("dd.MM.yyyy"), attr.comment);
            }
            

            Console.ReadLine();
        }

        static IEnumerable<Type> GetTypesWithHelpAttribute(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(VersionControlAttribute), true).Length > 0)
                {
                    yield return type;
                }
            }
        }

        static IEnumerable<MethodInfo> FindMethodsWithAttribute(Type type)
        {
            var methods = type.GetMethods(BindingFlags.Public);

            foreach(var method in methods)
            {
                var attributes = method.GetCustomAttributes( typeof(VersionControlAttribute), true );
                if (attributes != null && attributes.Length > 0)
                    return (IEnumerable<MethodInfo>) attributes;

            }

            return null;
        }
    }
}
