using Example.Core.Models;
using MSC.Socrata.Device.Soql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Core.Services
{
    public interface IUSGSEarthquakeReportsService
    {
        EarthquakeMagnitudeClass[] GetEarthquakeMagnitudeClasses();
        Task<Earthquake> GetEarthquakeByIdAsync(string id);
        Task<Earthquake[]> GetEarthquakesAsync(string query);
        Task<Earthquake[]> GetEarthquakesAsync(Action<Query<Earthquake>> initializeQuery);
    }
}
