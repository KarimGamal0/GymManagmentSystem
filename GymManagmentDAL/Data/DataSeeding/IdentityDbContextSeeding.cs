using GymManagmentDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.DataSeeding
{
    public static class IdentityDbContextSeeding
    {

        public static bool SeedDate(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                var HasUser = userManager.Users.Any();
                var HasRole = roleManager.Roles.Any();

                if (HasUser && HasRole) return false;
                if (!HasRole)
                {
                    var Roles = new List<IdentityRole>()
                    {
                        new() { Name = "SuperAdmin" },
                        new() { Name = "Admin" }
                    };
                    foreach (var role in Roles)
                    {
                        if (!roleManager.RoleExistsAsync(role.Name!).Result)
                        {
                            roleManager.CreateAsync(role).Wait();
                        }
                    }
                }

                if (!HasUser)
                {
                    var MainAdmin = new ApplicationUser()
                    {
                        FirstName = "Karim",
                        LastName = "Gamal",
                        UserName = "KarimGamal",
                        Email = "karim.gamal.cs@gmail.com",
                        PhoneNumber = "01001046713"
                    };
                    userManager.CreateAsync(MainAdmin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();

                    var Admin = new ApplicationUser()
                    {
                        FirstName = "Khaled",
                        LastName = "Gamal",
                        UserName = "KhaledGamal",
                        Email = "khaled.gamal.cs@gmail.com",
                        PhoneNumber = "01001046750"
                    };
                    userManager.CreateAsync(Admin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seed Failed {ex}");
                return false;
            }
        }
    }
}
