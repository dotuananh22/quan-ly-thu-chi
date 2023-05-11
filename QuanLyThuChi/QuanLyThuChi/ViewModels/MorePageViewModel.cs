using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyThuChi.ViewModels
{
    public class MorePageViewModel : ViewModelBase
    {
        public DelegateCommand HomeTapCommand { get; set; }
        public MorePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            HomeTapCommand = new DelegateCommand(HandleHomeTapCommand);
        }

        private async void HandleHomeTapCommand()
        {
            await NavigationService.NavigateAsync("MainPage/HomePage");
        }
    }
}
