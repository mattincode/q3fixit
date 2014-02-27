using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using sickhouse.q3fixit.ViewModels;

namespace sickhouse.q3fixit.Pages
{
    /// <summary>
    /// Interaction logic for NetworkPage.xaml
    /// </summary>
    public partial class NetworkPage : UserControl
    {
        public NetworkPage()
        {
            InitializeComponent();
            DataContext = new NetworkPageViewModel(OnLoadDone);
        }

        public void OnLoadDone(object obj)
        {
            ProgressBar.Visibility = Visibility.Hidden;
        }
    }
}
