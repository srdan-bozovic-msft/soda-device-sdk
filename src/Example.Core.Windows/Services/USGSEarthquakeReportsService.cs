using Example.Core.Models;
using MSC.Socrata.Device.Client;
using MSC.Socrata.Device.Soql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Core.Services
{
    public class USGSEarthquakeReportsService : IUSGSEarthquakeReportsService
    {
        private const string DataSet = "4tka-6guv";
        private readonly Consumer _consumer;
        public USGSEarthquakeReportsService()
        {
            //_consumer = new Consumer("soda.demo.socrata.com", "YOUR_TOKEN");
            _consumer = new Consumer("soda.demo.socrata.com");
        }

        public EarthquakeMagnitudeClass[] GetEarthquakeMagnitudeClasses()
        {
            return new[] {
                new EarthquakeMagnitudeClass("Great",8,double.MaxValue),
                new EarthquakeMagnitudeClass("Major",7,8),
                new EarthquakeMagnitudeClass("Strong",6,7),
                new EarthquakeMagnitudeClass("Moderate",5,6),
                new EarthquakeMagnitudeClass("Light",4,5),
                new EarthquakeMagnitudeClass("Minor",3,4),
            };
        }

        public async Task<Earthquake> GetEarthquakeByIdAsync(string id)
        {
            var response = await _consumer.GetObjectAsync<Earthquake>(DataSet, id);
            if (response.HasError)
                throw new Exception(response.Error.Message);
            return response.Entity;
        }

        public async Task<Earthquake[]> GetEarthquakesAsync(string query)
        {
            var response = await _consumer.GetObjectsAsync<Earthquake>(DataSet, query);
            if (response.HasError)
                throw new Exception(response.Error.Message);
            return response.Entity;
        }

        public async Task<Earthquake[]> GetEarthquakesAsync(Action<Query<Earthquake>> initializeQuery)
        {
            var query = new Query<Earthquake>(DataSet);
            initializeQuery(query);
            var response = await _consumer.GetObjectsAsync<Earthquake>(query);
            if (response.HasError)
                throw new Exception(response.Error.Message);
            return response.Entity;
        }
    }
}
