using Example.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Core.ViewModels
{
    public class EarthquakeGroupViewModel
    {
        private EarthquakeMagnitudeClass _model;
        public string Group { get { return _model.Name; } }
        public ObservableCollection<EarthquakeItemViewModel> Items { get; private set; }

        public EarthquakeGroupViewModel(EarthquakeMagnitudeClass model)
        {
            Items = new ObservableCollection<EarthquakeItemViewModel>();
        }
    }
}
