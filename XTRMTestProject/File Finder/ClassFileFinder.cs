using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace XTRMTestProject
{
    public static class ClassFileFinder
    {
        static List<string> classFiles;

        public static ClassFileDetails FindClassFile(System.Type t)
        {
            return FindClassFile(t.Name);
        }

        public static ClassFileDetails FindClassFile(string className)
        {
            ClassFileDetails details = new ClassFileDetails();//DatabaseLink.GetClassFileDetails(className);
            if (true)
            {
                //Lookup class name in file names
                classFiles = new List<string>();
                FindAllScriptFiles(Environment.CurrentDirectory);
                for (int i = 0; i < classFiles.Count; i++)
                {
                    if (classFiles[i].Contains(className))
                    {
                        details = new ClassFileDetails(className, classFiles[i], File.GetLastAccessTimeUtc(classFiles[i]));
                    }
                }
                //Lookup class name in the class file text 
                //if (details == null)
                //{
                //    for (int i = 0; i < classFiles.Count; i++)
                //    {
                //        string codeFile = File.ReadAllText(classFiles[i]);
                //        if (codeFile.Contains("class " + className))
                //        {
                //            details = new ClassFileDetails(className, classFiles[i], File.GetLastAccessTimeUtc(classFiles[i]));
                //        }
                //    }
                //}
                return details;
            }
            //else
            //{
            //    return details;
            //}
        }

        static void FindAllScriptFiles(string startDir)
        {
            try
            {
                foreach (string file in Directory.GetFiles(startDir))
                {
                    if (file.Contains(".cs"))
                        classFiles.Add(file);
                }
                foreach (string dir in Directory.GetDirectories(startDir))
                {
                    FindAllScriptFiles(dir);
                }
            }
            catch (System.Exception ex)
            {}
        }
    }
}
