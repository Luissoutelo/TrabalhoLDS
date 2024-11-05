using LDS_2425.Controllers;
using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace LDS_2425Tests.Controllers
{
    public class CategoriesControllerTests : IDisposable
    {
        private readonly CategoriesController controller;
        private readonly MachineHubContext dbContext;

        public CategoriesControllerTests()
        {

            var options = new DbContextOptionsBuilder<MachineHubContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            dbContext = new MachineHubContext(options, new PasswordHasher<User>());
            controller = new CategoriesController(dbContext);

            SeedDatabase();
        }
        private void SeedDatabase()
        {

            var category = new Category { Id = 1, Name = "Teste" };
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetCategory_ShouldReturnOk_WhenThereAreCategories()
        {
            // Act
            var result = await controller.GetCategory();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var jsonArray = JArray.FromObject(okResult.Value);

            Assert.NotEmpty(jsonArray);
        }

        [Fact]
        public async Task GetCategory_ShouldReturnOk_WhenThereisTheSearchedCategory()
        {
            //Arrange
            var id = 1;

            // Act
            var result = controller.GetCategory(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var jsonResult = JObject.FromObject(okResult.Value);
            Assert.NotNull(jsonResult);
        }

        [Fact]
        public async Task GetCategory_ShouldReturnNotFound_WhenThereisNotTheSearchedCategory()
        {
            //Arrange
            var id = 10;

            // Act
            var result = controller.GetCategory(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Add_ShouldReturnOk_WhenTheCategoryIsValid()
        {
            //Arrange
            var category = new Category { Id = 2, Name = "Teste2" };

            // Act
            var result = controller.Add(category);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task Add_ShouldReturnConflictObject_WhenThereisCategoryWithSameName()
        {
            //Arrange
            var category = new Category { Id = 2, Name = "Teste" };

            // Act
            var result = controller.Add(category);

            // Assert
            Assert.IsType<ConflictObjectResult>(result.Result);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenTheIdAndCategoryidAreDifferent()
        {
            //Arrange
            var id = 1;
            var category = new Category { Id = 2, Name = "Teste2" };

            // Act
            var result = controller.Update(id, category);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ShouldReturnObjectNotFound_WhenTheIdDoesNotExist()
        {
            //Arrange
            var id = 2;
            var category = new Category { Id = 2, Name = "Teste2" };

            // Act
            var result = controller.Update(id, category);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenTheIdAndCategoryIsValid()
        {
            //Arrange
            var id = 1;
            var existingCategory = dbContext.Categories.Find(id);

            // Act
            existingCategory.Name = "Teste2";

            var result = controller.Update(existingCategory.Id, existingCategory);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ShouldReturnConflictObjectResult_WhenThereisCategoryWithSameName()
        {
            //Arrange
            var id = 1;
            var existingCategory = dbContext.Categories.Find(id);
            var newCategory = new Category { Id = 2, Name = "Teste2" };

            // Act
            controller.Add(newCategory);

            existingCategory.Name = "Teste2";

            var result = controller.Update(existingCategory.Id, existingCategory);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenTheCategoryExists()
        {
            // Arrange
            var category = new Category { Id = 2, Name = "Teste2" };
            var addResult = controller.Add(category);

            //Act
            var result = controller.Delete(2);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFoundObject_WhenTheCategoryIdIsInvalid()
        {
            //Act
            var result = controller.Delete(2);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetMachineCountForCategory_ShouldReturnOk_WhenCategoryExists()
        {
            // Act
            var result = await controller.GetMachineCountForCategory(1);

            // Assert
            Assert.IsType<ActionResult<int>>(result);

            var okResult = result.Result as OkObjectResult;

            Assert.NotNull(okResult);
            var machineCount = (int)okResult!.Value!;

            Assert.True(machineCount >= 0);
        }



        [Fact]
        public async Task GetMachineCountForCategory_ShouldReturnNotFound_WhenTheCategoryIdIsInvalid()
        {
            // Act
            var result = await controller.GetMachineCountForCategory(2);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetLoanListingCountForCategory_ShouldReturnOk_WhenCategoryExists()
        {

            //Act
            var result = await controller.GetLoanListingCountForCategory(1);

            //Assert
            Assert.IsType<ActionResult<int>>(result);

            var okResult = result.Result as OkObjectResult;

            Assert.NotNull(okResult);
            var listingCount = (int)okResult!.Value!;

            Assert.True(listingCount >= 0);
        }

        [Fact]
        public async Task GetListingCountForCategory_ShouldReturnNotFound_WhenTheCategoryIdIsInvalid()
        {
            // Act
            var result = await controller.GetLoanListingCountForCategory(2);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
