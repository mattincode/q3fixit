using System;
using System.Windows;
using System.Windows.Forms;
using FirstFloor.ModernUI.Windows.Controls;
using sickhouse.q3fixit.ViewModels;
using UserControl = System.Windows.Controls.UserControl;

namespace sickhouse.q3fixit.Pages
{
    /// <summary>
    /// Interaction logic for QuakeFixPage.xaml
    /// </summary>
    public partial class QuakeFixPage : UserControl
    {
        private QuakeFixPageViewModel _vm = new QuakeFixPageViewModel(onRunAction);

        public QuakeFixPage()
        {
            InitializeComponent();
            DataContext = _vm;
        }

        private static void onRunAction(object obj)
        {            
            var result = ModernDialog.ShowMessage("Din q3config är nu uppdaterad.", "Klart!",  MessageBoxButton.OK);
        }

        private void btnBrowseQ3Folder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.SelectedPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);                     
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                _vm.Q3Folder = dialog.SelectedPath;
            }
        }
       
    }
}
