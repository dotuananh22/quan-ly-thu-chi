using QuanLyThuChi.ViewModels;
using Xamarin.Forms;

namespace QuanLyThuChi.Views
{
    public partial class SearchPage : BasePage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SearchPageViewModel.Instance.GetKhoanThuChi();
        }
    }
}
