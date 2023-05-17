using Prism;
using Prism.AppModel;
using Prism.Ioc;
using Prism.Services;
using QuanLyThuChi.Config;
using QuanLyThuChi.Models;
using QuanLyThuChi.ViewModels;
using QuanLyThuChi.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace QuanLyThuChi
{
    public partial class App
    {
        private readonly Database database = new Database();
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SearchPage, SearchPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<AddPage, AddPageViewModel>();
            containerRegistry.RegisterForNavigation<ChartPage, ChartPageViewModel>();
            containerRegistry.RegisterForNavigation<MorePage, MorePageViewModel>();
            containerRegistry.RegisterForNavigation<DanhSachThuPage, DanhSachThuPageViewModel>();
            containerRegistry.RegisterForNavigation<DanhSachChiPage, DanhSachChiPageViewModel>();
            containerRegistry.RegisterForNavigation<KhoanThuChiDetailPage, KhoanThuChiDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<UpdatePage, UpdatePageViewModel>();
            containerRegistry.RegisterSingleton<IPageDialogService, PageDialogService>();
            containerRegistry.RegisterForNavigation<LanguagePage, LanguagePageViewModel>();
            containerRegistry.RegisterSingleton<LocalizationService>();
        }
    }
}
