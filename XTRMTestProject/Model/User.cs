using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTRMTestProject.Model
{
    public class User
    {
        public int id { get; set; }

        public string login { get; set; }

        public virtual ICollection<Version> versions { get; set; }

        public User()
        {
            versions = new List<Version>();
        }
    }
}
