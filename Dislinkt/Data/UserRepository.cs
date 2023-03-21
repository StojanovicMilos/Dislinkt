using Dislinkt.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dislinkt.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextOptions _options;
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

        public UserRepository(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        {
            _options = options;
            _operationalStoreOptions = operationalStoreOptions;
            using var context = new ApplicationDbContext(options, operationalStoreOptions);
            if(context.Users.Any())
            {
                return;
            }

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            var registeredUser = new ApplicationUser
            {
                Email = "registered@user.com",
                FirstName = "Registered",
                LastName = "User",
                EmailConfirmed = true,
                AccessFailedCount = 0,
                Id = Guid.NewGuid().ToString(),
            };
            registeredUser.PasswordHash = passwordHasher.HashPassword(registeredUser, "registeredUser*123");

            var users = new List<ApplicationUser> { registeredUser };
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            using var context = new ApplicationDbContext(_options, _operationalStoreOptions);
            return context.Users.ToList();
        }
    }
}