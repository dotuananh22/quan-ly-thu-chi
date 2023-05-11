using Plugin.Media.Abstractions;
using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using QuanLyThuChi.DatabaseConfig;
using QuanLyThuChi.Enums;
using QuanLyThuChi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using System.IO;

namespace QuanLyThuChi.ViewModels
{
    public class AddPageViewModel : ViewModelBase
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
        private ObservableCollection<string> _categoryPicker;
        public ObservableCollection<string> CategoryPicker
        {
            get { return _categoryPicker; }
            set { SetProperty(ref _categoryPicker, value); }
        }

        public DelegateCommand CancelBtnCommand { get; set; }
        public DelegateCommand OKBtnCommand { get; set; }

        private readonly IPageDialogService _dialogService;
        private ImageSource _selectedImage;
        public ImageSource SelectedImage
        {
            get => _selectedImage;
            set => SetProperty(ref _selectedImage, value);
        }
        public DelegateCommand SelectImageCommand { get; }
        private MediaFile file;
        public AddPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            SetData();
            ClearData();
            _dialogService = dialogService;
            SelectImageCommand = new DelegateCommand(SelectImageAsync);
        }

        private async void SelectImageAsync()
        {
            // Check if the device has a camera
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await _dialogService.DisplayAlertAsync("No Camera", "No camera available.", "OK");
                return;
            }

            // Prompt the user to select an image or take a photo
            file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium
            });

            if (file == null)
                return;

            // Display the selected image in an Image control
            SelectedImage = ImageSource.FromFile(file.Path);
        }

        private void SetData()
        {
            SetDataToCategoryPicker();
            CancelBtnCommand = new DelegateCommand(OnCancelBtnClick);
            OKBtnCommand = new DelegateCommand(OnOKBtnClick);
            DateSelected = DateTime.Now;
        }

        private void OnOKBtnClick()
        {
            if (CategorySelected != null && MainTitle != "" && Cost != 0)
            {
                var imageBytes = file != null ? File.ReadAllBytes(file.Path) : null;
                var result = database.CreateKhoanThuChi(new KhoanThuChi
                {
                    Category = CategorySelected == "THU" ? Category.THU : Category.CHI,
                    Title = MainTitle,
                    Comment = Comment,
                    Date = DateSelected,
                    Image = imageBytes,
                    Cost = CategorySelected == "THU" ? Cost : -Cost,
                });

                if (result)
                {
                    DependencyService.Get<Toast>().Show("Thêm thành công");
                }
                else
                {
                    DependencyService.Get<Toast>().Show("Thêm thất bại");
                }
                ClearData();
            }
            else
            {
                DependencyService.Get<Toast>().Show("Vui lòng nhập đầy đủ thông tin!");
            }
        }

        private void ClearData()
        {
            CategorySelected = CategoryPicker[0];
            DateSelected = DateTime.Now;
            MainTitle = "";
            Cost = 0;
            Comment = "";
        }

        private void OnCancelBtnClick()
        {
            ClearData();
        }

        private void SetDataToCategoryPicker()
        {
            CategoryPicker = new ObservableCollection<string>();
            CategoryPicker.Add(Category.THU.ToString());
            CategoryPicker.Add(Category.CHI.ToString());
            CategorySelected = CategoryPicker[0];
        }
    }
}
