using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Soql.Clauses
{
    public abstract class ImmutableClause : BuildCapable
    {
        protected List<BuildCapable> _expressions = new List<BuildCapable>();

        /**
         * An array with the expressions contained in this class
         */
        public BuildCapable[] Expressions
        {
            get
            {
                return _expressions.ToArray();
            }
        }

        public T Append<T>(BuildCapable[] expressions)
            where T : ImmutableClause, new()
        {
            var appendedList = new List<BuildCapable>(expressions);
            T clause = new T();
            clause._expressions = appendedList;
            return clause;
        }
    }
}
