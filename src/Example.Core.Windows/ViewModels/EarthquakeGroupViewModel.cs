using Example.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Core.ViewModels
{
    public class EarthquakeGroupViewModel : List<EarthquakeItemViewModel>
    {
        private EarthquakeMagnitudeClass _model;
        public string Key { get { return _model.Name; } }

        public EarthquakeGroupViewModel(EarthquakeMagnitudeClass model)
        {
            _model = model;
        }
    }
}
