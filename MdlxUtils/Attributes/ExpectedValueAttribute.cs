using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdxLoader
{
    public class ExpectedDWORDValueAttribute : Attribute
    {
        private UInt32 expectedValue;

        public ExpectedDWORDValueAttribute(UInt32 value)
        {
            this.expectedValue = value;
        }

        public virtual UInt32 Value
        {
            get { return expectedValue; }
        }
    }
}
