using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTRMTestProject.Model
{
    public class ControlledClass
    {
        public int id { get; set; }

        public string name { get; set; }
        
        public  virtual ICollection<Version> versions { get; set; }

        public ControlledClass()
        {
            versions = new List<Version>();
        }
    }
}
