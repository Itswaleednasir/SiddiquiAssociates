using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using MyClientCoreProject.Models.DB;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.Repository.SqlRepository;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using Microsoft.OpenApi.Models;
using MyClientCoreProject.Utilities;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MyClientCoreProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "core api", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var wwwRootPath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(wwwRootPath, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials();
            }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                             .AddJsonOptions(options => {
                                 var resolver = options.SerializerSettings.ContractResolver;
                                 if (resolver != null)
                                     (resolver as DefaultContractResolver).NamingStrategy = null;
                             });        

            services.AddDbContext<SiddiquiAssociateDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            var Key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("AppSettings:JWT_SECRET"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidateLifetime = true

                };
           
            });

            services.AddScoped<ICustomLogger, CustomLogger>();
            services.AddScoped<IEmployee,EmployeeRepository>();
            services.AddScoped<IFile, FileRepository>();
            services.AddScoped<IReceipt, ReceiptRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "core api v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
            app.UseCors("AllowAll");

            app.UseAuthentication();
            //app.UseAuthorization();

            app.UseMvc();
            
        }
    }
}
