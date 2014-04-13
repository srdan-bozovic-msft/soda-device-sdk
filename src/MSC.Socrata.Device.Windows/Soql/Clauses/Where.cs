using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Soql.Clauses
{
    public class Where : ImmutableClause
    {
        public override string Build()
        {
            var expressions = Expressions;
            return expressions.Length == 0 ? "" : string.Format("where {0}", string.Join(" ", Expression.BuildAll(Expression.And(expressions))));
        }
    }
}
