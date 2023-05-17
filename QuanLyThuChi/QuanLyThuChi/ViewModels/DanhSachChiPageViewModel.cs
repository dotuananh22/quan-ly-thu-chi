using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using QuanLyThuChi.Config;
using QuanLyThuChi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace QuanLyThuChi.ViewModels
{
    public class DanhSachChiPageViewModel : ViewModelBase
    {
        public static DanhSachChiPageViewModel Instance { get; private set; }
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
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value, nameof(SearchText)); }
        }
        private double _totalCost;
        public double TotalCost
        {
            get { return _totalCost; }
            set { SetProperty(ref _totalCost, value); }
        }
        private KhoanThuChi _khoanChi;
        public KhoanThuChi KhoanChi
        {
            get { return _khoanChi; }
            set { SetProperty(ref _khoanChi, value); }
        }
        private readonly Database database = new Database();
        private ObservableCollection<KhoanThuChi> _listKhoanChi;
        public ObservableCollection<KhoanThuChi> ListKhoanChi
        {
            get { return _listKhoanChi; }
            set { SetProperty(ref _listKhoanChi, value); }
        }
        private ObservableCollection<KhoanThuChi> ListKhoanChiOld;
        public DelegateCommand SelectedIndexChangedCommand { get; private set; }


        public DanhSachChiPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Instance = this;
            SetDataToMonthPicker();
            SelectedIndexChangedCommand = new DelegateCommand(OnSelectedIndexChangedCommand);
            PropertyChanged += DanhSachChi_PropertyChanged;
            PropertyChanged += OnPropertyChanged;
            InitData();
        }

        public void InitData()
        {
            GetKhoanChi();
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

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchText))
            {
                if (SearchText != null)
                {
                    ListKhoanChi.Clear();
                    foreach (var khoanChi in ListKhoanChiOld)
                    {
                        if (khoanChi.Date.ToString("dd/MM").Contains(SearchText)
                            || khoanChi.Title.ToLower().Contains(SearchText))
                        {
                            ListKhoanChi.Add(khoanChi);
                        }
                    }
                }
            }
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

        private void CalculateTotalCost()
        {
            TotalCost = 0;
            foreach (var item in ListKhoanChi)
            {
                TotalCost += item.Cost;
            }
        }

        private void GetKhoanChi()
        {
            DateTime date = DateTime.ParseExact(MonthSelected, "MMMM", CultureInfo.CurrentCulture);
            ListKhoanChi = new ObservableCollection<KhoanThuChi>(database.GetAllKhoanThuChiByCategoryAndMonth(Enums.Category.CHI, date.Month));
            ListKhoanChiOld = new ObservableCollection<KhoanThuChi>(ListKhoanChi);
            CalculateTotalCost();
        }

        public override void Destroy()
        {
            PropertyChanged -= OnPropertyChanged;
            PropertyChanged -= DanhSachChi_PropertyChanged;
            base.Destroy();
        }
    }
}
