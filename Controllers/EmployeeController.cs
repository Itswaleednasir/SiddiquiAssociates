using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.Presenter;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.ViewModel;

namespace MyClientCoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        EmployeePresenter ep;

        public EmployeeController(IHostingEnvironment hostingEnvironment, IEmployee employee) :base(hostingEnvironment) //Constructor
        {
            ep = new EmployeePresenter(employee,hostingEnvironment);
        }

        #region Employee (GET,POST,PUT,DELETE)

        #region GET
        [Route("employee")]
        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {
                return Ok(ep.GetEmployees());
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region POST
        [Route("employee")]
        [HttpPost]
        public IActionResult CreateEmployee(TblEmployee model)
        {
            try
            {
                return Ok(ep.CreateNewEmployee(model));
            }
            catch(Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region PUT
        [Route("employee")]
        [HttpPut]
        public IActionResult UpdateEmployee(EmployeeRoleViewModel model)
        {
            try
            {
                return Ok(ep.UpdateEmployee(model));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region DELETE
        [Route("employee/{Id}")]
        [HttpDelete]
        public IActionResult DeleteEmployee(int Id)
        {
            try
            {
                return Ok(ep.DeleteEmployee(Id));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #endregion

        #region Roles (GET,POST,DELETE)

        #region GET
        [Route("roles")]
        [HttpGet]
        public IActionResult GetRoles()
        {
            try
            {
                return Ok(ep.GetRoles());
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region POST
        [Route("roles")]
        [HttpPost]
        public IActionResult CreateRole(TblEmployeeRole model)
        {
            try
            {
                return Ok(ep.CreateNewRole(model));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region DELETE
        [Route("roles/{Id}")]
        [HttpDelete]
        public IActionResult DeleteRole(int Id)
        {
            try
            {
                return Ok(ep.DeleteRole(Id));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #endregion

    }
}