using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace QuanLyThuChi.Customs
{
    public class CustomTabbedPageTop : Xamarin.Forms.TabbedPage
    {
        public CustomTabbedPageTop()
        {
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Top);
            //On<iOS>().SetToolbarPlacement(ToolbarPlacement.Top);
        }
    }
}
