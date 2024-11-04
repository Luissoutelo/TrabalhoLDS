using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Identity;
using Assert = Xunit.Assert;
using Newtonsoft.Json.Linq;
using System.Reflection.PortableExecutable;
using Machine = LDS_2425.Models.Machine;

namespace LDS_2425.Controllers.Tests
{
    public class FavoritesPageControllerTests : IDisposable
    {
        private readonly FavoritePageController controller;
        private readonly MachineHubContext dbContext;

        public FavoritesPageControllerTests()
        {
            var options = new DbContextOptionsBuilder<MachineHubContext>()
                .UseInMemoryDatabase("TestFavoritesDatabase")
                .Options;

            dbContext = new MachineHubContext(options, new PasswordHasher<User>());
            controller = new FavoritePageController(dbContext);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var favoritesPage = new FavoritesPage { Id = 999, UserId = 999 };
            dbContext.FavoritesPages.Add(favoritesPage);
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetFavorites_ShouldReturnNotFound_WhenFavoritesPageDoesNotExist()
        {
            // Arrange
            int testFavouriteId = 99999;

            // Act
            var result = await controller.GetFavorites(testFavouriteId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetFavorites_ShouldReturnFavoritePage_WhenFavoritesPageDoesExist()
        {
            // Arrange
            int testFavouriteId = 999;
            var loanListing = CreateLoanListing();
            var machine = AddMachineAsync();
            var request = new AddMachineToCartRequest { MachineId = machine.Id };
            var requestLLoanList = new AddMachineToCartRequest { MachineId = loanListing.Id };

            var addLoanMachine= await controller.AddToFavoritesLoanListing(testFavouriteId,requestLLoanList);
            var addMachine = await controller.AddToFavoritesMachine(testFavouriteId, request);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await controller.GetFavorites(testFavouriteId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var jsonResult = JObject.FromObject(okResult.Value);
            Assert.NotNull(jsonResult);


        }


        [Fact]
        public async Task AddToFavoritesMachine_ShouldReturnNotFound_WhenFavoritesPageDoesNotExist()
        {
            var favouritePageNotExists = 19129;
            var machine = AddMachineAsync();
            

            var request = new AddMachineToCartRequest { MachineId = machine.Id };

            
            var result = await controller.AddToFavoritesMachine(favouritePageNotExists, request);

            
            Assert.IsType<NotFoundObjectResult>(result);
        }
        
        [Fact]
        public async Task AddToFavoritesLoanMachine_ShouldReturnNotFound_WhenFavoritesPageDoesNotExist()
        {
            var favouritePageNotExists = 19129;

            var loanListing = CreateLoanListing();


            var request = new AddMachineToCartRequest { MachineId = loanListing.Id };


            var result = await controller.AddToFavoritesLoanListing(favouritePageNotExists, request);


            Assert.IsType<NotFoundObjectResult>(result);
        }
        public async Task AddToFavoritesLoanMachine_ShouldReturnNotFound_WhenLoanMachineDoesNotExist()
        {
            int favouritePageValid = 999;
            int machineNotExists = 2321;


            var request = new AddMachineToCartRequest { MachineId = machineNotExists };


            var result = await controller.AddToFavoritesLoanListing(favouritePageValid,request);


            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task AddMachineToFavouritePage_ShouldAddMachineToCart_WhenCartAndMachineExists()
        {
            var loanListing = CreateLoanListing();


            var request = new AddMachineToCartRequest { MachineId = loanListing.Id };

            
            var result = await controller.AddToFavoritesMachine(999, request);

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            var jsonResult = JObject.FromObject(okResult.Value);

            Assert.Equal("Machine added to favorites successfully.", jsonResult["Message"].ToString());

            var favoritesPage = await dbContext.FavoritesPageMachines
                .FirstOrDefaultAsync(fp=>fp.FavoritesPageId==999 && fp.MachineId==loanListing.Id);
            Assert.NotNull(favoritesPage);
           
        }
        [Fact]
        public async Task AddLoanMachineToFavouritePage_ShouldAddMachineToCart_WhenCartAndLoanMachineExists()
        {
            var loanListing = CreateLoanListing();

            var request = new AddMachineToCartRequest { MachineId = loanListing.Id };


            var result = await controller.AddToFavoritesLoanListing(999, request);


            var favoutePageExits = 999;

            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            var jsonResult = JObject.FromObject(okResult.Value);

            Assert.Equal("Machine added to favorites successfully.", jsonResult["Message"].ToString());

            var favoritesPage = await dbContext.FavoritesPageLoan_Listings
                .FirstOrDefaultAsync(fp => fp.FavoritesPageId == 999 && fp.Loan_ListingId == loanListing.Id);
            Assert.NotNull(favoritesPage);

        }

        [Fact]
        public async Task RemoveFromFavoritesMachine_ShouldReturnNotFound_WhenFavoritesPageDoesNotExist()
        {
            var machine = AddMachineAsync();

            int userThatNotExists = 12321;
            var result = await controller.RemoveFromFavoritesMachine(userThatNotExists, machine.Id);

           
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task RemoveFromFavoritesLoanMachine_ShouldReturnNotFound_WhenFavoritesPageDoesNotExist()
        {
            int FavouritePageNotExists = 45678765;
            var loanListing = CreateLoanListing();


            var result = await controller.RemoveFromFavoritesLoanListing(FavouritePageNotExists, loanListing.Id);

           
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task RemoveFromFavoritesLoanMachine_ShouldReturnNotFound_WhenMachineDoesNotExistInFavorites()
        {
            int testUserIdExists = 999;
            int machineIdNotExists = 100000000;

          
            var result = await controller.RemoveFromFavoritesLoanListing(testUserIdExists,machineIdNotExists);

           
            Assert.IsType<NotFoundObjectResult>(result);
        }


        [Fact]
        public async Task RemoveFromFavoritesMachine_ShouldRemoveMachineFromFavorites_WhenMachineExists()
        {

            int testUserIdExists = 999;
            var machine = AddMachineAsync();


            var request = new AddMachineToCartRequest { MachineId = machine.Id };
            var add = await controller.AddToFavoritesMachine(testUserIdExists, request);
            
            await dbContext.SaveChangesAsync();

            var result = await controller.RemoveFromFavoritesMachine(999, request.MachineId);

      
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            var jsonResult = JObject.FromObject(okResult.Value);


            Assert.Equal("Machine removed from favorites successfully.", jsonResult["Message"].ToString());

            var favoritesPage = await dbContext.FavoritesPageMachines
                .FirstOrDefaultAsync(fp=> fp.Id== testUserIdExists && fp.MachineId == machine.Id);
            Assert.Null(favoritesPage);

        }
        [Fact]
        public async Task RemoveFromFavoritesLoanMachine_ShouldRemoveLoanMachineFromFavorites_WhenMachineExists()
        {
      

            var loanListing = CreateLoanListing();
            int testUserIdExists = 999;
            var request = new AddMachineToCartRequest { MachineId = loanListing.Id };
            var add = await controller.AddToFavoritesMachine(testUserIdExists, request);

            await dbContext.SaveChangesAsync();

            
            var result = await controller.RemoveFromFavoritesMachine(999, request.MachineId);

         
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            var jsonResult = JObject.FromObject(okResult.Value);


            Assert.Equal("Machine removed from favorites successfully.", jsonResult["Message"].ToString());

            var favoritesPage = await dbContext.FavoritesPageLoan_Listings
                .FirstOrDefaultAsync(fp => fp.FavoritesPageId == 999 && fp.Loan_ListingId==999);
            Assert.Null(favoritesPage);

        }
        private async Task<Models.Machine> AddMachineAsync()
        {
            var machine = new Models.Machine
            {
                Id = 999,
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

            dbContext.Machines.Add(machine);
            await dbContext.SaveChangesAsync();

            return machine;
        }
        private async Task<Models.Loan_Listing>  CreateLoanListing()
        {
            var loanList=new Loan_Listing
            {
                Id = 999,
                Name = "Bulldozer X200",
                Brand = "Brand X",
                Model = "X200",
                Condition = "Good",
                Capacity = 5000,
                Description = "A reliable bulldozer suitable for heavy construction work.",
                YearManufacture = new DateOnly(2020, 5, 15),
                Price = 25000.00f,
                Image = "path/to/image.jpg",
                DateListed = new DateOnly(2024, 11, 1),
                StartDate = new DateOnly(2024, 11, 5),
                EndDate = new DateOnly(2024, 12, 5),
                WorkerAvailable = true,
                TransportNecessary = false,
                CategoryId = 3,
                OwnerId = 999,
                UserId = 999
            };
            dbContext.Loan_Listings.Add(loanList);
            await dbContext.SaveChangesAsync();
            return loanList;
        }

    }
}
