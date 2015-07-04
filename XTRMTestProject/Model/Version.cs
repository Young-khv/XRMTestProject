using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTRMTestProject.Model
{
    public class Version
    {
        public int id { get; set; }

        public string classBody { get; set; }

        public DateTime date { get; set; }

        public string comment { get; set; }

        public virtual User user { get; set; }

        public virtual Version previousVersion { get; set; }

        public virtual ControlledClass controlledClass { get; set; }
    }
}
