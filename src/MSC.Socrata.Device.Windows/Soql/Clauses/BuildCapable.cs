using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Soql.Clauses
{
    public abstract class BuildCapable
    {
        public abstract string Build();

        public static implicit operator BuildCapable(string s)
        {
            return AsExpression(s);
        }

        public static BuildCapable AsExpression(string expression)
        {
            return Expression.SimpleExpression(expression);
        }

        public static BuildCapable[] AsExpressions(params string[] expressions)
        {
            var simpleExpressions = new BuildCapable[expressions.Length];
            for (int i = 0; i < expressions.Length; i++)
            {
                var expression = expressions[i];
                simpleExpressions[i] = AsExpression(expression);
            }
            return simpleExpressions;
        }

        public static string[] BuildAll(params BuildCapable[] buildCapable)
        {
            var built = new string[buildCapable.Length];
            for (int i = 0; i < buildCapable.Length; i++)
            {
                var capable = buildCapable[i];
                built[i] = capable.Build();
            }
            return built;
        }

    }
}
