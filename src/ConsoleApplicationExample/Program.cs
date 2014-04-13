using MSC.Socrata.Device.Client;
using MSC.Socrata.Device.Soql;
using MSC.Socrata.Device.Soql.Clauses;
using MSC.Socrata.Device.Soql.Datatypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //var consumer = new Consumer("soda.demo.socrata.com", "YOUR_TOKEN");
            var consumer = new Consumer("soda.demo.socrata.com");

            var handle1 = consumer.GetObjectsAsync<Earthquake>("4tka-6guv");
            handle1.Wait();
            Earthquake[] result1 = handle1.Result;

            var handle2 = consumer.GetObjectAsync<Earthquake>("4tka-6guv", "15215665");
            handle2.Wait();

            Earthquake result2 = handle2.Result;

            var handle3 = consumer.GetObjectsAsync<Earthquake>("4tka-6guv", "select * where magnitude > 2.0");
            handle3.Wait();
            Earthquake[] result3 = handle3.Result;

            var query4 = new Query<Earthquake>("4tka-6guv");
            query4.AddWhere(Expression.Gt("magnitude", "2.0"));
            var handle4 = consumer.GetObjectsAsync<Earthquake>(query4);
            handle4.Wait();
            Earthquake[] result4 = handle4.Result;

            var query5 = new Query<Earthquake>("4tka-6guv");
            query5.WhereWithinBox("location", new GeoBox(37, -107, 25, -93));
            var handle5 = consumer.GetObjectsAsync<Earthquake>(query5);
            handle5.Wait();
            Earthquake[] result5 = handle5.Result;


            Console.ReadLine();
        }
    }
}
