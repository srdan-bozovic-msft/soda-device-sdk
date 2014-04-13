using MSC.Socrata.Device.Soql.Datatypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Soql.Clauses
{
    public class Expression : BuildCapable
    {
        public string Value { get; private set; }

        public bool RequiresWrap { get; private set; }

        public Expression(string value)
        {
            Value = value;
        }

        public Expression(string value, bool requiresWrap)
        {
            Value = value;
            RequiresWrap = requiresWrap;
        }

        public static Expression SimpleExpression(string value)
        {
            return new Expression(value);
        }

        public static Expression InfixedFunction(string function, BuildCapable arg)
        {
            return new Expression(string.Format("{0} {1}", function, arg.Build()));
        }

        public static Expression SuffixedFunction(string function, BuildCapable arg)
        {
            return new Expression(string.Format("{0} {1}", arg.Build(), function));
        }

        public static Expression ApplyOperator(string op, params BuildCapable[] expressions)
        {
            return new Expression(string.Join(string.Format(" {0} ", op), BuildAll(expressions)));
        }

        public static Expression Function(string function, params BuildCapable[] args)
        {
            return new Expression(string.Format("{0}({1})", function, string.Join(", ", BuildAll(args))));
        }

        public static Expression IsNotNull(BuildCapable expression)
        {
            return SuffixedFunction("is not null", expression);
        }

        public static Expression IsNull(BuildCapable expression)
        {
            return SuffixedFunction("is null", expression);
        }

        public static Expression Not(BuildCapable expression)
        {
            return InfixedFunction("not", expression);
        }

        public static Expression And(params BuildCapable[] expressions)
        {
            return ApplyOperator("and", expressions);
        }

        public static Expression And(params string[] expressions)
        {
            return And(AsExpressions(expressions));
        }

        public static Expression Or(params BuildCapable[] expressions)
        {
            return ApplyOperator("or", expressions);
        }

        public static Expression Or(params string[] expressions)
        {
            return Or(AsExpressions(expressions));
        }

        public static Expression Lt(BuildCapable left, BuildCapable right)
        {
            return ApplyOperator("<", left, right);
        }

        public static Expression Lte(BuildCapable left, BuildCapable right)
        {
            return ApplyOperator("<=", left, right);
        }

        public static Expression Eq(BuildCapable left, BuildCapable right)
        {
            return ApplyOperator("=", left, right);
        }

        public static Expression Neq(BuildCapable left, BuildCapable right)
        {
            return ApplyOperator("!=", left, right);
        }

        public static Expression Gt(BuildCapable left, BuildCapable right)
        {
            return ApplyOperator(">", left, right);
        }

        public static Expression Gte(BuildCapable left, BuildCapable right)
        {
            return ApplyOperator(">=", left, right);
        }

        /**
         * Wraps an expression with an upper function that would evaluate as uppercase in the server e.g. 'upper(a)'
         */
        public static Expression Upper(BuildCapable expression)
        {
            return Function("upper", expression);
        }

        /**
         * Wraps an expression with a lower function that would evaluate as lowercase in the server e.g. 'lower(a)'
         */
        public static Expression Lower(BuildCapable expression)
        {
            return Function("lower", expression);
        }

        /**
         * Wraps a left and right expression with a startsWith function e.g. 'starts_with(a, b)'
         */
        public static Expression StartsWith(BuildCapable left, BuildCapable right)
        {
            return Function("starts_with", left, right);
        }

        /**
         * Wraps a left and right expression with a contains function e.g. 'contains(a, b)'
         */
        public static Expression Contains(BuildCapable left, BuildCapable right)
        {
            return Function("contains", left, right);
        }

        /**
         * Joins a left and right expression with a multiply operator e.g. 'a * b'
         */
        public static Expression MultipliedBy(BuildCapable left, BuildCapable right)
        {
            return ApplyOperator("*", left, right);
        }

        /**
         * Joins a left and right expression with a divide operator e.g. 'a / b'
         */
        public static Expression DividedBy(BuildCapable left, BuildCapable right)
        {
            return ApplyOperator("/", left, right);
        }

        /**
         * Joins a left and right expression with an add operator e.g. 'a + b'
         */
        public static Expression Add(BuildCapable left, BuildCapable right)
        {
            return ApplyOperator("+", left, right);
        }

        /**
         * Joins a left and right expression with an subtract operator e.g. 'a - b'
         */
        public static Expression Subtract(BuildCapable left, BuildCapable right)
        {
            return ApplyOperator("-", left, right);
        }

        /**
         * Wraps an expression with an to_string function that would evaluate as a string cast in the server e.g. 'to_string(a)'
         */
        public static Expression CastToString(BuildCapable expression)
        {
            return Function("to_string", expression);
        }

        /**
         * Wraps an expression with an to_number function that would evaluate as a number cast in the server e.g. 'to_number(a)'
         */
        public static Expression ToNumber(BuildCapable expression)
        {
            return Function("to_number", expression);
        }

        /**
         * Wraps a left and right expression with a to_location function e.g. 'to_location(lat, lng)'
         */
        public static Expression ToLocation(BuildCapable lat, BuildCapable lng)
        {
            return Function("to_location", lat, lng);
        }

        /**
         * Wraps an expression with an to_boolean function that would evaluate as a number cast in the server e.g. 'to_boolean(a)'
         */
        public static Expression ToBoolean(BuildCapable expression)
        {
            return Function("to_boolean", expression);
        }

        /**
         * Wraps an expression with an to_fixed_timestamp function that would evaluate as a to fixed timestamp conversion in the server e.g. 'to_fixed_timestamp(a)'
         */
        public static Expression ToFixedTimestamp(BuildCapable expression)
        {
            return Function("to_fixed_timestamp", expression);
        }

        /**
         * Wraps an expression with an to_floating_timestamp function that would evaluate as a to floating timestamp conversion in the server e.g. 'to_floating_timestamp(a)'
         */
        public static Expression ToFloatingTimestamp(BuildCapable expression)
        {
            return Function("to_floating_timestamp", expression);
        }

        /**
         * Wraps an expression with a sum function that would evaluate as and aggregated sum in the server e.g. 'sum(a)'
         */
        public static Expression Sum(BuildCapable expression)
        {
            return Function("sum", expression);
        }

        /**
         * Wraps an expression with a count function that would evaluate as and aggregated count in the server e.g. 'count(a)'
         */
        public static Expression Count(BuildCapable expression)
        {
            return Function("count", expression);
        }

        /**
         * Wraps an expression with a avg function that would evaluate as and aggregated avg in the server e.g. 'avg(a)'
         */
        public static Expression Avg(BuildCapable expression)
        {
            return Function("avg", expression);
        }

        /**
         * Wraps an expression with a min function that would evaluate as a min value for an expression in the server e.g. 'min(a)'
         */
        public static Expression Min(BuildCapable expression)
        {
            return Function("min", expression);
        }

        /**
         * Wraps an expression with a max function that would evaluate as a max value for an expression in the server e.g. 'max(a)'
         */
        public static Expression Max(BuildCapable expression)
        {
            return Function("max", expression);
        }

        /**
         * Wraps an expression as an alias that can be further used in the query for other things such as aggregated calculation e.g. 'a as aliasOfA'
         */
        public static Expression As(BuildCapable expression, string alias)
        {
            return ApplyOperator("as", expression, SimpleExpression(alias));
        }

        /**
         * Represents a column in a clause e.g. 'a'
         */
        public static Expression Column(string name)
        {
            return SimpleExpression(name);
        }

        /**
         * Single quotes an expression for literal comparison eg. " a = 'something'  "
         */
        public static Expression Quoted(BuildCapable expression)
        {
            return SimpleExpression(string.Format("'{0}'", expression.Build()));
        }

        /**
         * Adds order direction to an aexpression e.g. 'a desc'
         */
        public static Expression Order(BuildCapable expression, OrderDirection direction)
        {
            return SimpleExpression(string.Format("{0} {0}", expression.Build(), Enum.GetName(typeof(OrderDirection), direction).ToLower()));
        }

        /**
         * Wraps an array of expressions in parentheses e.g. '(a)'
         */
        public static Expression Parentheses(params BuildCapable[] expressions)
        {
            return SimpleExpression(string.Format("({0})", BuildAll(expressions)));
        }

        /**
         * Wraps an array of expressions in parentheses e.g. '(a)'
         */
        public static Expression Parentheses(string[] expressions)
        {
            return Parentheses(AsExpressions(expressions));
        }

        /**
         * Wraps an expression with a within_box function to look up withinBox based properties in bounding box represented by ne and sw coordinates
         */
        public static Expression WithinBox(BuildCapable location, GeoBox geoBox)
        {
            return Function("within_box", location, SimpleExpression(geoBox.Build()));
        }

        public override string Build()
        {
            return RequiresWrap && Value != null ? Parentheses(SimpleExpression(Value)).Build() : Value;
        }

        public override string ToString()
        {
            return string.Format("Expression: {0}", Build());
        }
    }
}
