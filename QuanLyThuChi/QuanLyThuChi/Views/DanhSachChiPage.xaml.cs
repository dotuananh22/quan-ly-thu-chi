using QuanLyThuChi.ViewModels;
using Xamarin.Forms;

namespace QuanLyThuChi.Views
{
    public partial class DanhSachChiPage : BasePage
    {
        public DanhSachChiPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DanhSachChiPageViewModel.Instance.GetKhoanChi();
        }
    }
}
