using Example.Core.Models;
using Example.Core.Services;
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
            var service = new USGSEarthquakeReportsService();

            var handle1 = service.GetEarthquakesAsync("");
            handle1.Wait();
            var result1 = handle1.Result;

            var handle2 = service.GetEarthquakeByIdAsync("15215665");
            handle2.Wait();
            var result2 = handle2.Result;

            var handle3 = service.GetEarthquakesAsync("select * where magnitude > 2.0");
            handle3.Wait();
            var result3 = handle3.Result;

            var handle4 = service.GetEarthquakesAsync(q => q.AddWhere(Expression.Gt("magnitude", "2.0")));
            handle4.Wait();
            var result4 = handle4.Result;

            var handle5 = service.GetEarthquakesAsync(q => q.WhereWithinBox("location", new GeoBox(37, -107, 25, -93)));
            handle5.Wait();
            var result5 = handle5.Result;


            Console.ReadLine();
        }
    }
}
