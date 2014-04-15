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
    }
}
