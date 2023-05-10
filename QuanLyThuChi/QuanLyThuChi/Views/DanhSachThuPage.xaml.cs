using QuanLyThuChi.ViewModels;
using Xamarin.Forms;

namespace QuanLyThuChi.Views
{
    public partial class DanhSachThuPage : BasePage
    {
        public DanhSachThuPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DanhSachThuPageViewModel.Instance.GetKhoanThu();
        }
    }
}
