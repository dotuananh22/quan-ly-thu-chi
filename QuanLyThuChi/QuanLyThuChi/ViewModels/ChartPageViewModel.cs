using Microcharts;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using QuanLyThuChi.DatabaseConfig;
using QuanLyThuChi.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace QuanLyThuChi.ViewModels
{
    public class ChartPageViewModel : ViewModelBase
    {
        private readonly Database database = new Database();
        public static ChartPageViewModel Instance { get; private set; }
        private bool _isVisible;

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }
        private bool _isNotData;

        public bool IsNotData
        {
            get => _isNotData;
            set => SetProperty(ref _isNotData, value);
        }
        private List<string> _monthPicker;

        public List<string> MonthPicker
        {
            get => _monthPicker;
            set => SetProperty(ref _monthPicker, value);
        }
        private string _monthSelected;

        public string MonthSelected
        {
            get => _monthSelected;
            set => SetProperty(ref _monthSelected, value);
        }
        private ObservableCollection<ChartEntry> _data;
        public ObservableCollection<ChartEntry> Data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
        }
        private Chart _pieChart;

        public Chart PieChart
        {
            get => _pieChart;
            set => SetProperty(ref _pieChart, value);
        }
        private double _tongThu;
        public double TongThu
        {
            get => _tongThu;
            set => SetProperty(ref _tongThu, value);
        }
        private double _tongChi;
        public double TongChi
        {
            get => _tongChi;
            set => SetProperty(ref _tongChi, value);
        }
        private KhoanThuChi _khoanThu;
        public KhoanThuChi KhoanThu
        {
            get { return _khoanThu; }
            set { SetProperty(ref _khoanThu, value); }
        }
        private ObservableCollection<KhoanThuChi> _listKhoanThu;
        public ObservableCollection<KhoanThuChi> ListKhoanThu
        {
            get { return _listKhoanThu; }
            set { SetProperty(ref _listKhoanThu, value); }
        }
        private KhoanThuChi _khoanChi;
        public KhoanThuChi KhoanChi
        {
            get { return _khoanChi; }
            set { SetProperty(ref _khoanChi, value); }
        }
        private ObservableCollection<KhoanThuChi> _listKhoanChi;
        public ObservableCollection<KhoanThuChi> ListKhoanChi
        {
            get { return _listKhoanChi; }
            set { SetProperty(ref _listKhoanChi, value); }
        }
        public DelegateCommand SelectedIndexChangedCommand { get; private set; }

        public ChartPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Data = new ObservableCollection<ChartEntry>();
            SelectedIndexChangedCommand = new DelegateCommand(OnSelectedIndexChangedCommand);
            PropertyChanged += DanhSachThu_PropertyChanged;
            PropertyChanged += DanhSachChi_PropertyChanged;
            SetDataToMonthPicker();
            InitData();
            Instance = this;
        }

        public void InitData()
        {
            CreateChart();
            GetDataToChartByMonth(MonthSelected);
            SetDataToChart();
            GetKhoanThu();
            GetKhoanChi();
            CheckData();
        }

        private void CheckData()
        {
            IsVisible = TongThu == 0 && TongChi == 0 ? false : true;
            IsNotData = !IsVisible;
        }

        private async void DanhSachThu_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "KhoanThu")
            {
                if (KhoanThu != null)
                {
                    var p = new NavigationParameters();
                    p.Add("id", KhoanThu.Id);
                    await NavigationService.NavigateAsync("KhoanThuChiDetailPage", p);
                }
            }
        }
        public void GetKhoanThu()
        {
            DateTime date = DateTime.ParseExact(MonthSelected, "MMMM", CultureInfo.CurrentCulture);
            ListKhoanThu = new ObservableCollection<KhoanThuChi>(database.GetTop3KhoanThuByMonth(date.Month));
        }

        private async void DanhSachChi_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "KhoanChi")
            {
                if (KhoanChi != null)
                {
                    var p = new NavigationParameters();
                    p.Add("id", KhoanChi.Id);
                    await NavigationService.NavigateAsync("KhoanThuChiDetailPage", p);
                }
            }
        }
        public void GetKhoanChi()
        {
            DateTime date = DateTime.ParseExact(MonthSelected, "MMMM", CultureInfo.CurrentCulture);
            ListKhoanChi = new ObservableCollection<KhoanThuChi>(database.GetTop3KhoanChiByMonth(date.Month));
        }

        public override void Destroy()
        {
            PropertyChanged -= DanhSachThu_PropertyChanged;
            PropertyChanged -= DanhSachChi_PropertyChanged;
            base.Destroy();
        }

        private void OnSelectedIndexChangedCommand()
        {
            InitData();
        }

        private void SetDataToMonthPicker()
        {
            MonthPicker = new List<string>();
            int year = DateTime.Now.Year; // The current year
            string currentMonth = new DateTime(year, DateTime.Now.Month, 1).ToString("MMMM");
            for (int i = 1; i <= 12; i++)
            {
                MonthPicker.Add(new DateTime(year, i, 1).ToString("MMMM"));
            }
            MonthSelected = currentMonth;
        }

        private void CreateChart()
        {
            PieChart = null;
            PieChart = new DonutChart
            {
                Entries = Data,
                HoleRadius = 0,
                LabelMode = LabelMode.None,
            };
        }

        private void GetDataToChartByMonth(string month)
        {
            DateTime date = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture);
            TongThu = database.GetTotalCostKhoanThuChiByMonth(Enums.Category.THU, date.Month);
            TongChi = database.GetTotalCostKhoanThuChiByMonth(Enums.Category.CHI, date.Month);
        }

        private void SetDataToChart()
        {
            if (Data != null)
            {
                Data.Clear();
            }
            Data.Add(new ChartEntry((float)TongThu)
            {
                Label = "Khoản thu",
                ValueLabel = TongThu.ToString(),
                Color = SKColor.Parse("#00A651"),
                ValueLabelColor = SKColor.Parse("#00A651"),
            }); Data.Add(new ChartEntry((float)(-TongChi))
            {
                Label = "Khoản chi",
                ValueLabel = (-TongChi).ToString(),
                Color = SKColor.Parse("#ED1C24"),
                ValueLabelColor = SKColor.Parse("#ED1C24"),
            });
        }
    }
}
