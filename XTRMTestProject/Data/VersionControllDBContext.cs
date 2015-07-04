using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTRMTestProject.Model;

namespace XTRMTestProject.Data
{
    public class VersionControllContext: DbContext
    {
        public VersionControllContext()
            : base(@"Data Source=PC-HOME\MSSQLSERVER2012;Initial Catalog=VersionControll;Integrated Security=True")
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DbSet<ControlledClass> ControlledClasses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<XTRMTestProject.Model.Version> Versions { get; set; }
    }
}
