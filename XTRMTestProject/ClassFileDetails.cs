using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XTRMTestProject
{
    public class ClassFileDetails
    {
        public int OID { get; set; }
        public string className { get; set; }

        [Sqo.Attributes.UniqueConstraint]
        public string path { get; set; }

        public System.DateTime updateTime { get; set; }

        internal ClassFileDetails()
        {
        }

        internal ClassFileDetails(string setClassName, string setPath, System.DateTime setUpdateTime)
        {
            className = setClassName;
            path = setPath;
            updateTime = setUpdateTime;

            DatabaseLink.StoreClassFileDetails(this);
        }
    }
}
