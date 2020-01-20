using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyClientCoreProject.Helper;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.Utilities;
using MyClientCoreProject.ViewModel;

namespace MyClientCoreProject.Repository.SqlRepository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly DocumentManagement_WContext dbContext;
        private readonly IHostingEnvironment env;
        UtilitiesProvider up;

        public EmployeeRepository(DocumentManagement_WContext dbContext, IHostingEnvironment env) //Constructor
        {
            this.dbContext = dbContext;
            this.env = env;
            up = new UtilitiesProvider(env);
        }

        #region Employee (GET,POST,PUT,DELETE)

        #region GET
        public IList<EmployeeRoleViewModel> GetEmployees()
        {
            try
            {
                var employeeList = dbContext.EmployeeRoleViewModel.FromSql("sp_GetAllEmployees").ToList();
                return employeeList;
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(EmployeeRepository)} - Method name:  {nameof(GetEmployees)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateEmployee(TblEmployee model)
        {
            try
            {
                var IsEmployeeExists = dbContext.TblEmployee.Where(x => x.FullName == model.FullName || x.UserName == model.UserName || x.PhoneNo == model.PhoneNo).FirstOrDefault();
                if (IsEmployeeExists != null)
                    return 0;
                else
                {
                    dbContext.Add(model);
                    return dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(EmployeeRepository)} - Method name:  {nameof(CreateEmployee)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region PUT
        public int UpdateEmployee(EmployeeRoleViewModel model)
        {
            try
            {
                var recordToUpdateInDb = dbContext.TblEmployee.Where(x => x.Id == model.EmployeeId).FirstOrDefault();
                if (recordToUpdateInDb != null)
                {
                    var duplicateNameRecord = dbContext.TblEmployee.Where(x => x.Id != model.EmployeeId &&
                                                                          x.FullName == model.FullName &&
                                                                          x.UserName == model.Username &&
                                                                          x.PhoneNo == model.PhoneNo).FirstOrDefault();

                    if (duplicateNameRecord != null)
                    {
                        return 0;
                    }
                    else
                    {
                        recordToUpdateInDb.FullName = model.FullName;
                        recordToUpdateInDb.UserName = model.Username;
                        recordToUpdateInDb.PhoneNo = model.PhoneNo;
                        recordToUpdateInDb.Password = model.Password;
                        recordToUpdateInDb.RoleId = model.RoleId;
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
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(EmployeeRepository)} - Method name:  {nameof(UpdateEmployee)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region DELETE
        public TblEmployee DeleteEmployee(int Id)
        {
            try
            {
                var IsEmployeeExists = dbContext.TblEmployee.Find(Id);
                if (IsEmployeeExists != null)
                {
                    dbContext.TblEmployee.Remove(IsEmployeeExists);
                    dbContext.SaveChanges();
                }
                return IsEmployeeExists;
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(EmployeeRepository)} - Method name:  {nameof(DeleteEmployee)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #endregion

        #region Roles (GET.POST,DELETE)

        #region GET
        public IList<SelectListItem> GetRole()
        {
            try
            {
                return dbContext.TblEmployeeRole.Select(x => new SelectListItem
                {
                    Text = x.Role,
                    Value = x.Id.ToString()
                }).OrderBy(x => x.Value).ToList();
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(EmployeeRepository)} - Method name:  {nameof(GetRole)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateRole(TblEmployeeRole model)
        {
            try
            {
                var IsEmployeeRoleExists = dbContext.TblEmployeeRole.Where(x => x.Role == model.Role).FirstOrDefault();
                if (IsEmployeeRoleExists != null)
                    return 0;
                else
                {
                    dbContext.Add(model);
                    return dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(EmployeeRepository)} - Method name:  {nameof(CreateRole)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region DELETE
        public TblEmployeeRole DeleteRole(int Id)
        {
            try
            {
                var IsEmployeeRoleExists = dbContext.TblEmployeeRole.Find(Id);
                if (IsEmployeeRoleExists != null)
                {
                    dbContext.TblEmployeeRole.Remove(IsEmployeeRoleExists);
                    dbContext.SaveChanges();
                }
                return IsEmployeeRoleExists;
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(EmployeeRepository)} - Method name:  {nameof(DeleteRole)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #endregion

        #region Authorization

        #region GET
        public IEnumerable<TblEmployee> GetEmployeeForAuthorization(Expression<Func<TblEmployee, bool>> predicate)
        {
            try
            {
                return dbContext.TblEmployee.Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Repository name: {nameof(EmployeeRepository)} - Method name:  {nameof(GetEmployeeForAuthorization)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #endregion
    }
}
