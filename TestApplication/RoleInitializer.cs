using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestApplication.DAL.Entities;

namespace TestApplication
{
    public class RoleInitializer
    {
        public static UserManager<User> userManager;
        public static RoleManager<Role> roleManager;

        public static async Task InitializeAsync()
        {
            string adminEmail = "Director@mail.ru";
            string password = "!Number98";
            if (await roleManager.FindByNameAsync("director") == null)
            {
                await roleManager.CreateAsync(new Role { Name = "director" });
                await roleManager.CreateAsync(new Role { Name = "manager" });
                await roleManager.CreateAsync(new Role { Name = "employee" });
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "director");
                }
            }
        }
    }
}
