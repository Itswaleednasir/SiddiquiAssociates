using MyClientCoreProject.Models;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyClientCoreProject.Repository.Interfaces
{
    public interface IReceipt
    {
        #region Messrs (GET,POST,PUT,DELETE)

        #region GET
        IList<TblMessrs> GetMessrs();
        #endregion

        #region POST
        int CreateMessr(TblMessrs model);
        #endregion

        #region PUT
        int UpdateMessr(TblMessrs model);
        #endregion

        #region DELETE
        TblMessrs DeleteMessr(int Id);
        #endregion

        #endregion

        #region Receipt (GET,POST,PUT,DELETE)

        #region GET
        IList<GetReceiptViewModel> GetReceipt(int HouseId);
        #endregion

        #region POST
        int CreateNewReceipt(ReceiptPaymentViewModel model);
        #endregion

        #region PUT
        int UpdateReceipt(ReceiptPaymentViewModel model);
        #endregion

        #region DELETE

        #endregion

        #endregion

        #region Register Messr (GET,POST,PUT,DELETE)

        #region GET
        IList<TblRegisterMessrs> GetRegisterMessr();
        #endregion

        #region POST
        int CreateRegisterMessr(TblRegisterMessrs model);
        #endregion

        #region PUT
        int UpdateRegisterMessr(TblRegisterMessrs model);
        #endregion

        #region DELETE
        TblRegisterMessrs DeleteRegisterMessr(int id);
        #endregion

        #endregion
    }
}
