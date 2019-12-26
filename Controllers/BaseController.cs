using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using MyClientCoreProject.Helper;

namespace MyClientCoreProject.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IHostingEnvironment _hostingEnvironment;

        public BaseController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void Exceptions(Exception ex)
        {
            var routeData = this.ControllerContext.RouteData;
            LogException(ex, routeData);
            if (ex.InnerException != null)
            {
                LogException(ex.InnerException, routeData);
            }
        }

        protected void LogException(Exception ex, RouteData route)
        {
            var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "AllExceptions", "LogException.txt");

            string controller = route.Values["controller"].ToString();
            string action = route.Values["action"].ToString();

            StaticHelper.LogException(path: folderPath, errorMessage: ex.Message, $"Controller Name: {controller} - Action Name: {action}", stackTrace: ex.StackTrace);
        }
    }
}