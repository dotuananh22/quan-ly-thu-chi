using Prism;
using Prism.Ioc;
using QuanLyThuChi.ViewModels;
using QuanLyThuChi.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace QuanLyThuChi
{
    public partial class App
    {
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
        }
    }
}
