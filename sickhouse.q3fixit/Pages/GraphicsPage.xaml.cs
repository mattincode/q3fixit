using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using sickhouse.q3fixit.ViewModels;

namespace sickhouse.q3fixit.Pages
{
    /// <summary>
    /// Interaction logic for GraphicsPage.xaml
    /// </summary>
    public partial class GraphicsPage : UserControl
    {
        public GraphicsPage()
        {
            InitializeComponent();
            DataContext = new GraphicsPageViewModel(OnLoadDone);
        }

        public void OnLoadDone(object obj)
        {
            ProgressBar.Visibility = Visibility.Hidden;
        }
    }
}
