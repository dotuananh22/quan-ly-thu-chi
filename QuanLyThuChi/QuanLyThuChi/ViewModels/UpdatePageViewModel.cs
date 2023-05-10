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
    public class UpdatePageViewModel : ViewModelBase
    {
        private readonly Database database = new Database();
        private string _categorySelected;
        public string CategorySelected
        {
            get { return _categorySelected; }
            set { SetProperty(ref _categorySelected, value); }
        }
        private DateTime _dateSelected;
        public DateTime DateSelected
        {
            get { return _dateSelected; }
            set { SetProperty(ref _dateSelected, value); }
        }
        private DateTime _maxDate = DateTime.Now;

        public DateTime MaxDate
        {
            get { return _maxDate; }
            set { SetProperty(ref _dateSelected, value); }
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
        private Guid KhoanThuChiId;

        private ObservableCollection<string> _categoryPicker;
        public ObservableCollection<string> CategoryPicker
        {
            get { return _categoryPicker; }
            set { SetProperty(ref _categoryPicker, value); }
        }

        public DelegateCommand CancelBtnCommand { get; set; }
        public DelegateCommand UpdateBtnCommand { get; set; }
        public UpdatePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            CancelBtnCommand = new DelegateCommand(OnCancelBtnClick);
            UpdateBtnCommand = new DelegateCommand(OnUpdateBtnClick);
            SetDataToCategoryPicker();
        }

        private async void OnUpdateBtnClick()
        {
            if (CategorySelected != null && MainTitle != "" && Cost != 0)
            {
                var result = database.UpdateKhoanThuChi(new KhoanThuChi
                {
                    Id = KhoanThuChiId,
                    Category = CategorySelected == "THU" ? Category.THU : Category.CHI,
                    Title = MainTitle,
                    Comment = Comment,
                    Date = DateSelected,
                    //Image = "https://images2.thanhnien.vn/Uploaded/maiphuong/2022_08_11/xo-so-tran-ngoc-1352.jpg",
                    Cost = Cost,
                });

                if (result)
                {
                    DependencyService.Get<Toast>().Show("Cập nhật thành công");
                }
                else
                {
                    DependencyService.Get<Toast>().Show("Cập nhật thất bại");
                }
                await NavigationService.GoBackAsync();
            }
            else
            {
                DependencyService.Get<Toast>().Show("Vui lòng nhập đầy đủ thông tin!");
            }
        }

        private async void OnCancelBtnClick()
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
            setDataToView();
        }

        private void setDataToView()
        {
            KhoanThuChi khoanThuChiById = database.GetKhoanThuChiById(KhoanThuChiId);
            if (khoanThuChiById != null)
            {
                CategorySelected = khoanThuChiById.Category.ToString();
                DateSelected = khoanThuChiById.Date;
                MainTitle = khoanThuChiById.Title;
                Comment = khoanThuChiById?.Comment;
                Cost = khoanThuChiById.Cost;
            }
        }
        private void SetDataToCategoryPicker()
        {
            CategoryPicker = new ObservableCollection<string>();
            CategoryPicker.Add(Category.THU.ToString());
            CategoryPicker.Add(Category.CHI.ToString());
        }
    }
}
