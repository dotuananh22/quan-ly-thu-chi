using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using QuanLyThuChi.DatabaseConfig;
using QuanLyThuChi.Enums;
using QuanLyThuChi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace QuanLyThuChi.ViewModels
{
    public class KhoanThuChiDetailPageViewModel : ViewModelBase
    {
        private Guid KhoanThuChiId;
        private readonly Database database = new Database();
        private KhoanThuChi _khoanThu;
        public KhoanThuChi KhoanThu
        {
            get { return _khoanThu; }
            set { SetProperty(ref _khoanThu, value); }
        }
        private string _categorySelected;
        public string CategorySelected
        {
            get { return _categorySelected; }
            set { SetProperty(ref _categorySelected, value); }
        }
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }
        private string _mainTitle;
        public string MainTitle
        {
            get { return _mainTitle; }
            set { SetProperty(ref _mainTitle, value); }
        }
        private double _cost;
        public double Cost
        {
            get { return _cost; }
            set { SetProperty(ref _cost, value); }
        }
        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand DeleteBtnCommand { get; set; }
        public DelegateCommand UpdateBtnCommand { get; set; }
        public KhoanThuChiDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            BackCommand = new DelegateCommand(HandleBack);
            DeleteBtnCommand = new DelegateCommand(OnDeleteBtnCommand);
            UpdateBtnCommand = new DelegateCommand(OnUpdateBtnCommand);
        }

        private void OnDeleteBtnCommand()
        {
            
            HandleDelete();
        }

        private void OnUpdateBtnCommand()
        {
            Debug.WriteLine("Update");
        }

        private async void HandleDelete()
        {
            var result = database.DeleteKhoanThuChi(KhoanThuChiId);
            if (result)
            {
                DependencyService.Get<Toast>().Show("Xóa thành công");
            }
            else
            {
                DependencyService.Get<Toast>().Show("Xóa thất bại");
            }
            await NavigationService.GoBackAsync();
        }

        private async void HandleBack()
        {
            await NavigationService.GoBackAsync();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("id"))
            {
                KhoanThuChiId = parameters.GetValue<Guid>("id");
            }
            getChiTietKhoanThu(KhoanThuChiId);
        }

        private void getChiTietKhoanThu(Guid khoanThuChiId)
        {
            KhoanThu = database.GetKhoanThuChiById(khoanThuChiId);
            if (KhoanThu != null)
            {
                CategorySelected = KhoanThu.Category.ToString();
                Date = KhoanThu.Date;
                MainTitle = KhoanThu.Title;
                Comment = KhoanThu.Comment;
                Cost = KhoanThu.Cost;
            }
        }
    }
}
