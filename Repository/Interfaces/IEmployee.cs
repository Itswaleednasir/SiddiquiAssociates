using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.ViewModel;
using System.Linq.Expressions;

namespace MyClientCoreProject.Repository.Interfaces
{
    public interface IEmployee
    {
        #region Employee (GET,POST,PUT,DELETE)

        #region GET
        IList<EmployeeRoleViewModel> GetEmployees();
        #endregion

        #region POST
        int CreateEmployee(TblEmployee model);
        #endregion

        #region PUT
        int UpdateEmployee(EmployeeRoleViewModel model);
        #endregion

        #region DELETE
        TblEmployee DeleteEmployee(int Id);
        #endregion

        #endregion

        #region Roles(GET,POST,DELETE)

        #region GET
        IList<SelectListItem> GetRole();
        #endregion

        #region POST
        int CreateRole(TblEmployeeRole model);
        #endregion

        #region DELETE
        TblEmployeeRole DeleteRole(int Id);
        #endregion

        #endregion

        #region Authorization(GET)
        IEnumerable<TblEmployee> GetEmployeeForAuthorization(Expression<Func<TblEmployee, bool>> predicate);

        TblEmployee SaveToken(string token, int id);
        #endregion
    }
}
