using LDS_2425.Controllers;
using LDS_2425.Data;
using LDS_2425.Migrations;
using LDS_2425.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ReceiptControllerTest
{
    [TestClass]
    public class ReceiptControllerTest
    {

        private ReceiptsController _receipts;
        private MachineHubContext _machineHubContext;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MachineHubContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options; // Usar a BD em memoria


            _machineHubContext = new MachineHubContext(options, new PasswordHasher<User>());
            _receipts = new ReceiptsController(_machineHubContext);
        }


        [TestMethod]
        public void GetReceipts_ShouldReturnAllReceiptsTest()
        {
            //// Arrange
            _machineHubContext.Receipts.Add(new Receipt 
            { 
                Id = 1,
                CompanyName = "MachineHub",
                CompanyEmail  = "MachineHubContext@email.com",
                CompanyPhone = 918171615,
                CompanyNif = 123456789,
                DateReceipt = DateTime.Parse("2020-10-10"),
                OwnerId=2,
                UserId=3,
                Loan_ListingId=7,
                MachineId=1,
                ContractId=1 
            }
            );
            _machineHubContext.Receipts.Add(new Receipt
            {
                Id = 2,
                CompanyName = "MachineHub",
                CompanyEmail = "MachineHubContext@email.com",
                CompanyPhone = 918171615,
                CompanyNif = 123456789,
                DateReceipt = DateTime.Parse("2020-10-10"),
                OwnerId = 2,
                UserId = 3,
                Loan_ListingId = 14,
                MachineId = 3,
                ContractId = 2
            }
            ); _machineHubContext.SaveChanges();

            //// Act
            var result = _receipts.GetReceipts();

            //// Assert
            var okResult = result.Result as OkObjectResult;
            var receipts = okResult.Value as List<Receipt>;
            Assert.AreEqual(2, receipts.Count);
        }

        
        [TestMethod]
        public void GetReceiptssById_ExistTest()
        {

            ////Arrange
            var receipt = new Receipt
            {
                Id = 2,
                CompanyName = "MachineHub",
                CompanyEmail = "MachineHubContext@email.com",
                CompanyPhone = 918171615,
                CompanyNif = 123456789,
                DateReceipt = DateTime.Parse("2020-10-10"),
                OwnerId = 2,
                UserId = 3,
                Loan_ListingId = 14,
                MachineId = 3,
                ContractId = 2
            };
            _machineHubContext.Receipts.Add(receipt);
            _machineHubContext.SaveChanges();

            ////Act
            var result = _receipts.GetReceipts(2);

            ////Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var tempReceipt = okResult.Value as Receipt;
            Assert.IsNotNull(tempReceipt);
        }


        [TestMethod]
        public void GetReceiptsById_NotExistTest()
        {
            ////Arrange
            var IdTest = 6666;

            ////Act
            var result = _receipts.GetReceipts(IdTest);

            ////Assert
            var notFoundResult = result.Result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
        }


        [TestMethod]
        public void AddReceipts_InvalidContractTest()
        {
            ////Arrange
            var newReceipt = new Receipt
            {
                Id = 6666,
                CompanyName = "MachineHub",
                CompanyEmail = "MachineHubContext@email.com",
                CompanyPhone = 918171615,
                CompanyNif = 123456789,
                DateReceipt = DateTime.Parse("2020-10-10"),
                OwnerId = 2,
                UserId = 3,
                Loan_ListingId = 0,
                MachineId = 0,
                ContractId = 2
            };


            ////Act
            var result = _receipts.Add(newReceipt);

            ////Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
        }


        [TestMethod]
        public void AddReceipts_ValidContractTest()
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

            var contract = new Contract { Id = 1, ListingId = 7 };
            _machineHubContext.Contracts.Add(contract);

            // Salvar as dependências
            _machineHubContext.SaveChanges();

            var newReceipt = new Receipt
            {
                CompanyName = "MachineHub",
                CompanyEmail = "MachineHubContext@email.com",
                CompanyPhone = 918171615,
                CompanyNif = 123456789,
                DateReceipt = DateTime.Parse("2020-10-10"),
                OwnerId = 2,
                UserId = 3,
                Loan_ListingId = 7,
                MachineId = 3,
                ContractId = 1
            };



            // Act
            var result = _receipts.Add(newReceipt);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
           Assert.IsNotNull(createdResult);
            var savedReceipt = createdResult.Value as Receipt;
            Assert.IsNotNull(savedReceipt);
            Assert.AreEqual(newReceipt.CompanyName, savedReceipt.CompanyName);

        }

    }
}
