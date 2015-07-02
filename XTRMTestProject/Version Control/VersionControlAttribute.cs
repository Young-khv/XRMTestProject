using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTRMTestProject
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class VersionControlAttribute: Attribute
    {
        public string userLogin;
        public DateTime commitDateTime;
        public string comment;
        public string csFileRealName;

        public VersionControlAttribute(string userLogin, string dateTime, string comment, string csFileRealName)
        {
            this.userLogin = userLogin;
            this.comment = comment;
            this.csFileRealName = csFileRealName;
            try
            {
                this.commitDateTime = Convert.ToDateTime(dateTime);
            }
            catch (Exception)
            {
                throw new ArgumentException("Date string has invalid format");
            }
            
        }
    }
}
