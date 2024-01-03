using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Initializer.Interfaces;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(AppDbContext context, UserManager<ApplicationUser> user, RoleManager<IdentityRole> role)
        {
            _context = context;
            _user = user;
            _role = role;
        }

        public void Initialize()
        {
            IdentityRole adminUser = _role.FindByNameAsync(IdentityConfiguration.Admin).Result;

            if (adminUser != null)
                return;

            IdentityRole adminRole = new IdentityRole(IdentityConfiguration.Admin);
            IdentityRole clientRole = new IdentityRole(IdentityConfiguration.Client);

            _role.CreateAsync(adminRole).GetAwaiter().GetResult();
            _role.CreateAsync(clientRole).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "victor-admin",
                Email = "victor-admin@guirra.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (61) 12345-6789",
                FirstName = "Victor",
                LastName = "Admin"
            };

            _user.CreateAsync(admin, "Guirra123$").GetAwaiter().GetResult();
            _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var adminClaims = _user.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
            }).Result;
            
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "victor-client",
                Email = "victor-client@guirra.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (61) 12345-6789",
                FirstName = "Victor",
                LastName = "Client"
            };

            _user.CreateAsync(client, "Guirra123$").GetAwaiter().GetResult();
            _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();

            var clientClaims = _user.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FirstName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client),
            }).Result;
        }
    }
}
