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
                List<string> result = new List<string>();

                var oldVersionCode = version.previousVersion.classBody.Split(new string[] { "[~]" }, StringSplitOptions.None).ToList();

                var LCSMatrix = GetLongestCommonSubsequenceMatrix(oldVersionCode, newVersionCode);

                GetDiff(LCSMatrix,result,oldVersionCode,newVersionCode, oldVersionCode.Count, newVersionCode.Count);

                return result;
            }
        }

        private static void GetDiff(int[,] lcsMatrix, List<string> result, List<string> s1, List<string> s2, int i, int j)
        {
            if (i > 0 && j > 0 && s1[i - 1] == s2[j - 1])
            {
                GetDiff(lcsMatrix, result, s1, s2, i - 1, j - 1);
                result.Add("  " + s1[i - 1]);
            }
            else
            {
                if (j > 0 && (i == 0 || lcsMatrix[i, j - 1] >= lcsMatrix[i - 1, j]))
                {
                    GetDiff(lcsMatrix, result, s1, s2, i, j - 1);
                    result.Add("+ " + s2[j - 1]);
                }
                else if (i > 0 && (j == 0 || lcsMatrix[i, j - 1] < lcsMatrix[i - 1, j]))
                {
                    GetDiff(lcsMatrix, result, s1, s2, i - 1, j);
                    result.Add("- " + s1[i - 1]);
                }
            }
        }

        private static int[,] GetLongestCommonSubsequenceMatrix(List<string> s1, List<string> s2)
        {
            int[,] lcsMatrix = new int[s1.Count + 1, s2.Count + 1];

            for (int i = 0; i < lcsMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < lcsMatrix.GetLength(1); j++)
                {
                    lcsMatrix[i, j] = 0;
                }
            }

            for (int i = 1; i < lcsMatrix.GetLength(0); i++)
            {
                for (int j = 1; j < lcsMatrix.GetLength(1); j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                        lcsMatrix[i, j] = lcsMatrix[i - 1, j - 1] + 1;
                    else
                        lcsMatrix[i, j] = Math.Max(lcsMatrix[i - 1, j], lcsMatrix[i, j - 1]);
                }
            }

            return lcsMatrix;
        }
    }
}
