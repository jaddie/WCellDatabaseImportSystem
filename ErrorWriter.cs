using System.IO;

namespace WCellDatabaseImportSystem
{
    class ErrorWriter
    {
        public static void Write(string value)
        {
            var errorLog = new StreamWriter("ErrorLog.txt",true) {AutoFlush = true};
            errorLog.WriteLine(value);
            errorLog.Close();
        }
    }
}
