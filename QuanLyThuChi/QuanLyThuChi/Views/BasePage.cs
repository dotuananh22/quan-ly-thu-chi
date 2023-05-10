using QuanLyThuChi.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    try
        //    {
        //        //var viewModel = (ViewModelBase)BindingContext;
        //        //viewModel?.OnAppearing();
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e);
        //    }
        //}
    }
}
