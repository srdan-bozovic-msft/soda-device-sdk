using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Soql.Clauses
{
    public class Offset : BuildCapable
    {
        /**
         * The record offset
         */
        public int? Value { get; private set; }

        /**
         * @param offset
         */
        public Offset(int? offset)
        {
            Value = offset;
        }

        /**
         * @see com.socrata.android.soql.clauses.BuildCapable#build()
         */
        public override string Build()
        {
            return Value == null ? "" : string.Format("offset {0}", Value);
        }
    }
}
