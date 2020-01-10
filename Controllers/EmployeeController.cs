using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.Presenter;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.Utilities;
using MyClientCoreProject.ViewModel;

namespace MyClientCoreProject.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICustomLogger _logger;
        private readonly RouteData route;
        EmployeePresenter ep;

        public EmployeeController(IHostingEnvironment hostingEnvironment, IEmployee employee, ICustomLogger logger)//Constructor
        {
            _logger = logger;
            route = ControllerContext.RouteData;
            ep = new EmployeePresenter(employee, hostingEnvironment);
        }

        #region Employee (GET,POST,PUT,DELETE)

        #region GET
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("employee")]
        public IActionResult GetEmployees()
        {
            try
            {
                return Ok(ep.GetEmployees());
            }
            catch (Exception ex)
            {
                _logger.Exceptions(ex, route.Values["controller"].ToString(), route.Values["action"].ToString());
                return StatusCode(500);
            }
        }
        #endregion

        #region POST
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("employee")]
        public IActionResult CreateEmployee(TblEmployee model)
        {
            try
            {
                return Ok(ep.CreateNewEmployee(model));
            }
            catch (Exception ex)
            {
                _logger.Exceptions(ex, route.Values["controller"].ToString(), route.Values["action"].ToString());
                return StatusCode(500);
            }
        }
        #endregion

        #region PUT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("employee")]
        public IActionResult UpdateEmployee(EmployeeRoleViewModel model)
        {
            try
            {
                return Ok(ep.UpdateEmployee(model));
            }
            catch (Exception ex)
            {
                _logger.Exceptions(ex, route.Values["controller"].ToString(), route.Values["action"].ToString());
                return StatusCode(500);
            }
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("employee/{Id}")]
        public IActionResult DeleteEmployee(int Id)
        {
            try
            {
                return Ok(ep.DeleteEmployee(Id));
            }
            catch (Exception ex)
            {
                _logger.Exceptions(ex, route.Values["controller"].ToString(), route.Values["action"].ToString());
                return StatusCode(500);
            }
        }
        #endregion

        #endregion

        #region Roles (GET,POST,DELETE)

        #region GET
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            try
            {
                return Ok(ep.GetRoles());
            }
            catch (Exception ex)
            {
                _logger.Exceptions(ex, route.Values["controller"].ToString(), route.Values["action"].ToString());
                return StatusCode(500);
            }
        }
        #endregion

        #region POST
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("roles")]
        public IActionResult CreateRole(TblEmployeeRole model)
        {
            try
            {
                return Ok(ep.CreateNewRole(model));
            }
            catch (Exception ex)
            {
                _logger.Exceptions(ex, route.Values["controller"].ToString(), route.Values["action"].ToString());
                return StatusCode(500);
            }
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("roles/{Id}")]
        public IActionResult DeleteRole(int Id)
        {
            try
            {
                return Ok(ep.DeleteRole(Id));
            }
            catch (Exception ex)
            {
                _logger.Exceptions(ex, route.Values["controller"].ToString(), route.Values["action"].ToString());
                return StatusCode(500);
            }
        }
        #endregion

        #endregion

    }
}