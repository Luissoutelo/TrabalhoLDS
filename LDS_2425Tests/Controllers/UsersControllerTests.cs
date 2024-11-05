using LDS_2425;
using LDS_2425.Controllers;
using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LDS_2425Tests.Controllers
{
    public class UsersControllerTests : IDisposable
    {
        private readonly UsersController controller;
        private readonly MachineHubContext dbContext;

        public UsersControllerTests()
        {

            var options = new DbContextOptionsBuilder<MachineHubContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new MachineHubContext(options, new PasswordHasher<User>());

            IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            controller = new UsersController(dbContext, passwordHasher, new TokenGenerator());

            SeedDatabase();
        }
        private void SeedDatabase()
        {

            var user = new User { Id = 1, Name = "Teste", Email = "teste@gmail.com", PasswordHash = "", PhoneNumber = 123123123, Type = "User" };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
