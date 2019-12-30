using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MyClientCoreProject.Models;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICustomLogger _logger;
        private readonly RouteData route;
        ReceiptPresenter rp;
        public ReceiptController(IHostingEnvironment hostingEnvironment, IReceipt receipt, ICustomLogger logger)//Constructor
        {
            _logger = logger;
            route = ControllerContext.RouteData;
            rp = new ReceiptPresenter(receipt,hostingEnvironment);
        }


        #region Messrs (GET,POST,PUT,DELETE)

        #region GET
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("messr")]
        public IActionResult GetMessrs()
       {
            try
            {
                return Ok(rp.GetMessrs());
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
        [HttpPost("messr")]
        public IActionResult CreateMessr(TblMessrs model)
        {
            try
            {
                return Ok(rp.CreateNewMessr(model));
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
        [HttpPut("messr")]
        public IActionResult UpdateMessrs(TblMessrs model)
        {
            try
            {
                return Ok(rp.UpdateMessr(model));
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
        [HttpDelete("messr/{Id}")]
        public IActionResult DeleteMessr(int Id)
        {
            try
            {
                return Ok(rp.DeleteMessr(Id));
            }
            catch (Exception ex)
            {
                _logger.Exceptions(ex, route.Values["controller"].ToString(), route.Values["action"].ToString());
                return StatusCode(500);
            }
        }
        #endregion

        #endregion

        #region Receipt (GET,POST,PUT,DELETE)

        #region GET
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("receipt/{id}")]
        public IActionResult GetReceipt(int id)
        {
            try
            {
                return Ok(rp.GetReceipt(id));
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
        [HttpPost("receipt")]
        public IActionResult PostReceipt(ReceiptPaymentViewModel model)
        {           
            try
            {
                return Ok(rp.CreateNewReceipt(model));
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
        [HttpPut("receipt")]
        public IActionResult UpdateReceipt(ReceiptPaymentViewModel model)
        {
            try
            {
                return Ok(rp.UpdateReceipt(model));
            }
            catch (Exception ex)
            {
                _logger.Exceptions(ex, route.Values["controller"].ToString(), route.Values["action"].ToString());
                return StatusCode(500);
            }
        }
        #endregion

        #region DELETE
        //[Route("receipt")]
        //[HttpDelete]
        //public IActionResult DeleteReceipt()
        //{
        //    try
        //    {
        //        return rp.DeleteReceipt();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Exceptions(ex, route.Values["controller"].ToString(), route.Values["action"].ToString());
        //        return StatusCode(500);
        //    }
        //}
        #endregion

        #endregion

        #region Register Messr (GET,POST,PUT,DELETE)

        #region GET
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ///         
        [HttpGet("registermessr")]
        public IActionResult GetRegisterMessrs()
        {
            try
            {
                return Ok(rp.GetRegisterMessr());
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
        [HttpPost("registermessr")]
        public IActionResult PostRegisterMessrs(TblRegisterMessrs model)
        {
            try
            {
                return Ok(rp.CreateNewRegisterMessr(model));
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
        [HttpPut("registermessr")]
        public IActionResult PutRegisterMessrs(TblRegisterMessrs model)
        {
            try
            {
                return Ok(rp.UpdateRegisterMessr(model));
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("registermessr/{id}")]        
        public IActionResult DeleteRegisterMessrs(int id)
        {
            try
            {
                return Ok(rp.DeleteRegisterMessr(id));
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