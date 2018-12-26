using AutoMapper;
using Geek.Project.Core.Repository.Impl;
using Geek.Project.Core.Repository.Interface;
using Geek.Project.Core.Service.Impl;
using Geek.Project.Core.Service.Interface;
using Geek.Project.Core.ViewModel.SysUser;
using Geek.Project.Infrastructure.DataBase;
using Geek.Project.Infrastructure.Services;
using Geek.Project.Infrastructure.UnitOfWork;
using Geek.Project.Portal.Models;
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
            #region 认证

            ////清空默认绑定的用户信息
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            ////添加认证服务
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = "Cookies";              //默认使用Cookies方案进行认证
            //    options.DefaultChallengeScheme = "oidc";        //默认认证失败时启用oidc方案
            //})
            //.AddCookie("Cookies")   //添加Cookies认证方案

            ////添加oidc方案
            //.AddOpenIdConnect("oidc", options =>
            //{
            //    options.SignInScheme = "Cookies";       //身份验证成功后使用Cookies方案来保存信息
            //    options.Authority = "http://140.143.7.32:5000";    //授权服务地址
            //    options.RequireHttpsMetadata = false;
            //    options.ClientId = "mvc_implicit";
            //    options.ResponseType = "id_token token";    //默认只返回id_token 这里添加上token(Access Token)
            //    options.SaveTokens = true;
            //});

            #endregion

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #region cookie

            services.AddAuthentication(AdminAuthorizeAttribute.AdminAuthenticationScheme)
                .AddCookie(AdminAuthorizeAttribute.AdminAuthenticationScheme, options =>
                {
                    options.LoginPath = "/Login/Index";//登录路径
                    options.LogoutPath = "/Login/LogOut";//退出路径
                    options.AccessDeniedPath = new PathString("/Error/Forbidden");//拒绝访问页面
                    options.Cookie.Path = "/";
                });

            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //https
            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //    options.HttpsPort = 8801;
            //});

            services.AddDbContext<ProjectDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                //options.UseSqlite("Data Source=MyDb.db");
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Scoped);

            //mapper
            services.AddAutoMapper();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISysUserRepository, SysUserRepository>();
            services.AddScoped<ISysRoleRepository, SysRoleRepository>();
            services.AddScoped<ISysLogRepository, SysLogRepository>();
            //services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

            services.AddScoped<ISysUserService, SysUserService>();
            services.AddScoped<ISysRoleService, SysRoleService>();
            services.AddScoped<ISysLogService, SysLogService>();

            //排序
            var propertyMappingContainer = new PropertyMappingContainer();
            propertyMappingContainer.Register<UserPropertyMapping>();
            services.AddSingleton<IPropertyMappingContainer>(propertyMappingContainer);

            services.AddTransient<ITypeHelperService, TypeHelperService>();
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
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            //app.UseHttpsRedirection();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Index}/{id?}");

            });
        }
    }
}
