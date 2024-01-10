using Asp.Versioning.ApiExplorer;
using AutoMapper;
using Core.Api.Configuration;
using Core.Api.SwapModels;
using Core.Data.Context;
using IdentityModel.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Core.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //identity
            services.AddIdentityConfig(Configuration);

            //resolve automaticamente pegando tudo que estiver dentro do assembly
            services.AddAutoMapper(typeof(Startup));

            services.AddApiConfig();

            var xapikey = Configuration["AuthSwap:x-api-key"];
            var granttype = Configuration["AuthSwap:grant_type"];
            var clientid = Configuration["AuthSwap:client_id"];
            var clientsecret = Configuration["AuthSwap:client_secret"];
            var url = Configuration["AuthSwap:url"];
            var urlBase = Configuration["AuthSwap:urlBase"];

            services.AddMemoryCache();
            services.AddHttpClient("Alpha", (sp, httpClient) =>
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("x-api-key", xapikey);

                var cache = sp.GetRequiredService<IMemoryCache>();
                httpClient.BaseAddress = new Uri(urlBase);

                var token = cache.Get<string>("AlphaToken");
                if (token == null)
                {
                    AuthRequest auth = new AuthRequest{ grant_type = granttype, client_id = clientid, client_secret = clientsecret };

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(auth), Encoding.UTF8, "application/json");

                    using (var response = httpClient.PostAsync(url, content).Result)
                    {
                        SwapModels.TokenResponse tokenResponse = JsonConvert.DeserializeObject<SwapModels.TokenResponse>(response.Content.ReadAsStringAsync().Result);
                        token = tokenResponse.access_token;
                        cache.Set("AlphaToken", token, new TimeSpan(0, 0, tokenResponse.expires_in - 180));
                    }
                }

                httpClient.SetBearerToken(token);
            })
                    .ConfigurePrimaryHttpMessageHandler(() => { return new SocketsHttpHandler { UseCookies = false }; })
                    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(20, retryAttempt => TimeSpan.FromMilliseconds(300 * retryAttempt)));

            services.AddSwaggerConfig();

            services.AddLoggingConfig(Configuration);

            //injeção de dependencia dos objetos e do contexto
            services.ResolveDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            // Add log file
            loggerFactory.AddFile("Configuration/Logs/LogCore-{Date}.txt", fileSizeLimitBytes: 20 * 1024 * 1024); // The maximum log file size (20MB here)

            Initialize.SeedUserAdmin(Configuration.GetSection("Roles").Get<List<string>>(), Configuration, app.ApplicationServices, loggerFactory).Wait();

            Initialize.SeedControllersActions(app.ApplicationServices, loggerFactory, Configuration).Wait();

            app.UseSwaggerConfig(provider);

            app.UseApiConfig(env);

            app.UseLoggingConfiguration();

            app.UseDeveloperExceptionPage();
        }
    }
}