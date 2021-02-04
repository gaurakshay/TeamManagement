using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotmim.Sync;
using Dotmim.Sync.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotmimSampleServer
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
            services.AddControllers();
            services.AddMemoryCache();

            // connection string to the db
            var connectionString = Configuration.GetSection("ConnectionStrings")["AppDataConnection"];

            // Create setup.
            var setup = new SyncSetup(new string[] { "Employees", "Memberships", "Teams" });
            //// Add filter on employee ID.
            //var employeeFilter = new SetupFilter("Employees");
            //employeeFilter.AddParameter("ID", "Employees", allowNull: true);
            //employeeFilter.AddWhere("ID", "Employees", "ID");
            //setup.Filters.Add(employeeFilter);
            //// Add filter on membership table
            //var membershipFilter = new SetupFilter("Memberships");
            //membershipFilter.AddParameter("ID", "Employees", allowNull: true);
            //membershipFilter.AddJoin(Join.Inner, "Employees")
            //    .On("Memberships", "EmployeeID", "Employees", "ID");
            //membershipFilter.AddWhere("ID", "Employees", "ID");
            //setup.Filters.Add(membershipFilter);
            //// Add filter on Team table
            //var teamFilter = new SetupFilter("Teams");
            //teamFilter.AddParameter("ID", "Employees", allowNull: true);
            //teamFilter.AddJoin(Join.Inner, "Memberships")
            //    .On("Teams", "ID", "Memberships", "TeamID");
            //teamFilter.AddJoin(Join.Inner, "Employees")
            //    .On("Memberships", "EmployeeID", "Employees", "ID");
            //teamFilter.AddWhere("ID", "Employees", "ID");
            //setup.Filters.Add(teamFilter);

            services.AddSyncServer<SqlSyncChangeTrackingProvider>(connectionString, setup);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
