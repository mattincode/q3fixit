using System.Windows;
using System.Windows.Forms;
using FirstFloor.ModernUI.Windows.Controls;
using sickhouse.q3fixit.ViewModels;
using UserControl = System.Windows.Controls.UserControl;

namespace sickhouse.q3fixit.Pages
{
    /// <summary>
    /// Interaction logic for MapCheckerPage.xaml
    /// </summary>
    public partial class MapCheckPage : UserControl
    {
        private MapCheckPageViewModel _vm;

        public MapCheckPage()
        {
            InitializeComponent();
            _vm = new MapCheckPageViewModel(OnLoadDone);
            DataContext = _vm;
        }

        private void OnLoadDone(string text)
        {
          //  var result = ModernDialog.ShowMessage(text, "Mapchecker klar", MessageBoxButton.OK);
        }

        private void btnBrowseQ3Folder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.SelectedPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _vm.Q3Folder = dialog.SelectedPath;
            }
        }
    }
}
