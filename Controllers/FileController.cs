using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using MyClientCoreProject.Models;
using MyClientCoreProject.Presenter;
using MyClientCoreProject.ViewModel;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.Utilities;
using Microsoft.AspNetCore.Routing;

namespace MyClientCoreProject.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ICustomLogger _logger;
        private readonly RouteData route;
        FilePresenter fp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="file"></param>
        /// <param name="logger"></param>
        public FileController(IHostingEnvironment hostingEnvironment, IFile file,ICustomLogger logger)//Constructor
        {
            _logger = logger;
            fp = new FilePresenter(file,hostingEnvironment);
            route = ControllerContext.RouteData;
        }

        #region File (GET,POST,DELETE)


        #region GET
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet("files")]
        public IActionResult GetFileNumbers()
        {
            try
            {
                return Ok(fp.GetFiles());
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
        [HttpPost("files")]
        public IActionResult CreateNewFile(TblFile model)
        {
            try
            {
                return Ok(fp.CreateNewFileNo(model));
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
        [HttpDelete("files/{id}")]
        public IActionResult DeleteFile(int Id)
        {
            try
            {
                return Ok(fp.DeleteFile(Id));
            }
            catch (Exception ex)
            {
                _logger.Exceptions(ex, route.Values["controller"].ToString(), route.Values["action"].ToString());
                return StatusCode(500);
            }
        }
        #endregion

        #endregion

        #region Sector (GET,POST,PUT,DELETE)

        #region GET
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("sectors")]
        public IActionResult GetSectors()
        {
            try
            {
                return Ok(fp.GetSectors());
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
        [HttpPost("sectors")]
        public IActionResult CreateNewSector(TblSectors model)
        {
            try
            {
                return Ok(fp.CreateSector(model));
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
        [HttpPut("sectors")]
        public IActionResult UpdateSector(TblSectors model)
        {
            try
            {
                return Ok(fp.UpdateSector(model));
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
        [HttpDelete("sectors/{id}")]
        public IActionResult DeleteSector(int Id)
        {
            try
            {
                return Ok(fp.DeleteSector(Id));
            }
            catch (Exception ex)
            {
                _logger.Exceptions(ex, route.Values["controller"].ToString(), route.Values["action"].ToString());
                return StatusCode(500);
            }
        }
        #endregion

        #endregion

        #region House Address (GET,POST,PUT)

        #region GET
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("houseno/{id?}")]
        public IActionResult GetHouseAddress(int id = 0)
        {
            try
            {
                return Ok(fp.GetHouseAddress(id));
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
        [HttpPost("houseno")]
        public IActionResult CreateHouseAddress(TblHouseAddress model)
        {
            try
            {
                return Ok(fp.CreateHouseAddress(model));
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
        [HttpPut("houseno")]
        public IActionResult UpdateHouseAddress(HouseAddressViewModel model)
        {
            try
            {
                return Ok(fp.UpdateHouseAddress(model));
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