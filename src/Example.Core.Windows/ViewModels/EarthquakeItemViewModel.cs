using Example.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Core.ViewModels
{
    public class EarthquakeItemViewModel
    {
        private Earthquake _model;
        public EarthquakeItemViewModel(Earthquake model)
        {
            _model = model;
        }

        public string Region
        {
            get { return _model.Region; }
        }

        public string DateTime
        {
            get { return _model.DateTime.ToString("yyyy-MM-dd'T'HH:mm:s"); }
        }

        public string Magnitude
        {
            get { return string.Format("{0:.00}", _model.Magnitude); }
        }

    }
}
