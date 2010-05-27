using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using meh = WCellDatabaseImportSystem.Properties.Settings;

namespace WCellDatabaseImportSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().ShowDialog();
        }

        private void BtnApplyDBFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRefreshDBList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LBoxMainDBFiles.Items.Clear();
                if (Directory.Exists(meh.Default.UDBDir + meh.Default.UDBMainDbFolderName))
                {
                    var udbDirDBs = new DirectoryInfo(meh.Default.UDBDir + meh.Default.UDBMainDbFolderName);
                        var dBfiles = udbDirDBs.GetFiles(".sql", SearchOption.TopDirectoryOnly);
                        foreach (var fileInfo in dBfiles)
                        {
                            LBoxMainDBFiles.Items.Add(fileInfo.Name);
                        }
                }
                else
                {
                    const string error = "Error UDB Main DB folder not found, please check settings or report this bug, if you are an advanced user edit the config directly (UDBMainDbFolderName variable) and please tell us!";
                    MessageBox.Show(error);
                }
            }
            catch(Exception ex)
            {
                ErrorWriter.Write(ex.Data + ex.Message + ex.StackTrace);
            }
        }
    }
}
