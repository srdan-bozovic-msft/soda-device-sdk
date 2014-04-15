using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Example.Core.Services;
using Example.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Core
{
    public class App : MvxApplication
    {
        public App()
        {
            Mvx.RegisterType<IUSGSEarthquakeReportsService, USGSEarthquakeReportsService>();
            RegisterAppStart<HomePageViewModel>();
        }
    }
}
