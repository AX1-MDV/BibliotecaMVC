using Microsoft.AspNetCore.Identity;

namespace BibliotecaMVC.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            // Roles requeridos
            var roles = new[] { "Administrador", "Lector" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Usuario administrador por defecto configurable desde appsettings
            var adminUserName = config["AdminUser:UserName"] ?? "admin";
            var adminEmail = config["AdminUser:Email"] ?? "admin@biblioteca.local";
            var adminPassword = config["AdminUser:Password"] ?? "Admin123!";

            var admin = await userManager.FindByNameAsync(adminUserName) ?? await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new IdentityUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Administrador");
                }
                // En producción, registre/muestre errores apropiadamente
            }
            else
            {
                if (!await userManager.IsInRoleAsync(admin, "Administrador"))
                {
                    await userManager.AddToRoleAsync(admin, "Administrador");
                }
            }
        }
    }
}