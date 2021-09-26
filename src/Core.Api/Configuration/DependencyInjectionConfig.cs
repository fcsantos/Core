using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Core.Api.Extensions;
using Core.Business.Intefaces;
using Core.Business.Notifications;
using Core.Business.Services;
using Core.Data.Context;
using Core.Data.Repository;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MyDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAppControllerRepository, AppControllerRepository>();
            services.AddScoped<IAppActionRepository, AppActionRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();


            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAppControllerService, AppControllerService>();
            services.AddScoped<IAppActionService, AppActionService>();
            services.AddScoped<IClientService, ClientService>();


            services.AddScoped<IDapperDbRepository, DapperDbRepository>();

            services.AddTransient<SymmetricEncryptDecrypt>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
