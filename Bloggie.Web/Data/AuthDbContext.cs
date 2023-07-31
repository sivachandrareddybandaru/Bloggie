using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext

    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Seed Roles (User , Admin , Super Admin)
            var adminRoleId = "3595e6f-c0f0-4b24-83c7-e51f4b250be9";
            var superAdminRoleId = "80d551dd-c8b9-462c-b2b0-1619e1e4a4df";
            var userRoleId = "90674d97-bf1e-4fd8-8c40-1fdb4d543866";
            var roles = new List<IdentityRole> {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id =adminRoleId,
                    ConcurrencyStamp= adminRoleId
                },
                new IdentityRole {
                Name ="SuperAdmin",
                NormalizedName ="SuperAdmin",
                Id=superAdminRoleId,
                ConcurrencyStamp= superAdminRoleId


                },
                new IdentityRole
                {
                    Name="User",
                    NormalizedName = "User",
                    Id=userRoleId,
                    ConcurrencyStamp= userRoleId

                }

            };
            builder.Entity<IdentityRole>().HasData(roles);



            var superAdminId = "c4c56cc0-4116-4418-93f8-9bd85a277bb3";
            var superAdminUser = new IdentityUser { 
            UserName="superadmin@bloggie.com",  
            Email ="superadmin@bloggie.com",
            NormalizedEmail="superadmin@bloggie.com".ToUpper(),
            NormalizedUserName="superadmin@bloggie.com".ToUpper(),
            Id=superAdminId,
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Superadmin@123");

            builder.Entity<IdentityUser>().HasData( superAdminUser );


            var superAdminRoles = new List<IdentityUserRole<string>> {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData( superAdminRoles );

        }

    }
}
