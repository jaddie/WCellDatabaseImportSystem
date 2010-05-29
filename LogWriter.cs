using System.IO;

namespace WCellDatabaseImportSystem
{
    class LogWriter
    {
        public static void WriteLine(string value)
        {
            var errorLog = new StreamWriter("ErrorLog.txt",true) {AutoFlush = true};
            errorLog.WriteLine(value);
            errorLog.Close();
        }
    }
}
