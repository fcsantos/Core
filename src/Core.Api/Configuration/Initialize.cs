using Core.Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Api.Configuration
{
    public static class Initialize
    {
        public static async Task SeedUserAdmin(IList<string> roles, IConfiguration configuration, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            var list = roles;
            var logger = loggerFactory.CreateLogger("UserAdminDatabase");

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                if (!dbContext.Users.Any(x => x.UserName.Equals(configuration["AppUserAdmin:UserName"])))
                {
                    logger.LogDebug("-----------------------------------Begin Add User Admin automatic-----------------------------------");

                    try
                    {
                        RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                        UserManager<IdentityUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                        var user = new IdentityUser { UserName = configuration["AppUserAdmin:UserName"], Email = configuration["AppUserAdmin:Email"], EmailConfirmed = Convert.ToBoolean(configuration["AppUserAdmin:EmailConfirmed"]) };
                        var result = await userManager.CreateAsync(user, configuration["AppUserAdmin:Password"]);
                        if (result.Succeeded)
                        {
                            if (!roleManager.RoleExistsAsync(configuration["AppUserAdmin:Role"]).Result)
                            {
                                IdentityRole role = new IdentityRole();
                                role.Name = configuration["AppUserAdmin:Role"];
                                roleManager.CreateAsync(role).Wait();
                            }
                            //add Role
                            userManager.AddToRoleAsync(user, configuration["AppUserAdmin:Role"]).Wait();

                            //add Claim teste
                            userManager.AddClaimAsync(user, new Claim(configuration["ClaimsFornecedor:ClaimType"], configuration["ClaimsFornecedor:ClaimValue"])).Wait();

                            logger.LogDebug("User admin created a new account with password and add to a role " + configuration["AppUserAdmin:Role"]);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error: " + ex.Message);
                    }

                    logger.LogDebug("------------------------------------End Add User Admin automatic------------------------------------");
                }
            }
        }
    }
}
