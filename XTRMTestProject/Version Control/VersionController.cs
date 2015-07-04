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
        private Type attributeType;
        
        public VersionController(Assembly assembly)
        {
            this.assembly = assembly;
            this.attributeType = typeof(VersionControlAttribute);
        }

        // Look at DB, compare state of classes with custom attribute and add newest versions to DB
        // Return TRUE when some changes are exist and FALSE when they does not
        public bool SearchAndAddNewVersions()
        {
            // Get all class types with custom attribute
            List<Type> typesWithAttyList = GetTypesWithAttribute();

            DateTime versionDate = new DateTime(1900,1,1);

            // Get lastest version of class (check class and methods max date in attr)
            foreach (var classWithAtty in typesWithAttyList)
            {
                versionDate = GetClassMaxVersionDateTime(classWithAtty);
            }

            // Compare max date of current class in project with DB max version and create new verion if we have newest (or create if class already not in DB)

            return false;
        }

        // Finde all classes with attribute type of attrType
        private List<Type> GetTypesWithAttribute()
        {
            List<Type> classesList = new List<Type>();

            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(attributeType, true).Length > 0)
                {
                    classesList.Add(type);
                }
            }
            return classesList;
        }

        // Finde all methods of class with type classType and attribute attrType
        private List<MethodInfo> FindMethodsWithAttribute(Type classType)
        {
            List<MethodInfo> methodList = new List<MethodInfo>();

            var methods = classType.GetMethods();

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(attributeType, true);

                if (attributes != null && attributes.Length > 0)
                    methodList.Add(method);
            }

            return methodList;
        }

        // Find higher version date of class by type
        private DateTime GetClassMaxVersionDateTime(Type type)
        {
            // Get datetime from class attribute
            var attrebutes = type.GetCustomAttributes(attributeType,false);

            VersionControlAttribute attr = attrebutes[0] as VersionControlAttribute;

            DateTime resultDate = attr.commitDateTime;

            // Search highers datetime in attributes of method in class
            var methodsOfClass = FindMethodsWithAttribute(type);

            foreach (var method in methodsOfClass)
            {
                attr = method.GetCustomAttributes(attributeType, false)[0] as VersionControlAttribute;

                resultDate = (attr.commitDateTime > resultDate) ? attr.commitDateTime : resultDate;
            }

            return resultDate;
        }
    }
}
