using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using QuanLyThuChi.Config;
using QuanLyThuChi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace QuanLyThuChi.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        public static SearchPageViewModel Instance { get; set; }
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
        private KhoanThuChi _khoanThuChi;
        public KhoanThuChi KhoanThuChi
        {
            get { return _khoanThuChi; }
            set { SetProperty(ref _khoanThuChi, value); }
        }
        private readonly Database database = new Database();
        private ObservableCollection<KhoanThuChi> _ListKhoanThuChi;
        public ObservableCollection<KhoanThuChi> ListKhoanThuChi
        {
            get { return _ListKhoanThuChi; }
            set { SetProperty(ref _ListKhoanThuChi, value); }
        }
        private ObservableCollection<KhoanThuChi> ListKhoanThuChiOld;
        public SearchPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            GetKhoanThuChi();
            Instance = this;
            PropertyChanged += DanhSachThuChi_PropertyChanged;
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchText))
            {
                if (SearchText != "")
                {
                    ListKhoanThuChi.Clear();
                    foreach (var khoanThuChi in ListKhoanThuChiOld)
                    {
                        if (khoanThuChi.Date.ToString("dd/MM").Contains(SearchText)
                            || khoanThuChi.Title.ToLower().Contains(SearchText))
                        {
                            ListKhoanThuChi.Add(khoanThuChi);
                        }
                    }
                }
                else
                {
                    ListKhoanThuChi.Clear();
                }
            }
        }

        private async void DanhSachThuChi_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "KhoanThuChi")
            {
                if (KhoanThuChi != null)
                {
                    var p = new NavigationParameters();
                    p.Add("id", KhoanThuChi.Id);
                    await NavigationService.NavigateAsync("KhoanThuChiDetailPage", p);
                }
            }
        }

        public void GetKhoanThuChi()
        {
            ListKhoanThuChiOld = new ObservableCollection<KhoanThuChi>(database.GetAllKhoanThuChi());
            ListKhoanThuChi = new ObservableCollection<KhoanThuChi>();
        }

        public override void Destroy()
        {
            PropertyChanged -= OnPropertyChanged;
            PropertyChanged -= DanhSachThuChi_PropertyChanged;
            base.Destroy();
        }
    }
}
