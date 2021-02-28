using System;
using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MRP.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "da8ff36d13e441ed9dafe01d86136bd0";
                o.LogId = new Guid("e2ec3abe-0d7e-4b2f-9d53-229c3656da8c");
            });

            //services.AddHealthChecks()
            //    .AddElmahIoPublisher(options =>
            //    {
            //        options.ApiKey = "388dd3a277cb44c4aa128b5c899a3106";
            //        options.LogId = new Guid("c468b2b8-b35d-4f1a-849d-f47b60eef096");
            //        options.HeartbeatId = "API Fornecedores";

            //    })
            //    .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
            //    .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            //add ElmahIo como provider(pegando todos os logs passados pelo logger
            //services.AddLogging(builder =>
            //{
            //    builder.AddElmahIo(o =>
            //    {
            //        o.ApiKey = "da8ff36d13e441ed9dafe01d86136bd0";
            //        o.LogId = new Guid("e2ec3abe-0d7e-4b2f-9d53-229c3656da8c");
            //    });
            //    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            //});

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}