﻿using System;
using System.Windows.Forms;
using System.Diagnostics;
using meh = WCell.DatabaseImportSystem.Properties.Settings;
using MySql.Data.MySqlClient;
namespace WCell.DatabaseImportSystem
{
    class MysqlHandler
    {
        public static Process Mysql;
        public static void ImportLargeSql(string filename)
        {
            try
            {
                var args = string.Format("/c mysql.exe --host={0} --user {1} --port 3306 {2} < {3}", meh.Default.MysqlHost, meh.Default.MysqlUsername, meh.Default.MysqlDatabase,filename);
                if(!string.IsNullOrEmpty(meh.Default.MysqlPassword))
                    args = string.Format("/c mysql.exe -host={0} --user {1} --password {2} --port 3306 {3} < {4}", meh.Default.MysqlHost, meh.Default.MysqlUsername, meh.Default.MysqlPassword, meh.Default.MysqlDatabase,filename);
                Mysql = new Process
                            {
                                StartInfo =
                                    {
                                        FileName = "cmd.exe",
                                        Arguments = args,
                                        RedirectStandardInput = false,
                                        RedirectStandardOutput = false,
                                        UseShellExecute = false,
                                        CreateNoWindow = true
                                    }
                                };
                Mysql.Start();
                MessageBox.Show(@"Importing Data, please wait...");
                Mysql.WaitForExit();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                LogWriter.WriteLine(ex.Data + ex.Message + ex.StackTrace);
            }
        }
        public static void Command(string command)
        {
            try
            {
                var connstring = new MySqlConnectionStringBuilder
                                     {
                                         Server = meh.Default.MysqlHost,
                                         UserID = meh.Default.MysqlUsername,
                                         Password = meh.Default.MysqlPassword,
                                         Database = meh.Default.MysqlDatabase
                                     };
                var connection = new MySqlConnection(connstring.ToString());
                LogWriter.WriteLine(@"opening connection to mysql");
                connection.Open();
                LogWriter.WriteLine(@"connection successful!");
                var sqlcommand = new MySqlCommand(command, connection);
                LogWriter.WriteLine(@"Running sql command");
                sqlcommand.ExecuteNonQuery();
                LogWriter.WriteLine(@"Sql Command finished.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"An Error occured while importing the file!, log output should have more info.");
                LogWriter.WriteLine(ex.Data + ex.StackTrace + ex.Message);
            }
        }
    }
}
