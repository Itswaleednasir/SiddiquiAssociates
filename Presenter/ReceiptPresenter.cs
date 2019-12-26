using Microsoft.AspNetCore.Hosting;
using MyClientCoreProject.Helper;
using MyClientCoreProject.Models;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.Utilities;
using MyClientCoreProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyClientCoreProject.Presenter
{
    public class ReceiptPresenter
    {
        private readonly IReceipt receipt;
        private readonly IHostingEnvironment env;
        UtilitiesProvider up;

        public ReceiptPresenter(IReceipt receipt, IHostingEnvironment env) // Constructor
        {
            this.receipt = receipt;
            this.env = env;
            up = new UtilitiesProvider(env);
        }

        #region Messrs (GET,POST,PUT,DELETE)

        #region GET
        public IList<TblMessrs> GetMessrs()
        {
            try
            {
                return receipt.GetMessrs();
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(ReceiptPresenter)} - Method name:  {nameof(GetMessrs)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateNewMessr(TblMessrs model)
        {
            try
            {
                return receipt.CreateMessr(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(ReceiptPresenter)} - Method name:  {nameof(CreateNewMessr)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region PUT
        public int UpdateMessr(TblMessrs model)
        {
            try
            {
                return receipt.UpdateMessr(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(ReceiptPresenter)} - Method name:  {nameof(UpdateMessr)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region DELETE
        public TblMessrs DeleteMessr(int Id)
        {
            try
            {
                return receipt.DeleteMessr(Id);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(ReceiptPresenter)} - Method name:  {nameof(DeleteMessr)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #endregion

        #region Receipt (GET,POST,PUT,DELETE)

        #region GET
        public IList<GetReceiptViewModel> GetReceipt(int HouseId)
        {
            try
            {
                return receipt.GetReceipt(HouseId);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(ReceiptPresenter)} - Method name:  {nameof(GetReceipt)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateNewReceipt(ReceiptPaymentViewModel model)
        {
            try
            {
                return receipt.CreateNewReceipt(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(ReceiptPresenter)} - Method name:  {nameof(CreateNewReceipt)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region PUT
        public int UpdateReceipt(ReceiptPaymentViewModel model)
        {
            try
            {
                return receipt.UpdateReceipt(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(ReceiptPresenter)} - Method name:  {nameof(UpdateReceipt)}", stackTrace: ex.StackTrace);
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
                return receipt.GetRegisterMessr();
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(ReceiptPresenter)} - Method name:  {nameof(GetRegisterMessr)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateNewRegisterMessr(TblRegisterMessrs model)
        {
            try
            {
                return receipt.CreateRegisterMessr(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(ReceiptPresenter)} - Method name:  {nameof(CreateNewRegisterMessr)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region PUT
        public int UpdateRegisterMessr(TblRegisterMessrs model)
        {
            try
            {
                return receipt.UpdateRegisterMessr(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(ReceiptPresenter)} - Method name:  {nameof(UpdateRegisterMessr)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region DELETE
        public TblRegisterMessrs DeleteRegisterMessr(int id)
        {
            try
            {
                return receipt.DeleteRegisterMessr(id);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(ReceiptPresenter)} - Method name:  {nameof(DeleteRegisterMessr)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #endregion

    }
}
