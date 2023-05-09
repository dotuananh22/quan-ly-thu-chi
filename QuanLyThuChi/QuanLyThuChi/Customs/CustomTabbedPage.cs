using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace QuanLyThuChi.Customs
{
    public class CustomTabbedPage : Xamarin.Forms.TabbedPage
    {
        public CustomTabbedPage()
        {
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            //On<iOS>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}
