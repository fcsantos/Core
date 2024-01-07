using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Core.Model;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;

namespace Core.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext, ISecurityKeyContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<KeyMaterial> SecurityKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new KeyMaterialMap());

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "user",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "geral",
                    NormalizedName = "GERAL"
                },
                new IdentityRole
                {
                    Name = "beneficiario",
                    NormalizedName = "BENEFICIARIO"
                },
                new IdentityRole
                {
                    Name = "cliente",
                    NormalizedName = "CLIENTE"
                }
            );

            base.OnModelCreating(builder);
        }
    }
}