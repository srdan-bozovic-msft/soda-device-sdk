using MSC.Socrata.Device.Soql.Clauses;
using MSC.Socrata.Device.Soql.Datatypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Soql
{
    public class Query<T> : BuildCapable
    {
        public Select SelectClause { get; private set; }

        /**
         * The where clause
         *
         * @see Where
         */
        public Where WhereClause { get; private set; }

        /**
         * The group by clause
         *
         * @see GroupBy
         */
        public GroupBy GroupByClause { get; private set; }

        /**
         * The order by clause
         *
         * @see OrderBy
         */
        public OrderBy OrderByClause { get; private set; }

        /**
         * The offset clause
         *
         * @see Offset
         */
        public int OffsetClause { get; private set; }

        /**
         * The limit clause
         *
         * @see Limit
         */
        public int LimitClause { get; private set; }

        /**
         * The dataset this query is referring to
         */
        public string Dataset { get; private set; }

        /**
         * The object type resulting objects from this query will be mapped to.
         */
        public Type Mapping { get; private set; }

        /**
         * Constructs a SODA query
         *
         * @param dataset Dataset this query is referring to
         * @param mapping The object type resulting objects from this query will be mapped to.
         */
        public Query(string dataset)
        {
            Dataset = dataset;
            Mapping = typeof(T);

            SelectClause = new Select();
            WhereClause = new Where();
            GroupByClause = new GroupBy();
            OrderByClause = new OrderBy();
            OffsetClause = 0;
            LimitClause = 25;
        }


        /**
         * Adds expressions to the clause replacing the clause with a new clause including the
         * appended expressions
         *
         * @param expressions the expressions to be appended
         */
        public void AddSelect(params BuildCapable[] expressions)
        {
            SelectClause = SelectClause.Append<Select>(expressions);
        }

        /**
         * Adds expressions to the clause replacing the clause with a new clause including the
         * appended expressions
         *
         * @param expressions the expressions to be appended
         */
        public void AddWhere(params BuildCapable[] expressions)
        {
            WhereClause = WhereClause.Append<Where>(expressions);
        }

        /**
         * Syntactic sugar to add a where expression of type Expression#isNotNull
         */
        public void WhereIsNotNull(BuildCapable expression)
        {
            AddWhere(Expression.IsNotNull(expression));
        }

        /**
         * Syntactic sugar to add a where expression of type Expression#isNull
         */
        public void WhereIsNull(BuildCapable expression)
        {
            AddWhere(Expression.IsNull(expression));
        }

        /**
         * Syntactic sugar to add a where expression of type Expression#gt
         */
        public void WhereGreaterThan(BuildCapable left, BuildCapable right)
        {
            AddWhere(Expression.Gt(left, right));
        }

        /**
         * Syntactic sugar to add a where expression of type Expression#get
         */
        public void WhereGreaterThanOrEquals(BuildCapable left, BuildCapable right)
        {
            AddWhere(Expression.Gte(left, right));
        }

        /**
         * Syntactic sugar to add a where expression of type Expression#lt
         */
        public void WhereLessThan(BuildCapable left, BuildCapable right)
        {
            AddWhere(Expression.Lt(left, right));
        }

        /**
         * Syntactic sugar to add a where expression of type Expression#lte
         */
        public void WhereLessThanOrEquals(BuildCapable left, BuildCapable right)
        {
            AddWhere(Expression.Lte(left, right));
        }

        /**
         * Syntactic sugar to add a where expression of type Expression#eq
         */
        public void WhereEquals(BuildCapable left, BuildCapable right)
        {
            AddWhere(Expression.Eq(left, right));
        }

        /**
         * Syntactic sugar to add a where expression of type Expression#neq
         */
        public void WhereNotEquals(BuildCapable left, BuildCapable right)
        {
            AddWhere(Expression.Neq(left, right));
        }

        /**
         * Syntactic sugar to add a where expression of type Expression#startsWith
         */
        public void WhereStartsWith(BuildCapable left, BuildCapable right)
        {
            AddWhere(Expression.StartsWith(left, right));
        }

        /**
         * Syntactic sugar to add a where expression of type Expression#contains
         */
        public void WhereContains(BuildCapable left, BuildCapable right)
        {
            AddWhere(Expression.Contains(left, right));
        }

        /**
         * Syntactic sugar to add a where expression of type Expression#withinBox
         */
        public void WhereWithinBox(BuildCapable location, GeoBox geoBox)
        {
            AddWhere(Expression.WithinBox(location, geoBox));
        }

        /**
         * Adds expressions to the clause replacing the clause with a new clause including the
         * appended expressions
         *
         * @param expressions the expressions to be appended
         */
        public void AddGroup(params BuildCapable[] expressions)
        {
            GroupByClause = GroupByClause.Append<GroupBy>(expressions);
        }

        /**
         * Adds expressions to the clause replacing the clause with a new clause including the
         * appended expressions
         *
         * @param expressions the expressions to be appended
         */
        public void AddOrder(params BuildCapable[] expressions)
        {
            OrderByClause = OrderByClause.Append<OrderBy>(expressions);
        }

        /**
         * Sets the query offset
         *
         * @param offset the query offset
         */
        public void SetOffset(int offset)
        {
            OffsetClause = offset;
        }

        /**
         * Sets the query limit
         *
         * @param limit the query limit
         */
        public void SetLimit(int limit)
        {
            LimitClause = limit;
        }

        /**
         * @see com.socrata.android.soql.clauses.BuildCapable#build()
         */
        public override string Build()
        {
            var parts = new BuildCapable[] { SelectClause, WhereClause, GroupByClause, OrderByClause, new Offset(OffsetClause), new Limit(LimitClause) };
            return string.Join(" ", Expression.BuildAll(parts)).Replace("  ", " ").Trim();
        }

        /**
         * Commodity method to construct a Select initialized with the provided expressions
         */
        public static Select Select(params string[] expressions)
        {
            return Select(Expression.AsExpressions(expressions));
        }

        /**
         * Commodity method to construct a Select initialized with the provided expressions
         */
        public static Select Select(params BuildCapable[] expressions)
        {
            return new Select().Append<Select>(expressions);
        }

        /**
         * Commodity method to construct a Where initialized with the provided expressions
         */
        public static Where Where(params string[] expressions)
        {
            return Where(Expression.AsExpressions(expressions));
        }

        /**
         * Commodity method to construct a Where initialized with the provided expressions
         */
        public static Where Where(params BuildCapable[] expressions)
        {
            return new Where().Append<Where>(expressions);
        }

        /**
         * Commodity method to construct a OrderBy initialized with the provided expressions
         */
        public static OrderBy OrderBy(params string[] expressions)
        {
            return OrderBy(Expression.AsExpressions(expressions));
        }

        /**
         * Commodity method to construct a OrderBy initialized with the provided expressions
         */
        public static OrderBy OrderBy(params BuildCapable[] expressions)
        {
            return new OrderBy().Append<OrderBy>(expressions);
        }

        /**
         * Commodity method to construct a GroupBy initialized with the provided expressions
         */
        public static GroupBy GroupBy(string[] expressions)
        {
            return GroupBy(Expression.AsExpressions(expressions));
        }

        /**
         * Commodity method to construct a GroupBy initialized with the provided expressions
         */
        public static GroupBy GroupBy(params BuildCapable[] expressions)
        {
            return new GroupBy().Append<GroupBy>(expressions);
        }

        /**
         * Commodity method to construct an Offset initialized with the provided value
         */
        public static Offset Offset(int offset)
        {
            return new Offset(offset);
        }

        /**
         * Commodity method to construct a Limit initialized with the provided value
         */
        public static Limit Limit(int limit)
        {
            return new Limit(limit);
        }

        /**
         * Commodity method to construct a GeoBox initialized with bounding box
         * constrained by north, east, south and west values
         */
        public static GeoBox Box(double north, double east, double south, double west)
        {
            return new GeoBox(north, east, south, west);
        }
    }
}
