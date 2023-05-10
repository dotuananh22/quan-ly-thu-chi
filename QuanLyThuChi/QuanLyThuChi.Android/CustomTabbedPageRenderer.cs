using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Tabs;
using QuanLyThuChi.Droid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using Xamarin.Forms.Platform.Android;
using QuanLyThuChi.Customs;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]

namespace QuanLyThuChi.Droid
{
    [Obsolete]
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        public class TabbedPageRenderer : TabbedRenderer
        {
            public TabbedPageRenderer(Context context) : base(context) { }

            private Activity _activity;
            protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
            {
                base.OnElementChanged(e);
                _activity = this.Context as Activity;
            }

            protected override void OnLayout(bool changed, int l, int t, int r, int b)
            {
                ActionBar actionBar = _activity.ActionBar;
                if (actionBar != null) actionBar.NavigationMode = ActionBarNavigationMode.Standard;
                base.OnLayout(changed, l, t, r, b);
            }
        }
    }
}