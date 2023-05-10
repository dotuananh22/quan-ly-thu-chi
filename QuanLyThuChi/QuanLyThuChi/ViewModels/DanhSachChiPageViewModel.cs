using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using QuanLyThuChi.DatabaseConfig;
using QuanLyThuChi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace QuanLyThuChi.ViewModels
{
    public class DanhSachChiPageViewModel : ViewModelBase
    {
        public static DanhSachChiPageViewModel Instance { get; private set; }
        private double _totalCost;
        public double TotalCost
        {
            get { return _totalCost; }
            set { SetProperty(ref _totalCost, value); }
        }
        private readonly Database database = new Database();
        private ObservableCollection<KhoanThuChi> _listKhoanChi;
        public ObservableCollection<KhoanThuChi> ListKhoanChi
        {
            get { return _listKhoanChi; }
            set { SetProperty(ref _listKhoanChi, value); }
        }

        public DanhSachChiPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            GetKhoanChi();
            Instance = this;
        }

        private void CalculateTotalCost()
        {
            TotalCost = 0;
            foreach (var item in ListKhoanChi)
            {
                TotalCost += item.Cost;
            }
        }

        public void GetKhoanChi()
        {
            ListKhoanChi = new ObservableCollection<KhoanThuChi>(database.GetAllKhoanThuChiByCategory(Enums.Category.CHI));
            CalculateTotalCost();
        }
    }
}
