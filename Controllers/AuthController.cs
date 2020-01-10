using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.Presenter;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.Utilities;

namespace MyClientCoreProject.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ICustomLogger _logger;
        private readonly RouteData route;
        private readonly AuthPresenter _ap;

        //Constructor
        public AuthController(IEmployee employee, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _ap = new AuthPresenter(employee, hostingEnvironment, configuration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("token")]
        public async Task<object> AuthToken(TblEmployee model)
        {
            try
            {
                var response = _ap.GetAuthToken(model.UserName, model.Password);
                return new { token = response };
            }
            catch (Exception ex)
            {
                _logger.Exceptions(ex, route.Values["controller"].ToString(), route.Values["action"].ToString());
                return null;
            }
        }
    }
}