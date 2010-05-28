using System;
using System.IO;
using System.Windows;
using meh = WCellDatabaseImportSystem.Properties.Settings;

namespace WCellDatabaseImportSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            BtnRefreshDbListClick(null,null);
        }

        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().ShowDialog();
        }

        private void BtnApplyDbFileClick(object sender, RoutedEventArgs e)
        {
            MysqlHandler.Connect();
            var file = meh.Default.UDBDir + meh.Default.UDBMainDbFolderName + LBoxMainDBFiles.Items.CurrentItem;
            if(File.Exists(file))
            {
                var dbSql = new StreamReader(file);
                while (!dbSql.EndOfStream)
                {
                    var sql = dbSql.ReadLine();
                    MysqlHandler.Command(sql);
                }
                dbSql.Close();
                MessageBox.Show("Import should be finished!");
            }
            else
            {
                MessageBox.Show("The file selected could not be found, or there is a lack of permission to read it!");
            }
        }

        private void BtnRefreshDbListClick(object sender, RoutedEventArgs e)
        {
            try
            {
                LBoxMainDBFiles.Items.Clear();
                if (Directory.Exists(meh.Default.UDBDir + meh.Default.UDBMainDbFolderName))
                {
                    var udbDirDBs = new DirectoryInfo(meh.Default.UDBDir + meh.Default.UDBMainDbFolderName);
                        var dBfiles = udbDirDBs.GetFiles("*.sql", SearchOption.AllDirectories);
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
