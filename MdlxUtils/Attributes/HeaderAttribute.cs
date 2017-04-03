using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdxLoader
{
    public class HeaderAttribute : Attribute
    {
        private string name;

        public HeaderAttribute(string name)
        {
            this.name = name;
        }

        public virtual string Name
        {
            get { return name; }
        }
    }
}
