using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTime.Model
{
    public static class DataInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = "OtcsAdmin",
                    Email = "osamak063@gmail.com",
                    PhoneNumber = "03432507836",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }


            if (userManager.FindByEmailAsync("deo@gmail.com").Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = "OtcsDeo",
                    Email = "osamak@gmail.com",
                    PhoneNumber = "03432507836",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "DataEntryOperator").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Client").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Client";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("DataEntryOperator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "DataEntryOperator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
