using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QuanLyThuChi.Views
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
