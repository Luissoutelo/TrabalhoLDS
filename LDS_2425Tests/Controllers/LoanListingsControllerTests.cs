
using LDS_2425.Controllers;
using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static System.Net.Mime.MediaTypeNames;
using Assert = Xunit.Assert;

namespace LDS_2425Tests.Controllers
{
    public class LoanListingsControllerTests : IDisposable
    {

        private readonly LoanListingsController _loanListingController;
        private readonly MachineHubContext _dbContext;

        public LoanListingsControllerTests()
        {
            var dbContext = new DbContextOptionsBuilder<MachineHubContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new MachineHubContext(dbContext, new PasswordHasher<User>());
            _loanListingController = new LoanListingsController(_dbContext);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var loanListing = new Loan_Listing
            {
                Id = 1,
                Name = "Bulldozer X200",
                Brand = "Brand X",
                Model = "X200",
                Condition = "Good",
                Capacity = 5000,
                Description = "A reliable bulldozer suitable for heavy construction work.",
                YearManufacture = DateOnly.Parse("2020-05-15"),
                Price = 25000.00f,
                Image = "path/to/image.jpg",
                DateListed = DateOnly.Parse("2024-11-01"),
                StartDate = DateOnly.Parse("2024-11-05"),
                EndDate = DateOnly.Parse("2024-12-05"),
                WorkerAvailable = true,
                TransportNecessary = false,
                CategoryId = 3,
                OwnerId = 1,
                UserId = 1
            };

            _dbContext.Loan_Listings.Add(loanListing);
            _dbContext.Categories.Add(new Category { Id = 1, Name = "TestCategory" });
            _dbContext.SaveChanges();
        }

        [Fact]
        public void GetLoanListings_ReturnsOkResult_WithListOfLoanListings()
        {
            var result = _loanListingController.GetLoanListings();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Loan_Listing>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void GetLoanListings_ReturnNotFound_WhenIdDoesNotExist()
        {
            var result = _loanListingController.GetLoanListing(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Add_ReturnsCreatedAtActionResult_WhenValidLoanListingIsProvided()
        {
            var newLoanListing = new Loan_Listing
            {
                Name = "New Machine",
                Brand = "New Brand",
                Model = "New Model",
                Condition = "New",
                Capacity = 500,
                Description = "New Machine for testing",
                YearManufacture = DateOnly.Parse("2010-10-10"),
                Price = 15000,
                Image = "",
                DateListed = DateOnly.Parse("2024-11-04"),
                WorkerAvailable = true,
                TransportNecessary = true,
                CategoryId = 1,
                OwnerId = 1
            };

            var result = _loanListingController.Add(newLoanListing);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var loanListing = Assert.IsType<Loan_Listing>(createdResult.Value);
            Assert.Equal("New Machine", loanListing.Name);
        }

        [Fact]
        public void Update_ReturnsNoContent_WhenValidLoanListingIsProvided()
        {
            var existingLoanListing = _dbContext.Loan_Listings.Find(1);

            existingLoanListing.Name = "New Machine";
            existingLoanListing.Brand = "New Brand";
            existingLoanListing.Model = "New Model";
            existingLoanListing.Condition = "New";
            existingLoanListing.Capacity = 500;
            existingLoanListing.Description = "New Machine for testing";
            existingLoanListing.YearManufacture = DateOnly.Parse("2010-10-10");
            existingLoanListing.Price = 15000;
            existingLoanListing.Image = "";
            existingLoanListing.DateListed = DateOnly.Parse("2024-11-04");
            existingLoanListing.WorkerAvailable = true;
            existingLoanListing.TransportNecessary = true;
            existingLoanListing.CategoryId = 1;
            existingLoanListing.OwnerId = 1;

            var result = _loanListingController.Update(existingLoanListing.Id, existingLoanListing);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNoContent_WhenLoanListingIsDeleted()
        {
            var result = _loanListingController.Delete(1);

            Assert.IsType<NoContentResult>(result);
            Assert.Null(_dbContext.Loan_Listings.Find(1));
        }

        [Fact]
        public void LoanMachine_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var loanListing = new Loan_Listing
            {
                Id = 99999,
                Name = "Bulldozer X200",
                Brand = "Brand X",
                Model = "X200",
                Condition = "Good",
                Capacity = 5000,
                Description = "A reliable bulldozer suitable for heavy construction work.",
                YearManufacture = DateOnly.Parse("2020-05-15"),
                Price = 25000.00f,
                Image = "path/to/image.jpg",
                DateListed = DateOnly.Parse("2024-11-01"),
                StartDate = DateOnly.Parse("2024-11-05"),
                EndDate = DateOnly.Parse("2024-12-05"),
                WorkerAvailable = true,
                TransportNecessary = false,
                CategoryId = 3,
                OwnerId = 1,
                UserId = 1
            };
            var result = _loanListingController.LoanMachine(1, loanListing);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void LoanMachine_ReturnsNotFound_WhenMachineDoesNotExist()
        {
            var loanListing = new Loan_Listing
            {
                Id = 99,
                Name = "Bulldozer X200",
                Brand = "Brand X",
                Model = "X200",
                Condition = "Good",
                Capacity = 5000,
                Description = "A reliable bulldozer suitable for heavy construction work.",
                YearManufacture = DateOnly.Parse("2020-05-15"),
                Price = 25000.00f,
                Image = "path/to/image.jpg",
                DateListed = DateOnly.Parse("2024-11-01"),
                StartDate = DateOnly.Parse("2024-11-05"),
                EndDate = DateOnly.Parse("2024-12-05"),
                WorkerAvailable = true,
                TransportNecessary = false,
                CategoryId = 3,
                OwnerId = 1,
                UserId = 1
            };

            var result = _loanListingController.LoanMachine(99, loanListing);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void LoanMachine_ReturnsBadRequest_WhenStartDateIsBeforeDateListed()
        {
            var loanListing = new Loan_Listing
            {
                Id = 1,
                Name = "Bulldozer X200",
                Brand = "Brand X",
                Model = "X200",
                Condition = "Good",
                Capacity = 5000,
                Description = "A reliable bulldozer suitable for heavy construction work.",
                YearManufacture = DateOnly.Parse("2020-05-15"),
                Price = 25000.00f,
                Image = "path/to/image.jpg",
                DateListed = DateOnly.Parse("2024-11-01"),
                StartDate = DateOnly.Parse("2024-10-05"),//Start date before DateListed
                EndDate = DateOnly.Parse("2024-12-05"),
                WorkerAvailable = true,
                TransportNecessary = false,
                CategoryId = 3,
                OwnerId = 1,
                UserId = 1
            };

            var result = _loanListingController.LoanMachine(1, loanListing);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Start date needs to be after listed date", badRequestResult.Value);
        }

        [Fact]
        public void LoanMachine_ReturnsBadRequest_WhenMachineIsInUseDuringDates()
        {
            var loanListing = new Loan_Listing
            {
                Id = 1,
                Name = "Bulldozer X200",
                Brand = "Brand X",
                Model = "X200",
                Condition = "Good",
                Capacity = 5000,
                Description = "A reliable bulldozer suitable for heavy construction work.",
                YearManufacture = DateOnly.Parse("2020-05-15"),
                Price = 25000.00f,
                Image = "path/to/image.jpg",
                DateListed = DateOnly.Parse("2024-11-01"),
                StartDate = DateOnly.Parse("2024-11-05"),
                EndDate = DateOnly.Parse("2024-11-12"),
                WorkerAvailable = true,
                TransportNecessary = false,
                CategoryId = 3,
                OwnerId = 1,
                UserId = 1
            };

            var result = _loanListingController.LoanMachine(1, loanListing);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Start date needs to be after listed date", badRequestResult.Value);
        }

        [Fact]
        public void LoanMachine_ReturnsNoContent_WhenLoanDatesAreValid()
        {
            var existingLoanListing = _dbContext.Loan_Listings.Find(1);

            existingLoanListing.Name = "Bulldozer X200";
            existingLoanListing.Brand = "Brand X";
            existingLoanListing.Model = "X200";
            existingLoanListing.Condition = "Good";
            existingLoanListing.Capacity = 5000;
            existingLoanListing.Description = "A reliable bulldozer suitable for heavy";
            existingLoanListing.YearManufacture = DateOnly.Parse("2020-05-15");
            existingLoanListing.Price = 25000.00f;
            existingLoanListing.Image = "";
            existingLoanListing.DateListed = DateOnly.Parse("2022-11-01");
            existingLoanListing.StartDate = DateOnly.Parse("2024-12-15");
            existingLoanListing.EndDate = DateOnly.Parse("2024-12-22");
            existingLoanListing.WorkerAvailable = true;
            existingLoanListing.TransportNecessary = true;
            existingLoanListing.CategoryId = 3;
            existingLoanListing.OwnerId = 1;
            existingLoanListing.UserId = 2;

            var result = _loanListingController.Update(existingLoanListing.Id, existingLoanListing);

            Assert.IsType<NoContentResult>(result);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}