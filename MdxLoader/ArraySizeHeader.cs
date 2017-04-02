using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdxLoader
{
    public class ArraySizeAttribute : Attribute
    {
        private uint size;

        public ArraySizeAttribute(uint size)
        {
            this.size = size;
        }

        public virtual uint Size
        {
            get { return size; }
        }
    }
}
