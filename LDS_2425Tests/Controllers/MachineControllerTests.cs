using LDS_2425.Controllers;
using LDS_2425.Data;
using LDS_2425.Models;
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
using static System.Net.Mime.MediaTypeNames;
using Assert = Xunit.Assert;

namespace LDS_2425Tests.Controllers
{
    public class MachineControllerTests : IDisposable
    {
        private readonly MachinesController _machineController;
        private readonly MachineHubContext _dbContext;

        public MachineControllerTests()
        {
            var dbContext = new DbContextOptionsBuilder<MachineHubContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new MachineHubContext(dbContext, new PasswordHasher<User>());
            _machineController = new MachinesController(_dbContext);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var machine = new Machine
            {
                Id = 1,
                Name = "MachineTest",
                Brand = "BrandTest",
                Model = "ModelTest",
                Condition = "Good",
                Capacity = 1000,
                Description = "Good Machine for testing",
                Year_of_Manufacture = DateOnly.Parse("2010-10-10"),
                Price = 12000,
                Image = "",
                CategoryId = 1
            };
            _dbContext.Machines.Add(machine);
            _dbContext.Categories.Add(new Category { Id = 1, Name = "TestCategory" });
            _dbContext.SaveChanges();
        }

        public void Dispose()
        { 
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Fact]
        public void GetMachines_ReturnsOkResult_WithListOfMachines()
        {
            var result = _machineController.GetMachines();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var machines = Assert.IsType<List<Machine>>(okResult.Value);
            Assert.Single(machines);
        }

        [Fact]
        public void GetMachine_ReturnsOkResult_WithMachine()
        {
            var result = _machineController.Getmachine(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var machine = Assert.IsType<Machine>(okResult.Value);
            Assert.Equal(1, machine.Id);
            Assert.Equal("MachineTest", machine.Name);
            Assert.Equal("BrandTest", machine.Brand);
            Assert.Equal("ModelTest", machine.Model);
            Assert.Equal("Good", machine.Condition);
            Assert.Equal(1000, machine.Capacity);
            Assert.Equal("Good Machine for testing", machine.Description);
            Assert.Equal(DateOnly.Parse("2010-10-10"), machine.Year_of_Manufacture);
            Assert.Equal(12000, machine.Price);
            Assert.Equal("", machine.Image);
            Assert.Equal(1, machine.CategoryId);
        }

        [Fact]
        public void GetMachine_ReturnsNotFound_WhenMachineDoesNotExist()
        {
            var result = _machineController.Getmachine(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void AddMachine_ReturnsCreatedResult_WithMachine()
        {
            var newMachine = new Machine
            {
                Id = 3,
                Name = "NewMachine",
                Brand = "NewBrand",
                Model = "NewModel",
                Condition = "Excellent",
                Capacity = 2000,
                Description = "New Machine for testing",
                Year_of_Manufacture = DateOnly.Parse("2015-05-05"),
                Price = 15000,
                Image = "",
                CategoryId = 1
            };

            var result = _machineController.Add(newMachine);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var machine = Assert.IsType<Machine>(createdResult.Value);
            Assert.Equal("NewMachine", machine.Name);
        }

        [Fact]
        public void AddMachine_ReturnsBadRequest_WhenCategoryDoesNotExist()
        {
            var newMachinne = new Machine
            {
                
                Id = 4,
                Name = "InvalidMachine",
                Brand = "NewBrand",
                Model = "NewModel",
                Condition = "Excellent",
                Capacity = 2000,
                Description = "New Machine for testing",
                Year_of_Manufacture = DateOnly.Parse("2015-05-05"),
                Price = 15000,
                Image = "",
                CategoryId = 9999
            };

            var result = _machineController.Add(newMachinne);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Category with Id 9999 doesn't exist.", badRequestResult.Value);
        }

        [Fact]
        public void UpdateMachine_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            var existingMachine = _dbContext.Machines.Find(1);

            existingMachine.Name = "UpdatedMachine";
            existingMachine.Brand = "UpdatedBrand";
            existingMachine.Model = "UpdatedModel";
            existingMachine.Condition = "Like New";
            existingMachine.Capacity = 1200;
            existingMachine.Description = "Updated description";
            existingMachine.Year_of_Manufacture = DateOnly.Parse("2012-12-12");
            existingMachine.Price = 13000;
            existingMachine.Image = "";
            existingMachine.CategoryId = 1;

            var result = _machineController.Update(existingMachine.Id, existingMachine);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateMachine_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var updateMachine = new Machine 
            {
                Id = 9999,
                Name = "UpdatedMachineInvalid",
                Brand = "UpdatedBrandInvalid",
                Model = "UpdatedModelInvalid",
                Condition = "Like New",
                Capacity = 1200,
                Description = "Updated description Invalid",
                Year_of_Manufacture = DateOnly.Parse("2012-12-12"),
                Price = 13000,
                Image = "",
                CategoryId = 1
            };

            var result = _machineController.Update(1, updateMachine);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteMachine_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            var result = _machineController.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteMachine_ReturnsNotFound_WhenMachineDoesNotExist()
        {
            var result = _machineController.Delete(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
