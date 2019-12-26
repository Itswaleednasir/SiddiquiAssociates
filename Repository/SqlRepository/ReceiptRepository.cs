using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MyClientCoreProject.Helper;
using MyClientCoreProject.Models;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.Utilities;
using MyClientCoreProject.ViewModel;

namespace MyClientCoreProject.Repository.SqlRepository
{
    public class ReceiptRepository : IReceipt
    {
        private readonly SiddiquiAssociateDBContext dbContext;
        private readonly IHostingEnvironment env;
        UtilitiesProvider up;

        public ReceiptRepository(SiddiquiAssociateDBContext dbContext, IHostingEnvironment env) //Constructor
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
                var IsMessrExists = dbContext.TblMessrs.Where(x => x.PhoneNo == model.PhoneNo || x.Cnic == model.Cnic).FirstOrDefault();
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
                                                                            (x.PhoneNo == model.PhoneNo ||
                                                                             x.Cnic == model.Cnic)).FirstOrDefault();

                    if (duplicateNameRecord != null)
                    {
                        return 0;
                    }
                    else
                    {
                        recordToUpdateInDb.Name = model.Name;
                        recordToUpdateInDb.PhoneNo = model.PhoneNo;
                        recordToUpdateInDb.Cnic = model.Cnic;
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
                var receiptTable = dbContext.TblReceipt.Select(x => x).Where(x => x.HouseId == model.HouseId).FirstOrDefault();

                //var paymentTable = dbContext.TblPayments.Select(x => x).Where(x => x.ReceiptId == receiptTable.Id).order
                //var viewModel = new Order();
                //viewModel.GL = "model ki value";
                //viewModel.BLIC = "model ki value1";

                //var checkReciept = orderDetail;
                var payments = new TblPayments();
                foreach (var item in model.GetType().GetProperties())
                {

                    var columnName = item.Name;
                    var val = model.GetType().GetProperty(columnName).GetValue(model);

                    if (receiptTable.GetType().GetProperty(columnName) != null)
                    {
                        if (val != null && !string.IsNullOrEmpty(val.ToString()) && val.ToString() != "0")
                        {
                            receiptTable.GetType().GetProperty(columnName).SetValue(receiptTable, val);
                        }

                    }

                    if (payments.GetType().GetProperty(columnName) != null)
                    {

                        if (val != null && !string.IsNullOrEmpty(val.ToString()) && val.ToString() != "0")
                        {
                            payments.GetType().GetProperty(columnName).SetValue(payments, val);
                        }
                    }
                    //var IsReceiptExists = dbContext.TblReceipt.Where(x => x.HouseId == model.HouseId).FirstOrDefault();
                    //if (IsReceiptExists == null)
                    //    return 0;
                    //else
                    //{
                    //    int updateReceiptCount = dbContext.Database.ExecuteSqlCommand($"sp_UpdateReceipt {model.HouseId},{model.Amount},{model.PaymentDate}");
                    //    return updateReceiptCount;
                    //}
                }
                if (payments != null)
                {
                    payments.ReceiptId = receiptTable.Id;
                    dbContext.TblPayments.Add(payments);
                }

                if (receiptTable != null)
                {
                    dbContext.Entry(receiptTable).State = EntityState.Modified;

                }

                return dbContext.SaveChanges();
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
