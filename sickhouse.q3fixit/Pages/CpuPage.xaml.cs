using System.Windows;
using System.Windows.Controls;
using sickhouse.q3fixit.ViewModels;

namespace sickhouse.q3fixit.Pages
{
    /// <summary>
    /// Interaction logic for CpuPage.xaml
    /// </summary>
    public partial class CpuPage : UserControl
    {
        public CpuPage()
        {
            InitializeComponent();
            DataContext = new CpuPageViewModel(OnLoadDone);
        }

        public void OnLoadDone(object obj)
        {
            ProgressBar.Visibility = Visibility.Hidden;
        }
    }
}
