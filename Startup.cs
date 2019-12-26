using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Microsoft.AspNetCore.Mvc;
using MyClientCoreProject.Models.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyClientCoreProject.Repository.Interfaces;
using MyClientCoreProject.Repository.SqlRepository;


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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                             .AddJsonOptions(options => {
                                 var resolver = options.SerializerSettings.ContractResolver;
                                 if (resolver != null)
                                     (resolver as DefaultContractResolver).NamingStrategy = null;
                             });

            services.AddDbContext<SiddiquiAssociateDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            services.AddCors();

            services.AddScoped<IEmployee,EmployeeRepository>();
            services.AddScoped<IFile, FileRepository>();
            services.AddScoped<IReceipt, ReceiptRepository>();

            //services.AddScoped<,>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

            app.UseMvc();
        }
    }
}
