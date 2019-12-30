using System;
using System.Linq;
using System.Collections.Generic;
using MyClientCoreProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyClientCoreProject.Repository.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using MyClientCoreProject.Utilities;
using MyClientCoreProject.Helper;
using MyClientCoreProject.ViewModel;
using Microsoft.EntityFrameworkCore;
using MyClientCoreProject.Models.DB;

namespace MyClientCoreProject.Repository.SqlRepository
{
    public class FileRepository : IFile
    {
        private readonly SiddiquiAssociateDBContext dbContext;
        private readonly IHostingEnvironment env;
        UtilitiesProvider up;

        public FileRepository(SiddiquiAssociateDBContext dbContext, IHostingEnvironment env) //Constructor
        {
            this.dbContext = dbContext;
            this.env = env;
            up = new UtilitiesProvider(env);
        }

        #region File (GET,POST,DELETE)

        #region GET
        public IList<SelectListItem> GetFiles()
        {
            try
            {
                return dbContext.TblFile.Select(x => new SelectListItem
                {
                    Text = x.FileNo,
                    Value = x.Id.ToString()
                }).OrderBy(x => x.Text).ToList();
            }
            catch(Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(FileRepository)} - Method name:  {nameof(GetFiles)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateNewFileNo(TblFile model)
        {
            try
            {
                dbContext.Add(model);
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(EmployeeRepository)} - Method name:  {nameof(CreateNewFileNo)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region DELETE
        public TblFile DeleteFile(int Id)
        {
            try
            {
                var FileId = dbContext.TblFile.Find(Id);
                if (FileId != null)
                {
                    dbContext.TblFile.Remove(FileId);
                    dbContext.SaveChanges();
                }
                return FileId;
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(EmployeeRepository)} - Method name:  {nameof(DeleteFile)}", stackTrace: ex.StackTrace);
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
                return dbContext.TblSectors.Select(x => new SelectListItem
                {
                    Text = x.SectorNo,
                    Value = x.Id.ToString()
                }).OrderBy(x => x.Text).ToList();
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(FileRepository)} - Method name:  {nameof(GetSectors)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateNewSector(TblSectors model)
        {
            try
            {
                var count = dbContext.TblSectors.Where(x => x.SectorNo == model.SectorNo).FirstOrDefault();
                if(count == null)
                {
                    dbContext.Add(model);
                    return dbContext.SaveChanges();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(FileRepository)} - Method name:  {nameof(CreateNewSector)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region PUT
        public int UpdateSector(TblSectors model)
        {
            try
            {
                var recordToUpdateInDb = dbContext.TblSectors.Where(x => x.Id == model.Id).FirstOrDefault();
                if (recordToUpdateInDb != null)
                {
                    var duplicateNameRecord = dbContext.TblSectors.Where(x => x.Id != model.Id && x.SectorNo == model.SectorNo).FirstOrDefault();

                    if (duplicateNameRecord != null)
                    {
                        return 0;
                    }
                    else
                    {
                        recordToUpdateInDb.SectorNo = model.SectorNo;
                        return dbContext.SaveChanges();                      
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(FileRepository)} - Method name:  {nameof(UpdateSector)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region DELETE
        public TblSectors DeleteSector(int Id)
        {
            try
            {
                var SectorId = dbContext.TblSectors.Where(x => x.Id == Id).FirstOrDefault();
                if(SectorId != null)
                {
                    var houseId = dbContext.TblHouseAddress.Where(x => x.SectorId == SectorId.Id).FirstOrDefault();
                    dbContext.TblHouseAddress.RemoveRange(houseId);
                    dbContext.TblSectors.RemoveRange(SectorId);
                    dbContext.SaveChanges();
                }
                return SectorId;
            }
            catch(Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(FileRepository)} - Method name:  {nameof(DeleteSector)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #endregion

        #region House Address (GET,POST,PUT)

        #region GET
        public IList<HouseAddressViewModel> GetHouseAddress(int SectorId)
        {
            try
            {
                var houseAddressList = dbContext.HouseAddressViewModel.FromSql($"sp_GetAllHouseAddress {SectorId}").ToList();
                return houseAddressList;
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(FileRepository)} - Method name:  {nameof(GetHouseAddress)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateHouseAddress(TblHouseAddress model)
        {
            try
            {
                var IsHouseNoExists = dbContext.TblHouseAddress.Where(x => x.HouseNo == model.HouseNo && x.SectorId == model.SectorId).FirstOrDefault();
                if (IsHouseNoExists != null)
                    return 0;
                else
                {
                    dbContext.Add(model);
                    return dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(FileRepository)} - Method name:  {nameof(CreateHouseAddress)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region PUT
        public int UpdateHouseAddress(HouseAddressViewModel model)
        {
            try
            {
                var recordToUpdateInDb = dbContext.TblHouseAddress.Where(x => x.Id == model.HouseId).FirstOrDefault();
                if (recordToUpdateInDb != null)
                {
                    var duplicateNameRecord = dbContext.TblHouseAddress.Where(x => x.Id != model.HouseId &&
                                                                              x.HouseNo == model.HouseNo &&
                                                                              x.SectorId == model.SectorID ).FirstOrDefault();

                    if (duplicateNameRecord != null)
                        return 0;
                    else
                    {
                        recordToUpdateInDb.HouseNo = model.HouseNo;
                        recordToUpdateInDb.SectorId = model.SectorID;
                        return dbContext.SaveChanges();
                    }
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(FileRepository)} - Method name:  {nameof(UpdateHouseAddress)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #endregion
    }
}
