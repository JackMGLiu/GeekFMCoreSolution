﻿using Geek.Project.Infrastructure.DataBase;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;

namespace Geek.Project.Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logDbConnection = @"Server=140.143.7.32,1433\\mssql2012;database=GeekBolgDb;uid=sa;pwd=sa2012LJ";
            var logTable = "Logs";
            var opts = new ColumnOptions
            {
                AdditionalDataColumns = new Collection<DataColumn>
                {
                    new DataColumn {DataType = typeof (string), ColumnName = "User"},
                    new DataColumn {DataType = typeof (string), ColumnName = "Class"},
                    new DataColumn {DataType = typeof (string), ColumnName = "Url"}
                }
            };

            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File(Path.Combine("logs", @"log.txt"), rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level} {Class} {Url} {User}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                    .WriteTo.MSSqlServer(logDbConnection, logTable, columnOptions: opts, autoCreateSqlTable: true, restrictedToMinimumLevel: LogEventLevel.Information)
                    //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .CreateLogger();

            //CreateWebHostBuilder(args).Build().Run();

            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<ProjectDbContext>();
                    ProjectContextSeed.SeedAsync(context, loggerFactory).Wait();
                }
                catch (Exception e)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(e, "初始化数据失败！");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseIISIntegration()
                .UseSerilog();
    }
}
