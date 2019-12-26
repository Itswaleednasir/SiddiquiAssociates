using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyClientCoreProject.Helper
{
    public static class StaticHelper
    {

        public static void LogException(string path,string errorMessage, string methodName, string stackTrace)
        {
            using (var streamWriter = File.AppendText(path))
            {
                var currentDateTime = DateTime.Now;
                streamWriter.Write("\n \r Log Entry : ");
                streamWriter.WriteLine("{0} {1}", currentDateTime.ToLongTimeString(), currentDateTime.ToLongDateString() + ": ");
                streamWriter.WriteLine(methodName);
                streamWriter.WriteLine(errorMessage);
                streamWriter.WriteLine("STACK TRACE: ");
                streamWriter.WriteLine(stackTrace);
                streamWriter.WriteLine("<<------------------------------->>");
            }
        }
    }
}
