using Common;
using DataAcesss.Data;
using Hotel.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Service
{
    public class DbInitializer : IDinitializer
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbInitializer(ApplicationDbContext db , UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void Initalize()
        {
            try
            {
                if(db.Database.GetPendingMigrations().Count() > 0)
                {
                    db.Database.Migrate();
                }
               

            }
            catch
            {

            }
           
            if (db.Roles.Any(x => x.Name == SD.Role_Admin)) return;

            roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();


            userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            }, "Aa123456!@").GetAwaiter().GetResult();


            IdentityUser user = db.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
            userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

        }
    }
}
