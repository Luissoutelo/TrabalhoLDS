using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Identity;
using Assert = Xunit.Assert;
using Newtonsoft.Json.Linq;

namespace LDS_2425.Controllers.Tests
{
    public class ShoppingCartControllerTests : IDisposable
    {
        private readonly ShoppingCartController controller;
        private readonly MachineHubContext dbContext;

        public ShoppingCartControllerTests()
        {

            var options = new DbContextOptionsBuilder<MachineHubContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            dbContext = new MachineHubContext(options, new PasswordHasher<User>());
            controller = new ShoppingCartController(dbContext);


            SeedDatabase();
        }

        private void SeedDatabase()
        {

            var shoppingCart = new ShoppingCart { userId = 123 };
            dbContext.ShoppingCarts.Add(shoppingCart);
            dbContext.SaveChanges();
            dbContext.SaveChanges();
        }
        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetMachineFromCart_ShouldReturnNotFound_WhenShoppingCartDoesNotExist()
        {
            // Arrange
            int testUserId = 999;

            // Act
            var result = await controller.GetMachineFromCart(testUserId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetMachineFromCart_ShouldReturnEmptyMessage_WhenCartIsEmpty()
        {
            
            var userId = 123; 

            
            var result = await controller.GetMachineFromCart(userId);

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult); 

            
            var jsonResult = JObject.FromObject(okResult.Value);
            Assert.Equal("The shopping cart is empty.", jsonResult["Message"].ToString()); 
        }




        [Fact]
        public async Task AddMachineToCart_ShouldReturnNotFound_WhenShoppingCartDoesNotExist()
        {
            // Arrange
            int testUserId = 999;
            var request = new AddMachineToCartRequest { MachineId = 1000 };

            // Act
            var result = await controller.AddMachineToCart(testUserId, request);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task AddMachineToCart_ShouldAddMachineToCart_WhenCartExists()
        {
            var machineForSale = new Machine
            {
                Id = 1000,
                Name = "Excavator Model Y",
                Brand = "Brand B",
                Model = "Model Y",
                Condition = "New",
                Capacity = 7000,
                Description = "A powerful excavator for construction sites.",
                Year_of_Manufacture = new DateOnly(2023, 1, 1),
                Price = 35000.00f,
                Image = "path/to/image2.jpg",
                CategoryId = 1,
            };
            // Arrange
            var request = new AddMachineToCartRequest { MachineId = 1000 }; 

            // Act
            var result = await controller.AddMachineToCart(123, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult); 
            var jsonResult = JObject.FromObject(okResult.Value); 

            Assert.Equal("Machine added to cart.", jsonResult["message"].ToString()); 

            var shoppingCart = await dbContext.ShoppingCarts
                .Include(sc => sc.Machines)
                .FirstOrDefaultAsync(c => c.userId == 123);
            Assert.NotNull(shoppingCart);
            Assert.Single(shoppingCart.Machines);
        }


        [Fact]
        public async Task RemoveMachineFromCart_ShouldReturnNotFound_WhenShoppingCartDoesNotExist()
        {

            int testUserId = 999;
            int machineId = 1;


            var result = await controller.RemoveMachineFromCart(testUserId, machineId);


            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task RemoveMachineFromCart_ShouldReturnNotFound_WhenMachineDoesNotExistInCart()
        {

            int machineId = 100000000;


            var result = await controller.RemoveMachineFromCart(123, machineId);


            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task RemoveMachineFromCart_ShouldRemoveMachineFromCart_WhenMachineExists()
        {
            // Arrange
            var machineForSale = new Machine
            {
                Id = 1000,
                Name = "Excavator Model Y",
                Brand = "Brand B",
                Model = "Model Y",
                Condition = "New",
                Capacity = 7000,
                Description = "A powerful excavator for construction sites.",
                Year_of_Manufacture = new DateOnly(2023, 1, 1),
                Price = 35000.00f,
                Image = "path/to/image2.jpg",
                CategoryId = 1,
            };

            
            var request = new AddMachineToCartRequest { MachineId = machineForSale.Id };
            await controller.AddMachineToCart(123, request);

            
            var result = await controller.RemoveMachineFromCart(123, machineForSale.Id); 

            
            var okResult = Assert.IsType<OkObjectResult>(result);

            
            var jsonResult = JObject.FromObject(okResult.Value);
            Assert.Equal("Machine deleted from cart.", jsonResult["message"].ToString());

            var shoppingCart = await dbContext.ShoppingCarts
                .Include(sc => sc.Machines)
                .FirstOrDefaultAsync(c => c.userId == 123);
            Assert.NotNull(shoppingCart);
            Assert.Empty(shoppingCart.Machines); 
        }


    }
}
