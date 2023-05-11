using QuanLyThuChi.Customs;
using Xamarin.Forms;

namespace QuanLyThuChi.Views
{
    public partial class MainPage: CustomTabbedPageBottom
    {
        public static MainPage Instance { get; private set; }
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
          
        }

        public void SetPageIndex(int index)
        {
            SelectedItem = index;
        }
    }
}
