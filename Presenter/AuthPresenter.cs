using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyClientCoreProject.Helper;
using MyClientCoreProject.Models.DB;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.Utilities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyClientCoreProject.Presenter
{
    public class AuthPresenter
    {
        private readonly IEmployee _employee;
        private readonly IConfiguration configuration;
        UtilitiesProvider up;

        public AuthPresenter(IEmployee employee, IHostingEnvironment env,IConfiguration configuration)//Constructor
        {
            _employee = employee;
            this.configuration = configuration;
            up = new UtilitiesProvider(env);
        }

        public string GetAuthToken(string userName, string password)
        {
            try
            {
                var token = "";

                var employeeRecord = _employee.GetEmployeeForAuthorization(x => x.UserName == userName && x.Password == password).FirstOrDefault();
                if (employeeRecord != null)
                {
                    var employeeRole = employeeRecord.RoleId == 1 ? "Admin" : "Employee";
                    var jwtSecret = configuration.GetValue<string>("AppSettings:JWT_SECRET");
                    var accessTokenExpiration = configuration.GetValue<int>("AppSettings:ACCESS_TOKEN_EXPIRES_IN_MINUTES");

                    token = GenerateTokenAsync(userId: employeeRecord.Id,
                                               userName: employeeRecord.UserName,
                                               employeeRole: employeeRole,
                                               jwtSecret: jwtSecret,
                                               jwtExpiry: accessTokenExpiration);
                    //var empRecord = _employee.SaveToken(token, employeeRecord.Id);
                }
                return token;
            }
            catch (Exception ex)
            {
                StaticHelper.LogException(path: up.GetLogFilePath(), errorMessage: ex.Message, methodName: $"Class Name: {nameof(AuthPresenter)} - Method name:  {nameof(GetAuthToken)}", stackTrace: ex.StackTrace);
                return null;
            }
        }

        public string GenerateTokenAsync(int userId, string userName,string employeeRole,string jwtSecret, int jwtExpiry)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecret);

            var identity = new ClaimsIdentity(authenticationType: "JWT");
            identity.AddClaim(new Claim("userId", userId.ToString()));
            identity.AddClaim(new Claim("userName", userName));
            identity.AddClaim(new Claim("role", employeeRole));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                identity
                ),
                Expires = DateTime.UtcNow.AddMinutes(jwtExpiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenResult = tokenHandler.WriteToken(token);
            return tokenResult;
        }
    }
}
