using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyThuChi.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        public SearchPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}
