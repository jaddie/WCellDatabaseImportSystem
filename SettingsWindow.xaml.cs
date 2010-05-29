using System.Windows;
using System.Windows.Forms;
using meh = WCell.DatabaseImportSystem.Properties.Settings;
using MessageBox = System.Windows.MessageBox;
using MessageBoxOptions = System.Windows.Forms.MessageBoxOptions;

namespace WCell.DatabaseImportSystem
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow
    {
        public SettingsWindow()
        {
            InitializeComponent();
            TBoxUser.Text = meh.Default.MysqlUsername;
            TBoxPassword.Text = meh.Default.MysqlPassword;
            TBoxHost.Text = meh.Default.MysqlHost;
            TBoxDatabase.Text = meh.Default.MysqlDatabase;
            TBoxUDBDir.Text = meh.Default.UDBDir;
        }
        /// <summary>
        /// Save the values which the user has provided in the textboxes after checking for nulls.
        /// </summary>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(TBoxHost.Text) | string.IsNullOrEmpty(TBoxUser.Text) | string.IsNullOrEmpty(TBoxPassword.Text) | string.IsNullOrEmpty(TBoxDatabase.Text))
            {
                if (MessageBox.Show("One of the variables seems to be null, are you sure you want to continue?", "Are you certain?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
                {
                    return;
                }
            }
            meh.Default.MysqlHost = TBoxHost.Text;
            meh.Default.MysqlUsername = TBoxUser.Text;
            meh.Default.MysqlPassword = TBoxPassword.Text;
            meh.Default.MysqlDatabase = TBoxDatabase.Text;
            meh.Default.Save();
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Ask the user where the main UDB folder is located
        /// TODO: Add code to deal with the current folder so if the user points us @ a subfolder adapt to it etc.
        /// </summary>
        private void BtnBrowseUdbDirClick(object sender, RoutedEventArgs e)
        {
            using (var selectFolder = new FolderBrowserDialog())
                                        {
                                            selectFolder.ShowDialog(); 
                                            meh.Default.UDBDir = selectFolder.SelectedPath;
                                            TBoxUDBDir.Text = selectFolder.SelectedPath; 
                                        }
        }
    }
}
