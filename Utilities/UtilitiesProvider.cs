using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyClientCoreProject.Utilities
{
    public class UtilitiesProvider
    {
        private readonly IHostingEnvironment env;

        public UtilitiesProvider(IHostingEnvironment env) //Constructor
        {
            this.env = env;
        }

        #region Method (Get exception log file path)
        public string GetLogFilePath()
        {
            var filePath = Path.Combine(env.WebRootPath, "AllExceptions", "LogException.txt");
            return filePath;
        }
        #endregion
    }
}
