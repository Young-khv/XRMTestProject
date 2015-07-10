using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Data.SqlClient;
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

            Model.Version newVersion = null;

            bool result = false;

           // Get lastest version of class (check class and methods max date in attr)
            foreach (var classWithAttr in typesWithAttyList)
            {
                newVersion = GetLastestVersionByType(classWithAttr);

                try
                {
                    // Compare max date of current class in project with DB max version and create new verion if we have newest (or create if class already not in DB)
                    if (db.ControlledClasses.Any(c => c.name == classWithAttr.Name))
                    {
                        var modifiedClass = db.ControlledClasses.First(c => c.name == classWithAttr.Name);

                        var lastestVersion = modifiedClass.versions.OrderByDescending(v => v.date).First();

                        if (newVersion.date > lastestVersion.date)
                        {
                            AddVersionToClass(newVersion, modifiedClass, lastestVersion);

                            result = true;
                        }
                    }

                    else
                    {
                        CreateNewClassToVersionControl(classWithAttr);
                       
                        result = true;
                    }

                    db.SaveChanges();
                }

                catch
                {
                    result = false;
                }
            }

            return result;
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

        // Getting class lastest version info ----- REFACTOR
        private Model.Version GetLastestVersionByType(Type type)
        {
            var attribute = type.GetCustomAttributes(attributeType, false).First() as VersionControlAttribute;

            Model.Version resultVersion = new Model.Version();

            ConfigStartupVersionState(attribute, resultVersion);

            var methodsOfClass = FindMethodsWithAttribute(type);

            foreach (var method in methodsOfClass)
            {
                attribute = method.GetCustomAttributes(attributeType, false).First() as VersionControlAttribute;

                if (attribute.commitDateTime > resultVersion.date)
                {
                   ConfigStartupVersionState(attribute, resultVersion);
                }
            }

            return resultVersion;
        }

        // Creating new class to version controll system
        private void CreateNewClassToVersionControl(Type type)
        {
            ControlledClass newClass = new ControlledClass();

            newClass.name = type.Name;

            db.ControlledClasses.Add(newClass);
            //db.SaveChanges();

            var version = GetLastestVersionByType(type);

            AddVersionToClass(version, newClass, null);
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

        // Creating new version of controlled class
        private void AddVersionToClass(Model.Version version, ControlledClass controlledClass, Model.Version previousVersion)
        {
            version.controlledClass = controlledClass;
            version.classBody = GetClassBody(version.realCLassFileName);
            version.previousVersion = previousVersion;

            db.Versions.Add(version);
            //db.SaveChanges();
        }

        // Getting user by name or create and return if does not exist
        private User GetUserByNameString(string userName)
        {
            User user = null;

            if (!db.Users.Any(u => u.login == userName))
            {
                user = new User();
                user.login = userName;

                db.Users.Add(user);
                //db.SaveChanges();
            }

            else
            {
                user = db.Users.First(u => u.login == userName);
            }

            return user;
        }

        // Configuring startup state of new version and return it
        private void ConfigStartupVersionState(VersionControlAttribute attribute, Model.Version version)
        {
            version.date = attribute.commitDateTime;

            version.comment = attribute.comment;

            version.realCLassFileName = attribute.csFileRealName;

            version.user = GetUserByNameString(attribute.userLogin);
        }
    }
}
