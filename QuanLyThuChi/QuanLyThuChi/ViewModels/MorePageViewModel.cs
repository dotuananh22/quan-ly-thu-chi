using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using QuanLyThuChi.Customs;
using QuanLyThuChi.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace QuanLyThuChi.ViewModels
{
    public class MorePageViewModel : ViewModelBase
    {
        public DelegateCommand HomeTapCommand { get; set; }
        public DelegateCommand SearchTapCommand { get; set; }
        public DelegateCommand ChartTapCommand { get; set; }
        public DelegateCommand AddTapCommand { get; set; }
        public DelegateCommand LanguageTapCommand { get; set; }
        public MorePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            HomeTapCommand = new DelegateCommand(HandleHomeTapCommand);
            SearchTapCommand = new DelegateCommand(HandleSearchTapCommand);
            ChartTapCommand = new DelegateCommand(HandleChartTapCommand);
            AddTapCommand = new DelegateCommand(HandleAddTapCommand);
            LanguageTapCommand = new DelegateCommand(HandleLanguageTapCommand);
        }

        private async void HandleLanguageTapCommand()
        {
            await NavigationService.NavigateAsync("LanguagePage");
        }

        private void HandleAddTapCommand()
        {
            MainPage.Instance.SetPageIndex(2);
        }

        private void HandleChartTapCommand()
        {
            MainPage.Instance.SetPageIndex(3);
        }

        private void HandleSearchTapCommand()
        {
            MainPage.Instance.SetPageIndex(1);
        }

        private void HandleHomeTapCommand()
        {
            MainPage.Instance.SetPageIndex(0);
        }
    }
}
