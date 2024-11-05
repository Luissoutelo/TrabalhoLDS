using LDS_2425.Controllers;
using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDS_2425Tests.Controllers


{
    [TestClass]
    public class ContractsControllerTests
    {

        private ContractsController _contracts;
        private MachineHubContext _machineHubContext;


        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MachineHubContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options; // Usar a BD em memoria


            _machineHubContext = new MachineHubContext(options, new PasswordHasher<User>());
            _contracts = new ContractsController(_machineHubContext);
        }


        [TestMethod]
        public void GetContracts_ShouldReturnAllContractsTest()
        {
            // Arrange
            _machineHubContext.Contracts.Add(new Contract { Id = 1, ListingId = 1 });
            _machineHubContext.Contracts.Add(new Contract { Id = 2, ListingId = 2 });
            _machineHubContext.SaveChanges();

            // Act
            var result = _contracts.GetContracts();

            // Assert
            var okResult = result.Result as OkObjectResult;
            var contracts = okResult.Value as List<Contract>;
            Assert.AreEqual(2, contracts.Count);
        }


        [TestMethod]
        public void GetContractsById_ExistTest()
        {

            //Arrange
            var contract = new Contract { Id = 1, ListingId = 1 };
            _machineHubContext.Contracts.Add(contract);
            _machineHubContext.SaveChanges();

            //Act
            var result = _contracts.GetContracts(1);

            //Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var tempContract = okResult.Value as Contract;
            Assert.IsNotNull(tempContract);
        }


        [TestMethod]
        public void GetContractsById_NotExistTest()
        {
            //Arrange
            var IdTest = 6666;

            //Act
            var result = _contracts.GetContracts(IdTest);

            //Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
        }


        [TestMethod]
        public void AddContracts_ContractExistsTest()
        {
            //Arrange

            var newContract = new Contract { Id = 1, ListingId = 1 };
            _machineHubContext.Contracts.Add(newContract);
            _machineHubContext.SaveChanges();

            var ContractTmp = new Contract { ListingId = 1 };

            //Act
            var result = _contracts.Add(newContract);

            //Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
        }

        [TestMethod]
        public void AddContracts_ContractNotFoundTest()
        {
            //Arrange
            var newContract = new Contract { ListingId = 666 };

            //Act
            var result = _contracts.Add(newContract);

            //Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
        }


        [TestMethod]
        public void AddContracts_InvalidContractTest()
        {
            //Arrange
            var newContract = new Contract { ListingId = 0 };


            //Act
            var result = _contracts.Add(newContract);

            //Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
        }

        [TestMethod]
        public void AddContracts_ValidContractTest()
        {
            // Arrange
            var existingListing = new Loan_Listing
            {
                Id = 7,
                Name = "MachineLoan1",
                Brand = "Brand1",
                Model = "Model1",
                Condition = "Good",
                Capacity = 1000,
                Description = "Good Condition",
                YearManufacture = DateOnly.Parse("2020-10-10"),
                Price = 3000,
                Image = "",
                DateListed = DateOnly.Parse("2024-10-29"),
                StartDate = DateOnly.Parse("2024-11-04"),
                EndDate = DateOnly.Parse("2024-11-20"),
                WorkerAvailable = true,
                TransportNecessary = true,
                CategoryId = 1,
                OwnerId = 1,
                UserId = 3
            };

            _machineHubContext.Loan_Listings.Add(existingListing);
            _machineHubContext.SaveChanges();

            var newContract = new Contract { ListingId = 7 };

            // Act
            var result = _contracts.Add(newContract);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            var savedContract = createdResult.Value as Contract;
            Assert.AreEqual(newContract.ListingId, savedContract.ListingId);
        }

    }
}




