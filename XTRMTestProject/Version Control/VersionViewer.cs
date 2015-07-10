using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers;
using XTRMTestProject.Data;
using XTRMTestProject.Model;

namespace XTRMTestProject.Version_Control
{
    public class VersionViewer
    {
        private VersionControllContext db;

        public VersionViewer()
        {
            this.db = new VersionControllContext();
        }

        public List<ControlledClass> GetAllControlledClasses()
        {
            return db.ControlledClasses.ToList();
        }

        public List<User> GetAllUsers()
        {
            return db.Users.ToList();
        }

        public List<Model.Version> GetVersionsForClass(int classID)
        {
            return db.Versions.Where(v=>v.controlledClass.id == classID).ToList();
        }

        public List<Model.Version> GetVersionsByUser(int userID)
        {
            return db.Versions.Where(v => v.user.id == userID).ToList();
        }

        public List<string> GetVersionSourceCode(Model.Version version)
        {
            var newVersionCode = version.classBody.Split(new string[] { "[~]" }, StringSplitOptions.None).ToList();

            if (version.previousVersion == null)
            {
                return newVersionCode;
            }
            else
            {
                var oldVersionCode = version.previousVersion.classBody.Split(new string[] { "[~]" }, StringSplitOptions.None).ToList();
                return GetDifferenceBetweenTwoLists(newVersionCode,oldVersionCode);
            }
        }

        // TODO: reorganize method to more correct working (look at diff algorythms)
        public  List<string> GetDifferenceBetweenTwoLists(List<string> newVersion, List<string> oldVersion)
        {
            List<string> result = new List<string>();

            var deletedItems = oldVersion.Except(newVersion).ToList();

            var addedItems = newVersion.Except(oldVersion).ToList();

            // Use j - index to work with old list, i - index - with new list
            int i = 0;
            int j = 0;
            for (int step = 0; step < oldVersion.Count + deletedItems.Count + addedItems.Count ; step++)
            {
                if (i < newVersion.Count && j < oldVersion.Count && newVersion[i] == oldVersion[j])
                {
                    result.Add(newVersion[i]);
                    if (i < newVersion.Count){i++;}
                    if (j < oldVersion.Count){j++;}
                }

                else
                {
                    if (j < oldVersion.Count)
                    {
                        if (deletedItems.FindIndex(f => f == oldVersion[j]) >= 0)
                        {
                            result.Add(string.Format("-{0}", oldVersion[j]));
                           j++;
                        }
                    }

                    if (i < newVersion.Count)
                    {
                        if (addedItems.FindIndex(f => f == newVersion[i]) >= 0)
                        {
                            result.Add(string.Format("+{0}", newVersion[i]));
                            i++;
                        }
                    }
                }
            }

            return result;
        }
    }
}
