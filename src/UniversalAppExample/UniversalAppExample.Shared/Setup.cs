using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsStore.Platform;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace UniversalAppExample
{
    public class Setup : MvxStoreSetup
    {
        public Setup(Frame rootFrame)
            : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Example.Core.App();
        }
    }
}
