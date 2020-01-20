using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MyClientCoreProject.Helper;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.Utilities;
using MyClientCoreProject.ViewModel;

namespace MyClientCoreProject.Repository.SqlRepository
{
    public class ReceiptRepository : IReceipt
    {
        private readonly DocumentManagement_WContext dbContext;
        private readonly IHostingEnvironment env;
        UtilitiesProvider up;

        public ReceiptRepository(DocumentManagement_WContext dbContext, IHostingEnvironment env) //Constructor
        {
            this.dbContext = dbContext;
            this.env = env;
            up = new UtilitiesProvider(env);
        }

        #region Messrs (GET,POST,PUT,DELETE)

        #region GET
        public IList<TblMessrs> GetMessrs()
        {
            try
            {
                return dbContext.TblMessrs.Select(x => x).ToList();
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(ReceiptRepository)} - Method name:  {nameof(GetMessrs)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateMessr(TblMessrs model)
        {
            try
            {
                var IsMessrExists = dbContext.TblMessrs.Where(x => x.PhoneNo == model.PhoneNo).FirstOrDefault();
                if (IsMessrExists != null)
                    return 0;
                else
                {
                    dbContext.Add(model);
                    return dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(ReceiptRepository)} - Method name:  {nameof(CreateMessr)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region PUT
        public int UpdateMessr(TblMessrs model)
        {
            try
            {
                var recordToUpdateInDb = dbContext.TblMessrs.Where(x => x.Id == model.Id).FirstOrDefault();
                if (recordToUpdateInDb != null)
                {
                    var duplicateNameRecord = dbContext.TblMessrs.Where(x => x.Id != model.Id &&
                                                                            (x.PhoneNo == model.PhoneNo)).FirstOrDefault();

                    if (duplicateNameRecord != null)
                    {
                        return 0;
                    }
                    else
                    {
                        recordToUpdateInDb.Name = model.Name;
                        recordToUpdateInDb.PhoneNo = model.PhoneNo;
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
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(ReceiptRepository)} - Method name:  {nameof(UpdateMessr)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region DELETE
        public TblMessrs DeleteMessr(int Id)
        {
            try
            {
                var IsMessrExists = dbContext.TblMessrs.Find(Id);
                if (IsMessrExists != null)
                {
                    dbContext.TblMessrs.Remove(IsMessrExists);
                    dbContext.SaveChanges();
                }
                return IsMessrExists;
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(ReceiptRepository)} - Method name:  {nameof(DeleteMessr)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #endregion

        #region Receipt (GET,POST,PUT,DELETE)

        #region GET
        public IList<GetReceiptViewModel> GetReceipt(int houseId)
        {
            try
            {
                var isReceiptExists = dbContext.TblReceipt.Where(x => x.HouseId == houseId).FirstOrDefault();

                if (isReceiptExists == null)
                    return null;
                else
                {
                    var receiptList = dbContext.GetReceiptViewModel.FromSql($"sp_GetReceipt {houseId}").ToList();
                    return receiptList;
                }
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(ReceiptRepository)} - Method name:  {nameof(GetReceipt)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateNewReceipt(ReceiptPaymentViewModel model)
        {
            try
            {
                var IsReceiptExists = dbContext.TblReceipt.Where(x => x.Sno == model.Sno).FirstOrDefault();
                if (IsReceiptExists != null)
                    return 0;
                else

                {
                    int receiptCount = dbContext.Database.ExecuteSqlCommand($"sp_CreateReceipt {model.Sno},{model.FileId},{model.HouseId},{model.MessrsId},{model.ReceiptDate},{model.CaseTotalCost},{model.Amount},{model.Description}");
                    return receiptCount;
                }
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(ReceiptRepository)} - Method name:  {nameof(CreateNewReceipt)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region PUT
        public int UpdateReceipt(ReceiptPaymentViewModel model)
        {
            try
            {
                var IsReceiptExists = dbContext.TblReceipt.Where(x => x.HouseId == model.HouseId).FirstOrDefault();
                if (IsReceiptExists == null)
                    return 0;
                else
                {
                    int updateReceiptCount = 0;
                    if (model.Amount != 0 && model.PaymentDate != "")
                        updateReceiptCount = dbContext.Database.ExecuteSqlCommand($"sp_UpdateReceipt @HouseId = {model.HouseId},@NewPayment = {model.Amount},@PaymentDate = {model.PaymentDate},@ParamUpdate = Payment ");
                    else if (model.RegisterMessrsId != null && model.RegisterAmount != null && model.RegisterDate != "")
                        updateReceiptCount = dbContext.Database.ExecuteSqlCommand($"sp_UpdateReceipt @HouseId = {model.HouseId},@ParamUpdate = Register,@RegisterMessrsID = {model.RegisterMessrsId},@RegisterDate = {model.RegisterDate},@RegisterAmount = {model.RegisterAmount} ");
                    else if (model.DiaryNo != null && model.DiaryDate != "")
                        updateReceiptCount = dbContext.Database.ExecuteSqlCommand($"sp_UpdateReceipt @HouseId = {model.HouseId},@ParamUpdate = Diary,@DiaryNo = {model.DiaryNo},@DiaryDate = {model.DiaryDate}");
                    else if (model.DispatchNo != null && model.DispatchDate != "")
                        updateReceiptCount = dbContext.Database.ExecuteSqlCommand($"sp_UpdateReceipt @HouseId = {model.HouseId},@ParamUpdate = Dispatch,@DispatchNo = {model.DispatchNo},@DispatchDate = {model.DispatchDate}");
                    else if (model.StampDutyAmount != null && model.StampDutyDate != "")
                        updateReceiptCount = dbContext.Database.ExecuteSqlCommand($"sp_UpdateReceipt @HouseId = {model.HouseId},@ParamUpdate = StampDuty,@StampDutyAmount = {model.StampDutyAmount},@StampDutyDate = {model.StampDutyDate}");
                    else if (model.ChallanAmount != null && model.ChallanDate != "")
                        updateReceiptCount = dbContext.Database.ExecuteSqlCommand($"sp_UpdateReceipt @HouseId = {model.HouseId},@ParamUpdate = Challan,@ChallanAmount = {model.ChallanAmount},@ChallanDate = {model.ChallanDate}");
                    else if (model.FileExpenditure != null)
                        updateReceiptCount = dbContext.Database.ExecuteSqlCommand($"sp_UpdateReceipt @HouseId = {model.HouseId},@ParamUpdate = FileExpenditure,@FileExpenditure = {model.FileExpenditure}");
                    return updateReceiptCount;
                }
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(ReceiptRepository)} - Method name:  {nameof(UpdateReceipt)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region DELETE

        #endregion

        #endregion

        #region Register Messr (GET,POST,PUT,DELETE)

        #region GET
        public IList<TblRegisterMessrs> GetRegisterMessr()
        {
            try
            {
                var list = dbContext.TblRegisterMessrs.Select(x => x).ToList();
                return list;
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(ReceiptRepository)} - Method name:  {nameof(GetRegisterMessr)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateRegisterMessr(TblRegisterMessrs model)
        {
            try
            {
                var IsRegisterMessrExists = dbContext.TblRegisterMessrs.Where(x => x.PhoneNo == model.PhoneNo).FirstOrDefault();
                if (IsRegisterMessrExists != null)
                    return 0;
                else
                {
                    dbContext.Add(model);
                    return dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(ReceiptRepository)} - Method name:  {nameof(CreateRegisterMessr)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region PUT
        public int UpdateRegisterMessr(TblRegisterMessrs model)
        {
            try
            {
                var recordToUpdateInDb = dbContext.TblRegisterMessrs.Where(x => x.Id == model.Id).FirstOrDefault();
                if (recordToUpdateInDb != null)
                {
                    var duplicateNameRecord = dbContext.TblRegisterMessrs.Where(x => x.Id != model.Id &&
                                                                                x.PhoneNo == model.PhoneNo).FirstOrDefault();

                    if (duplicateNameRecord != null)
                        return 0;
                    else
                    {
                        recordToUpdateInDb.Name = model.Name;
                        recordToUpdateInDb.PhoneNo = model.PhoneNo;
                        return dbContext.SaveChanges();
                    }
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(ReceiptRepository)} - Method name:  {nameof(UpdateRegisterMessr)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region DELETE
        public TblRegisterMessrs DeleteRegisterMessr(int id)
        {
            try
            {
                var IsRegisterMessrExists = dbContext.TblRegisterMessrs.Find(id);
                if (IsRegisterMessrExists != null)
                {
                    dbContext.TblRegisterMessrs.Remove(IsRegisterMessrExists);
                    dbContext.SaveChanges();
                }
                return IsRegisterMessrExists;
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(ReceiptRepository)} - Method name:  {nameof(DeleteRegisterMessr)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #endregion

    }
}
