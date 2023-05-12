using QuanLyThuChi.Customs;
using QuanLyThuChi.ViewModels;
using System.Diagnostics;
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
            Instance = this;
        }

        public void SetPageIndex(int index)
        {
            if (index < Children.Count)
            {
                CurrentPage = Children[index];
            }
        }
    }
}
