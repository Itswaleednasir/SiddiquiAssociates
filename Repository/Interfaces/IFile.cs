using Microsoft.AspNetCore.Mvc.Rendering;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyClientCoreProject.Repository.Interfaces
{
    public interface IFile
    {
        #region File (GET,POST,DELETE)

        #region GET
        IList<SelectListItem> GetFiles();
        #endregion

        #region POST
        int CreateNewFileNo(TblFile model);
        #endregion

        #region DELETE
        TblFile DeleteFile(int Id);
        #endregion

        #endregion

        #region Sector (GET,POST,PUT,DELETE)

        #region GET
        IList<SelectListItem> GetSectors();
        #endregion

        #region POST
        int CreateNewSector(TblSectors model);
        #endregion

        #region PUT
        int UpdateSector(TblSectors model);
        #endregion

        #region DELETE
        TblSectors DeleteSector(int Id);
        #endregion

        #endregion

        #region House Address (GET,POST,PUT)

        #region GET
        IList<HouseAddressViewModel> GetHouseAddress(int id);
        #endregion

        #region POST
        int CreateHouseAddress(TblHouseAddress model);
        #endregion

        #region PUT
        int UpdateHouseAddress(HouseAddressViewModel model);
        #endregion

        #endregion
    }
}
