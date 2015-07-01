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
            
            Assembly assembley = Assembly.GetExecutingAssembly();
            List<Type> typeList = GetTypesWithHelpAttribute(assembley, typeof(VersionControlAttribute));
            foreach(var classType in typeList)
            {
               var methodList = FindMethodsWithAttribute(classType,typeof(VersionControlAttribute));             
            }

            Console.ReadLine();
        }

        static List<Type> GetTypesWithHelpAttribute(Assembly assembly, Type attrType)
        {
            List<Type> result = new List<Type>();
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(attrType, true).Length > 0)
                {
                    result.Add(type);                   
                }
            }
            return result;
        }

        // Finde all methods of class with type classType and attribute attrType
        static List<MethodInfo> FindMethodsWithAttribute(Type classType, Type attrType)
        {
            var methods = classType.GetMethods();
            List<MethodInfo> result = new List<MethodInfo>();
            foreach(var method in methods)
            {
                var attributes = method.GetCustomAttributes( attrType, true );
                if (attributes != null && attributes.Length > 0)
                    result.Add(method);
            }

            return result;
        }
    }
}
