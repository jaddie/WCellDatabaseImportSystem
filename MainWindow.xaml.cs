using System;
using System.IO;
using System.Windows;
using meh = WCellDatabaseImportSystem.Properties.Settings;
using System.Collections.Generic;
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

        private void WriteLine(string line)
        {
            TBoxOutput.Text = TBoxOutput.Text + "\n" + line;
        }

        private void BtnApplyDbFileClick(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteLine("Starting Import of" + LBoxDBUpdateFiles.SelectedValue);
                var file = new DirectoryInfo(meh.Default.UDBDir + meh.Default.UDBMainDbFolderName).GetFiles(LBoxMainDBFiles.SelectedValue.ToString(), SearchOption.AllDirectories);
                if (file.Length >= 1 && File.Exists(file[0].FullName))
                {
                    WriteLine(file[0].Name + "Exists, starting import, this may take a little while so please give the program time even if it appears to be unresponsive!");
                    MysqlHandler.ImportLargeSql(file[0].FullName);
                    WriteLine("Import should be complete");
                    MessageBox.Show("Import should be finished, however please check the database!");
                }
                else
                {
                    LogWriter.WriteLine(
                        "The file selected could not be found, or there is a lack of permission to read it!");
                    MessageBox.Show("The file selected could not be found, or there is a lack of permission to read it!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "An error occured while attempting to interact with the chosen file, make sure it is properly selected and try again!");
                LogWriter.WriteLine(ex.Data + ex.Message + ex.StackTrace);
            }
        }

        private void BtnRefreshDbListClick(object sender, RoutedEventArgs e)
        {
            try
            {
                LBoxMainDBFiles.Items.Clear();
                LBoxDBUpdateFiles.Items.Clear();
                if (Directory.Exists(meh.Default.UDBDir + meh.Default.UDBMainDbFolderName))
                {
                        foreach (var fileInfo in new DirectoryInfo(meh.Default.UDBDir + meh.Default.UDBMainDbFolderName).GetFiles("*.sql", SearchOption.AllDirectories))
                        {
                            LBoxMainDBFiles.Items.Add(fileInfo.Name);
                        }
                }
                else
                {
                    const string error = "Error UDB Main DB folder not found, please check settings or report this bug, if you are an advanced user edit the config directly (UDBMainDbFolderName variable) and please tell us!";
                    MessageBox.Show(error);
                    LogWriter.WriteLine(error);
                }
                if(Directory.Exists(meh.Default.UDBDir + meh.Default.UDBUpdatesFolder))
                {
                    foreach (var fileInfo in new DirectoryInfo(meh.Default.UDBDir + meh.Default.UDBUpdatesFolder).GetFiles("*.sql", SearchOption.AllDirectories))
                    {
                        LBoxDBUpdateFiles.Items.Add(fileInfo.Name);
                    }
                }
                else
                {
                    LBoxDBUpdateFiles.Items.Add("Updates folder does not exist!");
                }
            }
            catch(Exception ex)
            {
                LogWriter.WriteLine(ex.Data + ex.Message + ex.StackTrace);
            }
        }

        private void BtnApplyUpdates_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(meh.Default.UDBDir + meh.Default.UDBUpdatesFolder))
            {
                foreach (var update in LBoxDBUpdateFiles.Items)
                {
                    WriteLine(string.Format("Attempting to apply update {0}", update));
                    foreach (var fileInfo in new DirectoryInfo(meh.Default.UDBDir + meh.Default.UDBUpdatesFolder).GetFiles(update.ToString(), SearchOption.AllDirectories))
                    {
                        var sqlfile = new StreamReader(fileInfo.FullName);
                        MysqlHandler.Command(sqlfile.ReadToEnd());
                        WriteLine(string.Format("Update file {0} Should have been applied now", update));
                    }
                }
            }
            else
            {
                MessageBox.Show("Failed to access files, check permissions?");
            }
        }

        private void BtnRemoveUpdate_Click(object sender, RoutedEventArgs e)
        {
            LBoxDBUpdateFiles.Items.Remove(LBoxDBUpdateFiles.SelectedItem);
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
