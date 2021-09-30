using Auth.API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Auth.API.Data
{
    public class SeedData
    {
        public static void InitializeDummyData(IServiceProvider serviceProvider)
        {
            var _context = serviceProvider.GetRequiredService<AppIdentityDbContext>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            _context.Database.EnsureCreated();

            if (!_context.Roles.Any())
            {
                _ = _roleManager.CreateAsync(new() { Name = "Admin" }).Result;
                _ = _roleManager.CreateAsync(new() { Name = "Moderator" }).Result;
                _ = _roleManager.CreateAsync(new() { Name = "Editor" }).Result;
            }

            if (!_context.Users.Any())
            {
                AppUser user = new()
                {
                    UserName = "Erşan Kuneri",
                    Email = "ersankuneri@gmail.com"
                };
                var result = _userManager.CreateAsync(user, "TebrikDeğilTevkiftir0!").ConfigureAwait(false).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
