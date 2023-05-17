using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using QuanLyThuChi.Config;
using QuanLyThuChi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;

namespace QuanLyThuChi.ViewModels
{
    public class LanguagePageViewModel : ViewModelBase
    {
        private readonly LocalizationService _localizationService;
        private int _selectedLanguageIndex;

        public DelegateCommand BackCommand { get; set; }

        public LanguagePageViewModel(INavigationService navigationService, LocalizationService localizationService)
            : base(navigationService)
        {
            _localizationService = localizationService;
            BackCommand = new DelegateCommand(HandleBack);

            LanguageOptions = new List<LanguageOption>
            {
                new LanguageOption { DisplayName = "English", CultureInfo = new CultureInfo("en-US") },
                new LanguageOption { DisplayName = "Vietnamese", CultureInfo = new CultureInfo("vi-VN") }
            };
        }
        public List<LanguageOption> LanguageOptions { get; }
        public int SelectedLanguageIndex
        {
            get => _selectedLanguageIndex;
            set
            {
                if (SetProperty(ref _selectedLanguageIndex, value))
                {
                    var selectedLanguage = LanguageOptions.ElementAtOrDefault(value);
                    if (selectedLanguage != null)
                    {
                        CultureInfo.CurrentUICulture = selectedLanguage.CultureInfo;
                        RaisePropertyChanged(nameof(LocalizedStrings));
                    }
                }
            }
        }
        public dynamic LocalizedStrings => new
        {
            Title = _localizationService.GetString("Test"),
        };

        private async void HandleBack()
        {
            await NavigationService.GoBackAsync();
        }

        //private void OnSelectedIndexChangedCommand()
        //{
        //    AppSettings.Language = LanguageSelected == LanguagePicker[0] ? "vi" : "en";
        //    CultureInfo language = new CultureInfo(AppSettings.Language);
        //    Thread.CurrentThread.CurrentUICulture = language;
        //}

        //private void SetDataToLanguagePicker()
        //{
        //    LanguageOptions = new List<LanguageOption>
        //    {
        //        new LanguageOption { DisplayName = "English", CultureInfo = new CultureInfo("en-US") },
        //        new LanguageOption { DisplayName = "Vietnamese", CultureInfo = new CultureInfo("vi-VN") }
        //    };
        //}
    }
}
