﻿using System;
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

            DateTime newVersionDate = new DateTime(1900,1,1);

            string lastAuthor = string.Empty;

            // Get lastest version of class (check class and methods max date in attr)
            foreach (var classWithAttr in typesWithAttyList)
            {
                newVersionDate = GetClassMaxVersionDateTime(classWithAttr, lastAuthor);

                try
                {
                    // Compare max date of current class in project with DB max version and create new verion if we have newest (or create if class already not in DB)
                    if (db.ControlledClasses.Any(c => c.name == classWithAttr.Name))
                    {
                        var user = GetUserFromClassType(classWithAttr, lastAuthor);

                        var modifiedClass = db.ControlledClasses.First(c => c.name == classWithAttr.Name);

                        var lastestVersion = modifiedClass.versions.OrderByDescending(v => v.date).First();

                        if (newVersionDate > lastestVersion.date)
                        {
                            AddVersionToClass(user, modifiedClass, classWithAttr, lastestVersion);

                            return true;
                        }
                    }

                    else
                    {
                        CreateNewClassToVersionControl(classWithAttr, lastAuthor);

                        return true;
                    }
                }

                catch
                {
                    return false;
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
        private DateTime GetClassMaxVersionDateTime(Type type, string lastAuthor)
        {
            // Get datetime from class attribute
            var attribute = type.GetCustomAttributes(attributeType, false).First() as VersionControlAttribute;

            DateTime resultDate = attribute.commitDateTime;

            // Search highers datetime in attributes of method in class
            var methodsOfClass = FindMethodsWithAttribute(type);

            foreach (var method in methodsOfClass)
            {
                attribute = method.GetCustomAttributes(attributeType, false).First() as VersionControlAttribute;

                if (attribute.commitDateTime > resultDate)
                {
                    resultDate = attribute.commitDateTime;

                    lastAuthor = attribute.userLogin;
                }
            }

            return resultDate;
        }

        // Creating new class to version controll system
        private void CreateNewClassToVersionControl(Type type, string lastAuthor)
        {
            ControlledClass newClass = new ControlledClass();

            newClass.name = type.Name;

            db.ControlledClasses.Add(newClass);
            db.SaveChanges();

            var user = GetUserFromClassType(type, lastAuthor);

            AddVersionToClass(user, newClass, type, null);
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

        private void AddVersionToClass(User user, ControlledClass controlledClass, Type type, Model.Version previousVersion)
        {
            var attributes = type.GetCustomAttributes(attributeType, true).First() as VersionControlAttribute;

            XTRMTestProject.Model.Version version = new XTRMTestProject.Model.Version()
            {
                classBody = GetClassBody(attributes.csFileRealName),
                comment = attributes.comment,
                date = attributes.commitDateTime,
                user = user,
                controlledClass = controlledClass,
                previousVersion = previousVersion
            };

            db.Versions.Add(version);
            db.SaveChanges();
        }

        private User GetUserFromClassType(Type type, string lastAuthor)
        {
            var attributes = type.GetCustomAttributes(attributeType, true).First() as VersionControlAttribute;

            User user = null;

            if (lastAuthor == string.Empty)
            {
                user = GetUserByNameString(attributes.userLogin);
            }

            else
            {
                user = GetUserByNameString(lastAuthor);
            }

            return user;
        }

        private User GetUserByNameString(string userName)
        {
            User user = null;

            if (!db.Users.Any(u => u.login == userName))
            {
                user = new User();
                user.login = userName;

                db.Users.Add(user);
                db.SaveChanges();
            }

            else
            {
                user = db.Users.First(u => u.login == userName);
            }

            return user;
        }
    }
}
