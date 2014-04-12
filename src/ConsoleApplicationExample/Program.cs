using MSC.Socrata.Device.Client;
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
            var consumer = new Consumer("soda.demo.socrata.com", "");

            var handle1 = consumer.GetObjectsAsync<Earthquake>("4tka-6guv");
            handle1.Wait();
            Earthquake[] result1 = handle1.Result;

            var handle2 = consumer.GetObjectAsync<Earthquake>("4tka-6guv", "15215665");
            handle2.Wait();

            Earthquake result2 = handle2.Result;

            Console.ReadLine();
        }
    }
}
