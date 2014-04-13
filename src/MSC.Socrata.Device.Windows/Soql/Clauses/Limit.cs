using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Soql.Clauses
{
    public class Limit : BuildCapable
    {
        /**
         * The max number of results per page
         */
        public int? Value { get; private set; }

        /**
         * Constructs a limit clause
         *
         * @param limit The max number of results per page
         */
        public Limit(int? limit)
        {
            Value = limit;
        }

        /**
         * @see com.socrata.android.soql.clauses.BuildCapable#build()
         */
        public override string Build()
        {
            return Value == null ? "" : string.Format("limit {0}", Value);
        }
    }
}
