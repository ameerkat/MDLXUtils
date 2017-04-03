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
        private string expression;

        public ArraySizeAttribute(uint size)
        {
            this.size = size;
        }

        public ArraySizeAttribute(string expression)
        {
            this.expression = expression;
        }

        public virtual uint Size
        {
            get { return size; }
        }

        public virtual string Expression
        {
            get { return expression; }
        }
    }
}
