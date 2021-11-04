﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.JwtSigningCredentials;
using NetDevPack.Security.JwtSigningCredentials.Store.EntityFrameworkCore;

namespace Core.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext, ISecurityKeyContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<SecurityKeyWithPrivate> SecurityKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
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
                    Name = "fornecedor",
                    NormalizedName = "FORNECEDOR"
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