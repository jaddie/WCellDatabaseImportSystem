﻿using System;
using MySql.Data.MySqlClient;
using meh = WCellDatabaseImportSystem.Properties.Settings;
namespace WCellDatabaseImportSystem
{
    class MysqlHandler
    {
        static MySqlConnection Connect()
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
                var conn = new MySqlConnection(connstring.ToString());
                conn.Open();
                return conn;
            }
            catch(Exception ex)
            {
                ErrorWriter.Write(ex.Data + ex.Message + ex.StackTrace);
                return null;
            }
        }
        static bool Command(string command)
        {
            var sqlcommand = new MySqlCommand(command,Connect());
            var status = sqlcommand.ExecuteNonQuery();
            return status == 1;
        }
    }
}