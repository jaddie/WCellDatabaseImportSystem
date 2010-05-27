using System.Windows;
using System.Windows.Forms;
using meh = WCellDatabaseImportSystem.Properties.Settings;
using MessageBox = System.Windows.MessageBox;

namespace WCellDatabaseImportSystem
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
            if(TBoxHost.Text != null | TBoxUser.Text != null | TBoxPassword.Text != null | TBoxDatabase.Text != null)
            {
                MessageBox.Show("One of the values has not been filled in, please check it.");
                return;
            }
            meh.Default.MysqlHost = TBoxHost.Text;
            meh.Default.MysqlUsername = TBoxUser.Text;
            meh.Default.MysqlPassword = TBoxPassword.Text;
            meh.Default.MysqlDatabase = TBoxUser.Text;
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
