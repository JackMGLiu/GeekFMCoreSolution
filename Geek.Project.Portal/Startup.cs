using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geek.Project.Core.Repository.Impl;
using Geek.Project.Core.Repository.Interface;
using Geek.Project.Core.Service.Impl;
using Geek.Project.Core.Service.Interface;
using Geek.Project.Infrastructure.DataBase;
using Geek.Project.Infrastructure.Repository;
using Geek.Project.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geek.Project.Portal
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //https
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 9001;
            });

            services.AddDbContext<ProjectDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                //options.UseSqlite("Data Source=MyDb.db");
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Scoped);


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISysUserRepository, SysUserRepository>();
            //services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

            services.AddScoped<ISysUserService, SysUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
