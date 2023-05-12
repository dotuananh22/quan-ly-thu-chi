using QuanLyThuChi.ViewModels;
using Xamarin.Forms;

namespace QuanLyThuChi.Views
{
    public partial class ChartPage : BasePage
    {
        public ChartPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ChartPageViewModel.Instance.InitData();
        }
    }
}
