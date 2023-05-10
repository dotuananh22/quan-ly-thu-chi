using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using QuanLyThuChi.DatabaseConfig;
using QuanLyThuChi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace QuanLyThuChi.ViewModels
{
    public class DanhSachThuPageViewModel : ViewModelBase
    {
        public static DanhSachThuPageViewModel Instance { get; private set; }
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
        private KhoanThuChi _khoanThu;
        public KhoanThuChi KhoanThu
        {
            get { return _khoanThu; }
            set { SetProperty(ref _khoanThu, value); }
        }
        private readonly Database database = new Database();
        private ObservableCollection<KhoanThuChi> _listKhoanThu;
        public ObservableCollection<KhoanThuChi> ListKhoanThu
        {
            get { return _listKhoanThu; }
            set { SetProperty(ref _listKhoanThu, value); }
        }
        private ObservableCollection<KhoanThuChi> ListKhoanThuOld;

        public DanhSachThuPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            GetKhoanThu();
            Instance = this;
            PropertyChanged += DanhSachThu_PropertyChanged;
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchText))
            {
                if (SearchText != null)
                {
                    ListKhoanThu.Clear();
                    foreach (var khoanThu in ListKhoanThuOld)
                    {
                        if (khoanThu.Date.ToString("dd/MM").Contains(SearchText)
                            || khoanThu.Title.ToLower().Contains(SearchText))
                        {
                            ListKhoanThu.Add(khoanThu);
                        }
                    }
                }
            }
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

        private void CalculateTotalCost()
        {
            TotalCost = 0;
            foreach (var item in ListKhoanThu)
            {
                TotalCost += item.Cost;
            }
        }

        public void GetKhoanThu()
        {
            ListKhoanThu = new ObservableCollection<KhoanThuChi>(database.GetAllKhoanThuChiByCategory(Enums.Category.THU));
            ListKhoanThuOld = new ObservableCollection<KhoanThuChi>(ListKhoanThu);
            CalculateTotalCost();
        }

        public override void Destroy()
        {
            PropertyChanged -= OnPropertyChanged;
            PropertyChanged -= DanhSachThu_PropertyChanged;
            base.Destroy();
        }
    }
}
