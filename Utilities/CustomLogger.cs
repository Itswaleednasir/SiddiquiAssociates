using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using MyClientCoreProject.Helper;

namespace MyClientCoreProject.Utilities
{
    public class CustomLogger : ICustomLogger
    {
        protected readonly IHostingEnvironment _hostingEnvironment;

        public CustomLogger(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void Exceptions(Exception ex, string controllerName, string actionName)
        {
            LogException(ex, controllerName, actionName);

            if (ex.InnerException != null)
            {
                LogException(ex.InnerException, controllerName, actionName);
            }
        }

        public void LogException(Exception ex, string controllerName, string actionName)
        {
            var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "AllExceptions", "LogException.txt");

            StaticHelper.LogException(
                path: folderPath,
                errorMessage: ex.Message,
                $"Controller Name: {controllerName} - Action Name: {actionName}",
                stackTrace: ex.StackTrace);
        }
    }
}