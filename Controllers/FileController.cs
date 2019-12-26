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

namespace MyClientCoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseController
    {
        FilePresenter fp;
        public FileController(IHostingEnvironment hostingEnvironment, IFile file) : base(hostingEnvironment) //Constructor
        {
            fp =new FilePresenter(file,hostingEnvironment);
        }

        #region File (GET,POST,DELETE)

        #region GET
        [Route("files")]
        [HttpGet]
        public IActionResult GetFileNumbers()
        {
            try
            {
                return Ok(fp.GetFiles());
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region POST
        [Route("files")]
        [HttpPost]
        public IActionResult CreateNewFile(TblFile model)
        {
            try
            {
                return Ok(fp.CreateNewFileNo(model));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region DELETE
        [Route("files/{Id}")]
        [HttpDelete]
        public IActionResult DeleteFile(int Id)
        {
            try
            {
                return Ok(fp.DeleteFile(Id));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #endregion

        #region Sector (GET,POST,PUT,DELETE)

        #region GET
        [Route("sectors")]
        [HttpGet]
        public IActionResult GetSectors()
        {
            try
            {
                return Ok(fp.GetSectors());
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region POST
        [Route("sectors")]
        [HttpPost]
        public IActionResult CreateNewSector(TblSectors model)
        {
            try
            {
                return Ok(fp.CreateSector(model));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region PUT
        [Route("sectors")]
        [HttpPut]
        public IActionResult UpdateSector(TblSectors model)
        {
            try
            {
                return Ok(fp.UpdateSector(model));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region DELETE
        [Route("sectors/{Id}")]
        [HttpDelete]
        public IActionResult DeleteSector(int Id)
        {
            try
            {
                return Ok(fp.DeleteSector(Id));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #endregion

        #region House Address (GET,POST,PUT)

        #region GET
        [Route("houseno/{id?}")]
        [HttpGet]
        public IActionResult GetHouseAddress(int id = 0)
        {
            try
            {
                return Ok(fp.GetHouseAddress(id));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region POST
        [Route("houseno")]
        [HttpPost]
        public IActionResult CreateHouseAddress(TblHouseAddress model)
        {
            try
            {
                return Ok(fp.CreateHouseAddress(model));
            }
            catch (Exception ex)
            {
                Exceptions(ex);
                return StatusCode(500);
            }
        }
        #endregion

        #region PUT
        [Route("houseno")]
        [HttpPut]
        public IActionResult UpdateHouseAddress(HouseAddressViewModel model)
        {
            try
            {
                return Ok(fp.UpdateHouseAddress(model));
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