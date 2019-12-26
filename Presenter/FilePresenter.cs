using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyClientCoreProject.Helper;
using MyClientCoreProject.Models;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.Utilities;
using MyClientCoreProject.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyClientCoreProject.Presenter
{
    public class FilePresenter
    {
        private readonly IFile file;
        private readonly IHostingEnvironment env;
        UtilitiesProvider up;

        public FilePresenter(IFile file, IHostingEnvironment env) //Constructor
        {
            this.file = file;
            this.env = env;
            up = new UtilitiesProvider(env);
        }

        #region File (GET,POST,DELETE)

        #region GET
        public IList<SelectListItem> GetFiles()
        {
            try
            {
                return file.GetFiles();
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(FilePresenter)} - Method name:  {nameof(GetFiles)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateNewFileNo(TblFile model)
        {
            try
            {
                return file.CreateNewFileNo(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(FilePresenter)} - Method name:  {nameof(CreateNewFileNo)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region DELETE
        public TblFile DeleteFile(int Id)
        {
            try
            {
                return file.DeleteFile(Id);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(FilePresenter)} - Method name:  {nameof(DeleteFile)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #endregion

        #region Sector (GET,POST,PUT,DELETE)

        #region GET
        public IList<SelectListItem> GetSectors()
        {
            try
            {
                var sectors = file.GetSectors();
                return sectors;
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(FilePresenter)} - Method name:  {nameof(GetSectors)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateSector(TblSectors model)
        {
            try
            {
                return file.CreateNewSector(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(FilePresenter)} - Method name:  {nameof(CreateSector)}", stackTrace: ex.StackTrace);

                return 0;
            }
        }
        #endregion

        #region PUT
        public int UpdateSector(TblSectors model)
        {
            try
            {
                return file.UpdateSector(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(FilePresenter)} - Method name:  {nameof(UpdateSector)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region DELETE
        public TblSectors DeleteSector(int Id)
        {
            try
            {
                return file.DeleteSector(Id);
            }
            catch(Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(FilePresenter)} - Method name:  {nameof(DeleteSector)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #endregion

        #region House Address (GET,POST,PUT)

        #region GET
        public IList<HouseAddressViewModel> GetHouseAddress(int id)
        {
            try
            {
                return file.GetHouseAddress(id);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(FilePresenter)} - Method name:  {nameof(GetHouseAddress)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateHouseAddress(TblHouseAddress model)
        {
            try
            {
                return file.CreateHouseAddress(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(FilePresenter)} - Method name:  {nameof(CreateHouseAddress)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region PUT
        public int UpdateHouseAddress(HouseAddressViewModel model)
        {
            try
            {
                return file.UpdateHouseAddress(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(FilePresenter)} - Method name:  {nameof(UpdateHouseAddress)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #endregion

    }
}
