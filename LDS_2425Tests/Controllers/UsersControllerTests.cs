using LDS_2425;
using LDS_2425.Controllers;
using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Xunit;
using Assert = Xunit.Assert;

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
            IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            var user = new User { Id = 1, Name = "Teste", Email = "teste@gmail.com", PasswordHash = "", PhoneNumber = 123123123, Type = "User" };
            String password = "123";
            user.PasswordHash = passwordHasher.HashPassword(user, password);

            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public void Login_ShouldReturnOk_WhenCredentialsAreValid()
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = "teste@gmail.com", Password = "123" };

            // Act
            var result = controller.Login(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = "teste2@gmail.com", Password = "123" };

            // Act
            var result = controller.Login(loginRequest);

            // Assert
            var conflictResult = Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnOk_WhenThereAreUsers()
        {
            // Act
            var result = await controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var jsonArray = JArray.FromObject(okResult.Value);

            Assert.NotEmpty(jsonArray);
        }

        [Fact]
        public async Task GetUser_ShouldReturnOk_WhenThereisTheSearchedUser()
        {
            //Arrange
            var id = 1;

            // Act
            var result = controller.GetUser(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var jsonResult = JObject.FromObject(okResult.Value);
            Assert.NotNull(jsonResult);
        }

        [Fact]
        public async Task GetUser_ShouldReturnNotFound_WhenThereisNotTheSearchedUser()
        {
            //Arrange
            var id = 10;

            // Act
            var result = controller.GetUser(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Add_ShouldReturnOk_WhenTheUserIsValid()
        {
            //Arrange
            var user = new User { Id = 2, Name = "Teste2", Email = "teste2@gmail.com", PasswordHash = "", PhoneNumber = 123456789, Type = "User" };
            var password = "123";

            // Act
            var result = controller.Add(user, password);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task Add_ShouldReturnConflictObject_WhenThereisUserWithSameEmail()
        {
            //Arrange
            var user = new User { Id = 2, Name = "Teste2", Email = "teste@gmail.com", PasswordHash = "", PhoneNumber = 123456789, Type = "User" };
            var password = "123";

            // Act
            var result = controller.Add(user, password);

            // Assert
            Assert.IsType<ConflictObjectResult>(result.Result);
        }

        [Fact]
        public async Task Add_ShouldReturnConflictObject_WhenThereisUserWithSamePhoneNumber()
        {
            //Arrange
            var user = new User { Id = 2, Name = "Teste2", Email = "teste2@gmail.com", PasswordHash = "", PhoneNumber = 123123123, Type = "User" };
            var password = "123";

            // Act
            var result = controller.Add(user, password);

            // Assert
            Assert.IsType<ConflictObjectResult>(result.Result);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenTheIdAndUserIdAreDifferent()
        {
            //Arrange
            var id = 1;
            var user = new User { Id = 2, Name = "Teste2", Email = "teste@gmail.com", PasswordHash = "", PhoneNumber = 123456789, Type = "User" };

            // Act
            var result = controller.Update(id, user);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ShouldReturnObjectNotFound_WhenTheIdDoesNotExist()
        {
            //Arrange
            var id = 2;
            var user = new User { Id = 2, Name = "Teste2", Email = "teste@gmail.com", PasswordHash = "", PhoneNumber = 123456789, Type = "User" };

            // Act
            var result = controller.Update(id, user);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenTheIdAndUserIsValid()
        {
            //Arrange
            var id = 1;
            var existingUser = dbContext.Users.Find(id);

            // Act
            existingUser.Name = "Teste2";

            var result = controller.Update(existingUser.Id, existingUser);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ShouldReturnConflictObjectResult_WhenThereisUserWithSameEmail()
        {
            //Arrange
            var id = 1;
            var existingUser = dbContext.Users.Find(id);
            var user = new User { Id = 2, Name = "Teste2", Email = "teste2@gmail.com", PasswordHash = "", PhoneNumber = 123456789, Type = "User" };

            // Act
            controller.Add(user, "123");

            existingUser.Email = "teste2@gmail.com";

            var result = controller.Update(existingUser.Id, existingUser);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task Update_ShouldReturnConflictObjectResult_WhenThereisUserWithSamePhoneNumber()
        {
            //Arrange
            var id = 1;
            var existingUser = dbContext.Users.Find(id);
            var user = new User { Id = 2, Name = "Teste2", Email = "teste2@gmail.com", PasswordHash = "", PhoneNumber = 123456789, Type = "User" };

            // Act
            controller.Add(user, "123");

            existingUser.PhoneNumber = 123456789;

            var result = controller.Update(existingUser.Id, existingUser);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenTheUserExists()
        {
            //Arrange
            var id = 2;
            var user = new User { Id = id, Name = "Teste2", Email = "teste2@gmail.com", PasswordHash = "", PhoneNumber = 123456789, Type = "User" };
            dbContext.Add(user);
            dbContext.SaveChanges();

            //Act
            var result = controller.Delete(id);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFoundObject_WhenTheUserDoesNotExist()
        {
            //Act
            var result = controller.Delete(9999);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
