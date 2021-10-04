using System;
using System.Configuration;
using System.IO;
using Core.Job.Services;
using Hangfire;
using Hangfire.Storage.SQLite;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core.Job
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddHangfire(config =>
            //    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseDefaultTypeSerializer()
            //    .UseSqlServerStorage(Configuration["Hangfire"]));

            string diretoryHangfireDB = Configuration["DiretoryHangfire"];
            if (!Directory.Exists(diretoryHangfireDB))
            {
                Directory.CreateDirectory(diretoryHangfireDB);
            }

            services.AddHangfire(config =>
                config.UseSQLiteStorage(Configuration["ConnectionStringHangfire"]));

            services.AddHangfireServer();

            services.AddSingleton<ISendMailToPatient, SendMailToPatient>();
            services.AddSingleton<IPrintJob, PrintJob>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireServer();

            //Basic Authentication added to access the Hangfire Dashboard
            app.UseHangfireDashboard("/JARVIS", new DashboardOptions()
            {
                AppPath = null,
                DashboardTitle = "JARVIS Dashboard",
                Authorization = new[]{
                new HangfireCustomBasicAuthenticationFilter{
                    User = Configuration.GetSection("HangfireCredentials:UserName").Value,
                    Pass = Configuration.GetSection("HangfireCredentials:Password").Value
                }
            }
            }); ;

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Wellcome JARVIS!!");
                });
            });

            backgroundJobClient.Enqueue(() => Console.WriteLine("Jobs - Run every minute!"));

            //recurringJobManager.AddOrUpdate("Send Access to New Patient", () => serviceProvider.GetService<ISendMailToPatient>().SendAccessToNewPatientAsync(Configuration.GetSection("APICoreUrl").Value), "* * * * *");
            //recurringJobManager.AddOrUpdate("Notification of new message to the Patient", () => serviceProvider.GetService<ISendMailToPatient>().NotificationOfNewMessageToPatientAsync(Configuration.GetSection("APICoreUrl").Value), "* * * * *");
            //recurringJobManager.AddOrUpdate("Notification of inquiry schedule to the Patient", () => serviceProvider.GetService<ISendMailToPatient>().NotificationOfInquiryScheduleToPatient(Configuration.GetSection("APICoreUrl").Value), "* * * * *");
            recurringJobManager.AddOrUpdate("Job Sample", () => serviceProvider.GetService<IPrintJob>().Print(), "* * * * *");
        }
    }
}
