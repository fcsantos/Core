﻿using Elmah.Io.AspNetCore;
using Elmah.Io.AspNetCore.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MRP.Api.Extensions;
using System;

namespace MRP.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "da8ff36d13e441ed9dafe01d86136bd0";
                o.LogId = new Guid("d0b9e9ce-94a9-425c-864a-5719f53c0625");
            });

            services.AddHealthChecks()
                .AddElmahIoPublisher(options =>
                {
                    options.ApiKey = "da8ff36d13e441ed9dafe01d86136bd0";
                    options.LogId = new Guid("d0b9e9ce-94a9-425c-864a-5719f53c0625");
                    options.HeartbeatId = "API Fornecedores";

                })
                .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}