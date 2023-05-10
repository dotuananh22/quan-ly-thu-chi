using Prism;
using Prism.Ioc;
using QuanLyThuChi.DatabaseConfig;
using QuanLyThuChi.Models;
using QuanLyThuChi.ViewModels;
using QuanLyThuChi.Views;
using System;
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

            //Create database and init data
            //InitData();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        private void InitData()
        {
            database.db.DeleteAll<KhoanThuChi>();

            database.CreateKhoanThuChi(new KhoanThuChi
            {
                Category = Enums.Category.THU,
                Title = "Khoản thu này là khoản thu số 1 nhaaaaaaaa",
                Comment = "Trúng vé số trúng vé số trúng vé số trúng vé số trúng vé số trúng vé số trúng vé số",
                Date = new DateTime(2023,5,7),
                Image = "https://images2.thanhnien.vn/Uploaded/maiphuong/2022_08_11/xo-so-tran-ngoc-1352.jpg",
                Cost = 10000000,
            });
            database.CreateKhoanThuChi(new KhoanThuChi
            {
                Category = Enums.Category.THU,
                Title = "Khoản thu 2",
                Comment = "Trúng số tiếp",
                Date = new DateTime(2023, 5, 10),
                Image = "https://images2.thanhnien.vn/Uploaded/maiphuong/2022_08_11/xo-so-tran-ngoc-1352.jpg",
                Cost = 2000000,
            });
            database.CreateKhoanThuChi(new KhoanThuChi
            {
                Category = Enums.Category.CHI,
                Title = "Khoản chi này là khoản chi số 1 nhaaaa nhaaaaaaa",
                Comment = "Mua đồ ăn vặt Mua đồ ăn vặt Mua đồ ăn vặt Mua đồ ăn vặt",
                Date = new DateTime(2023, 5, 4),
                Image = "https://cdn.tgdd.vn/Files/2020/12/16/1314124/thuc-an-nhanh-la-gi-an-thuc-an-nhanh-co-tot-hay-khong-202201201405201587.jpg",
                Cost = -1100000,
            });
            database.CreateKhoanThuChi(new KhoanThuChi
            {
                Category = Enums.Category.CHI,
                Title = "Khoản chi 2",
                Comment = "Mua đồ ăn part 2",
                Date = new DateTime(2023, 5, 8),
                Image = "https://cdn.tgdd.vn/Files/2020/12/16/1314124/thuc-an-nhanh-la-gi-an-thuc-an-nhanh-co-tot-hay-khong-202201201405201587.jpg",
                Cost = -1200000,
            });
            database.CreateKhoanThuChi(new KhoanThuChi
            {
                Category = Enums.Category.CHI,
                Title = "Khoản chi 3",
                Comment = "Mua sách",
                Date = new DateTime(2023, 5, 10),
                Image = "https://lzd-img-global.slatic.net/g/p/7220e9ad826bf262581f6d8567590a4a.jpg_720x720q80.jpg",
                Cost = -200000,
            });
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
        }
    }
}
