using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MyClientCoreProject.Controllers;
using MyClientCoreProject.Helper;
using MyClientCoreProject.Utilities;
using MyClientCoreProject.Models.DB;

namespace MyClientCoreProject.Presenter
{
    public class EmployeePresenter
    {
        private readonly IEmployee employee;
        UtilitiesProvider up;

        public EmployeePresenter(IEmployee employee, IHostingEnvironment env) //Constructor
        {
            this.employee = employee;
            up = new UtilitiesProvider(env);
        }

        #region Employee (GET,POST,PUT,DELETE)

        #region GET
        public IList<EmployeeRoleViewModel> GetEmployees()
        {
            try
            {
                return employee.GetEmployees();
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(EmployeePresenter)} - Method name:  {nameof(GetEmployees)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateNewEmployee(TblEmployee model)
        {
            try
            {
                return employee.CreateEmployee(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(EmployeePresenter)} - Method name:  {nameof(CreateNewEmployee)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region PUT
        public int UpdateEmployee(EmployeeRoleViewModel model)
        {
            try
            {
                return employee.UpdateEmployee(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(EmployeePresenter)} - Method name:  {nameof(UpdateEmployee)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region DELETE
        public TblEmployee DeleteEmployee(int Id)
        {
            try
            {
                return employee.DeleteEmployee(Id);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(EmployeePresenter)} - Method name:  {nameof(DeleteEmployee)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #endregion

        #region Roles (GET,POST,DELETE)

        #region GET
        public IList<SelectListItem> GetRoles()
        {
            try
            {
                return employee.GetRole();
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(EmployeePresenter)} - Method name:  {nameof(GetRoles)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region POST
        public int CreateNewRole(TblEmployeeRole model)
        {
            try
            {
                return employee.CreateRole(model);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(EmployeePresenter)} - Method name:  {nameof(CreateNewRole)}", stackTrace: ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region DELETE
        public TblEmployeeRole DeleteRole(int Id)
        {
            try
            {
                return employee.DeleteRole(Id);
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(EmployeePresenter)} - Method name:  {nameof(DeleteRole)}", stackTrace: ex.StackTrace);
                return null;
            }
        }
        #endregion

        #endregion

    }
}

