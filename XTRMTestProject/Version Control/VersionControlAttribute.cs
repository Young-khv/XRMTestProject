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

        public VersionControlAttribute(string userLogin, string dateTime, string comment)
        {
            this.userLogin = userLogin;
            this.comment = comment;
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
