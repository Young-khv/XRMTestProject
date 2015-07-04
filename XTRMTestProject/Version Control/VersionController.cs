using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;
using XTRMTestProject.Data;
using XTRMTestProject.Model;

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
            foreach (var classWithAttr in typesWithAttyList)
            {
                versionDate = GetClassMaxVersionDateTime(classWithAttr);

                 // Compare max date of current class in project with DB max version and create new verion if we have newest (or create if class already not in DB)
                if (db.ControlledClasses.Any(c => c.name == classWithAttr.Name))
                {
                    // TODO
                }

                else
                {
                    CreateNewClassToVersionControl(classWithAttr);
                }
            }

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
            var attrebute = type.GetCustomAttributes(attributeType, false).First() as VersionControlAttribute;

            DateTime resultDate = attrebute.commitDateTime;

            // Search highers datetime in attributes of method in class
            var methodsOfClass = FindMethodsWithAttribute(type);

            foreach (var method in methodsOfClass)
            {
                attrebute = method.GetCustomAttributes(attributeType, false).First() as VersionControlAttribute;

                resultDate = (attrebute.commitDateTime > resultDate) ? attrebute.commitDateTime : resultDate;
            }

            return resultDate;
        }

        private void CreateNewClassToVersionControl(Type type)
        {
            var attributes = type.GetCustomAttributes(attributeType, true).First() as VersionControlAttribute;

            User user = db.Users.First(u => u.login == attributes.userLogin);

            if (user == null)
            {
                user = new User();
                user.login = attributes.userLogin;

                db.Users.Add(user);
                db.SaveChanges();
            }

            ControlledClass newClass = new ControlledClass();

            newClass.name = type.Name;

            db.ControlledClasses.Add(newClass);
            db.SaveChanges();

            AddVersionToClass(user, newClass, type);
        }

        // Getting class source code by class real file name (.cs file name required)
        private string GetClassBody(string classFileName)
        {
            string project_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            project_path = Directory.GetParent(project_path).ToString();

            string[] oDirectories = Directory.GetFiles(project_path, classFileName, SearchOption.AllDirectories);

            var syntaxTree = SyntaxTree.ParseFile(oDirectories[0]);

            var root = syntaxTree.GetRoot();

            var method = root.GetText();

            string methodLines = string.Empty;

            foreach (var line in method.Lines)
            {
                methodLines += string.Format("{0}[~]", line.ToString());
            }

            methodLines.Remove(methodLines.Length - 4, 3);

            return methodLines;
        }

        private void AddVersionToClass(User user, ControlledClass controlledClass, Type type)
        {
            var attributes = type.GetCustomAttributes(attributeType, true).First() as VersionControlAttribute;

            XTRMTestProject.Model.Version version = new XTRMTestProject.Model.Version()
            {
                classBody = GetClassBody(attributes.csFileRealName),
                comment = attributes.comment,
                date = attributes.commitDateTime,
                user = user,
                controlledClass = controlledClass
            };

            db.Versions.Add(version);
            db.SaveChanges();
        }
    }
}
