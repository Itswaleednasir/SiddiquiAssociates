using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyClientCoreProject.Models;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.Presenter;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.ViewModel;

namespace MyClientCoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : BaseController
    {
        ReceiptPresenter rp;
        public ReceiptController(IHostingEnvironment hostingEnvironment, IReceipt receipt) : base(hostingEnvironment) // Constructor
        {
            rp = new ReceiptPresenter(receipt,hostingEnvironment);
        }


        #region Messrs (GET,POST,PUT,DELETE)

        #region GET
        [Route("messr")]
        [HttpGet]
        public IActionResult GetMessrs()
        {
            try
            {
                return Ok(rp.GetMessrs());
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region POST
        [Route("messr")]
        [HttpPost]
        public IActionResult CreateMessr(TblMessrs model)
        {
            try
            {
                return Ok(rp.CreateNewMessr(model));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region PUT
        [Route("messr")]
        [HttpPut]
        public IActionResult UpdateMessrs(TblMessrs model)
        {
            try
            {
                return Ok(rp.UpdateMessr(model));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region DELETE
        [Route("messr/{Id}")]
        [HttpDelete]
        public IActionResult DeleteMessr(int Id)
        {
            try
            {
                return Ok(rp.DeleteMessr(Id));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #endregion

        #region Receipt (GET,POST,PUT,DELETE)

        #region GET
        [Route("receipt/{id}")]
        [HttpGet]
        public IActionResult GetReceipt(int id)
        {
            try
            {
                return Ok(rp.GetReceipt(id));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region POST
        [Route("receipt")]
        [HttpPost]
        public IActionResult PostReceipt(ReceiptPaymentViewModel model)
        {           
            try
            {
                return Ok(rp.CreateNewReceipt(model));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region PUT
        [Route("receipt")]
        [HttpPut]
        public IActionResult UpdateReceipt(ReceiptPaymentViewModel model)
        {
            try
            {
                return Ok(rp.UpdateReceipt(model));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
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
        //        Exceptions(ex);
        //        return StatusCode(500);
        //    }
        //}
        #endregion

        #endregion

        #region Register Messr (GET,POST,PUT,DELETE)

        #region GET
        [HttpGet]
        [Route("registermessr")]
        public IActionResult GetRegisterMessrs()
        {
            try
            {
                return Ok(rp.GetRegisterMessr());
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        [Route("registermessr")]
        public IActionResult PostRegisterMessrs(TblRegisterMessrs model)
        {
            try
            {
                return Ok(rp.CreateNewRegisterMessr(model));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region PUT
        [HttpPut]
        [Route("registermessr")]
        public IActionResult PutRegisterMessrs(TblRegisterMessrs model)
        {
            try
            {
                return Ok(rp.UpdateRegisterMessr(model));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("registermessr/{id}")]
        public IActionResult DeleteRegisterMessrs(int id)
        {
            try
            {
                return Ok(rp.DeleteRegisterMessr(id));
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