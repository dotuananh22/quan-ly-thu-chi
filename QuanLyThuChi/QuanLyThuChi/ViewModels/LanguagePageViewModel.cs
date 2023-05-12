using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace QuanLyThuChi.ViewModels
{
    public class LanguagePageViewModel : ViewModelBase
    {
        private List<string> _languagePicker;

        public List<string> LanguagePicker
        {
            get => _languagePicker;
            set => SetProperty(ref _languagePicker, value);
        }
        private string _languageSelected;

        public string LanguageSelected
        {
            get => _languageSelected;
            set => SetProperty(ref _languageSelected, value);
        }
        public DelegateCommand SelectedIndexChangedCommand { get; private set; }
        public DelegateCommand BackCommand { get; set; }

        public LanguagePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            SelectedIndexChangedCommand = new DelegateCommand(OnSelectedIndexChangedCommand);
            BackCommand = new DelegateCommand(HandleBack);
            LanguagePicker = new List<string>();
            SetDataToLanguagePicker();
        }

        private async void HandleBack()
        {
            await NavigationService.GoBackAsync();
        }

        private void OnSelectedIndexChangedCommand()
        {
            Debug.WriteLine("Language: " + LanguageSelected);
        }

        private void SetDataToLanguagePicker()
        {
            LanguagePicker.Add("Tiếng Việt");
            LanguagePicker.Add("Tiếng Anh");
            LanguageSelected = LanguagePicker[0];
        }
    }
}
