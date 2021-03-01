using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MRP.Api.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var idUser = SeedUsers(builder);
            SeedRoles(builder);
            SeedUserRoles(builder, idUser);
        }

        private string SeedUsers(ModelBuilder builder)
        {
            IdentityUser user = new IdentityUser()
            {
                UserName = "Admin",
                Email = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };

            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
            passwordHasher.HashPassword(user, "Admin*123");

            builder.Entity<IdentityUser>().HasData(user);

            return user.Id;
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "Fornecedor", ConcurrencyStamp = "2", NormalizedName = "Fornecedor teste" }
            );
        }

        private void SeedUserRoles(ModelBuilder builder, string idUser)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = idUser }
            );
        }
    }
}