using Cirrious.MvvmCross.ViewModels;
using Example.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Core.ViewModels
{
    public class HomePageViewModel : MvxViewModel
    {
        private readonly IUSGSEarthquakeReportsService _earthquakeReportService;

        private ObservableCollection<EarthquakeGroupViewModel> _earthquakeGroups;

        public ObservableCollection<EarthquakeGroupViewModel> EarthquakeGroups
        {
            get
            {
                return _earthquakeGroups;
            }
            set
            {
                _earthquakeGroups = value;
                RaisePropertyChanged(() => EarthquakeGroups);
            }
        }

        public HomePageViewModel(IUSGSEarthquakeReportsService earthquakeReportService)
        {
            _earthquakeReportService = earthquakeReportService;
        }

        public async override void Start()
        {
            var earthquakeClasses = _earthquakeReportService.GetEarthquakeMagnitudeClasses();
            var earthquakes = await _earthquakeReportService.GetEarthquakesAsync("select * where magnitude >= 3.0");
            var earthquakeGroups = new ObservableCollection<EarthquakeGroupViewModel>();
            foreach (var group in earthquakeClasses)
            {
                var items = earthquakes.Where(e => group.IsInClass(e.Magnitude));
                if(items.Count()>0)
                {
                    var earthquakeGroupViewModel = new EarthquakeGroupViewModel(group);
                    foreach (var item in items)
                    {
                        earthquakeGroupViewModel.Items.Add(new EarthquakeItemViewModel(item));
                    }
                    earthquakeGroups.Add(earthquakeGroupViewModel);
                }
            }
            EarthquakeGroups = earthquakeGroups;
            base.Start();
        }
    }
}
