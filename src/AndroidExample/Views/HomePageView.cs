using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.CrossCore.Converters;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using System.Collections;
using Example.Core.ViewModels;
using System.Globalization;
using System.Collections.ObjectModel;

namespace AndroidExample.Views
{
    [Activity(Label = "Soda Device SDK Android Example")]
    public class HomePageView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.HomePageView);
            var sessionListView = FindViewById<MvxListView>(Resource.Id.EarthquakeList);
            sessionListView.Adapter = new GroupedListAdapter(this, (IMvxAndroidBindingContext)BindingContext, KeyValueConverter);
        }

        protected virtual IMvxValueConverter KeyValueConverter
        {
            get
            {
                return null;
            }
        }


        public class GroupedListAdapter : MvxAdapter, ISectionIndexer
        {
            private Java.Lang.Object[] _sectionHeaders;
            private List<int> _sectionLookup;
            private List<int> _reverseSectionLookup;

            private readonly IMvxValueConverter _keyConverter;

            public GroupedListAdapter(Context context, IMvxAndroidBindingContext bindingContext, IMvxValueConverter keyConverter)
                : base(context, bindingContext)
            {
                _keyConverter = keyConverter;
            }

            protected override void SetItemsSource(IEnumerable list)
            {
                var groupedList = list as ObservableCollection<EarthquakeGroupViewModel>;

                if (groupedList == null)
                {
                    _sectionHeaders = null;
                    _sectionLookup = null;
                    _reverseSectionLookup = null;
                    base.SetItemsSource(null);
                    return;
                }

                var flattened = new List<object>();
                _sectionLookup = new List<int>();
                _reverseSectionLookup = new List<int>();
                var sectionHeaders = new List<string>();

                var groupsSoFar = 0;
                foreach (var group in groupedList)
                {
                    _sectionLookup.Add(flattened.Count);
                    var groupHeader = GetGroupHeader(group);
                    sectionHeaders.Add(groupHeader);
                    for (int i = 0; i <= group.Count; i++)
                        _reverseSectionLookup.Add(groupsSoFar);

                    flattened.Add(groupHeader);
                    flattened.AddRange(group.Select(x => (object)x));

                    groupsSoFar++;
                }

                _sectionHeaders = CreateJavaStringArray(sectionHeaders.Select(x => x.Length > 10 ? x.Substring(0, 10) : x).ToList());

                base.SetItemsSource(flattened);
            }

            private string GetGroupHeader(EarthquakeGroupViewModel group)
            {
                if (_keyConverter == null)
                    return group.Key.ToString();

                return (string)_keyConverter.Convert(group.Key, typeof(string), null, CultureInfo.CurrentUICulture);
            }

            public int GetPositionForSection(int section)
            {
                if (_sectionLookup == null)
                    return 0;

                return _sectionLookup[section];
            }

            public int GetSectionForPosition(int position)
            {
                if (_reverseSectionLookup == null)
                    return 0;

                return _reverseSectionLookup[position];
            }

            public Java.Lang.Object[] GetSections()
            {
                return _sectionHeaders;
            }

            private static Java.Lang.Object[] CreateJavaStringArray(List<string> inputList)
            {
                if (inputList == null)
                    return null;

                var toReturn = new Java.Lang.Object[inputList.Count];
                for (var i = 0; i < inputList.Count; i++)
                {
                    toReturn[i] = new Java.Lang.String(inputList[i]);
                }

                return toReturn;
            }

            public override int GetItemViewType(int position)
            {
                var item = GetRawItem(position);
                if (item is EarthquakeItemViewModel)
                    return 0;
                return 1;
            }

            public override int ViewTypeCount
            {
                get { return 2; }
            }

            protected override global::Android.Views.View GetBindableView(global::Android.Views.View convertView, object dataContext, int templateId)
            {
                if (dataContext is EarthquakeItemViewModel)
                    return base.GetBindableView(convertView, dataContext, Resource.Layout.ListItem_Earthquake);
                else
                    return base.GetBindableView(convertView, dataContext, Resource.Layout.ListItem_SeparatorToString);
            }
        }

    }
}