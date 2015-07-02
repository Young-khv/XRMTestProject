using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XTRMTestProject.Data;

namespace XTRMTestProject.Version_Control
{
    public class VersionController
    {
        private Assembly assembly;
        private VersionControllContext db = new VersionControllContext(); 

        public VersionController()
        {
            this.assembly = Assembly.GetExecutingAssembly();
        }

        public VersionController(Assembly assembly)
        {
            this.assembly = assembly;
        }

        // Look at DB, compare state of classes with custom attribute and add newest versions to DB
        // Return TRUE when some changes are exist and FALSE when they does not
        public bool SearchAndAddNewVersions()
        {
            return false;
        }

        // Finde all classes with attribute type of attrType
        private List<Type> GetTypesWithAttribute(Type attrType)
        {
            List<Type> classesList = new List<Type>();

            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(attrType, true).Length > 0)
                {
                    classesList.Add(type);
                }
            }
            return classesList;
        }

        // Finde all methods of class with type classType and attribute attrType
        private List<MethodInfo> FindMethodsWithAttribute(Type classType, Type attrType)
        {
            List<MethodInfo> methodList = new List<MethodInfo>();

            var methods = classType.GetMethods();

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(attrType, true);

                if (attributes != null && attributes.Length > 0)
                    methodList.Add(method);
            }

            return methodList;
        }
    }
}
