using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.Security.Claims;

namespace LDS_2425.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanListingsController : ControllerBase
    {

        private readonly MachineHubContext dbContext;

        public LoanListingsController(MachineHubContext dbContext) => this.dbContext = dbContext;

        [HttpGet]
        public ActionResult<IEnumerable<Loan_Listing>> GetLoanListings()
        {
            if (dbContext.Loan_Listings == null)
                return NotFound();

            return Ok(dbContext.Loan_Listings.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Loan_Listing> GetLoanListing(int id)
        {
            if (dbContext == null)
                return NotFound();

            var loanListing = dbContext.Loan_Listings.FirstOrDefault(l => l.Id == id);

            if (loanListing == null)
                return NotFound();

            return Ok(loanListing);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Loan_Listing> Add(Loan_Listing loan_Listing)
        {

            if (loan_Listing == null)
                return BadRequest();

            bool categoryExists = dbContext.Categories.Any(c => c.Id == loan_Listing.CategoryId);
            if (!categoryExists)
            {
                return BadRequest($"Category with Id {loan_Listing.CategoryId} doesn't exist.");
            }

            if (loan_Listing.YearManufacture > loan_Listing.DateListed)
                return BadRequest("Year of Manufacture has to be before Date listed");



            /*var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User is not authenticated.");

            loan_Listing.OwnerId = int.Parse(userId);*/

            dbContext.Loan_Listings.Add(loan_Listing);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(Add), new { id = loan_Listing.Id }, loan_Listing);
        }

        [Authorize(Roles = "User")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, Loan_Listing loan_Listing)
        {
            if (!loan_Listing.Id.Equals(id))
                return BadRequest();

            dbContext.Loan_Listings.Entry(loan_Listing).State = EntityState.Modified;
            dbContext.SaveChanges();
            return NoContent();
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (dbContext.Loan_Listings == null)
                return NotFound();

            var loanListing = dbContext.Loan_Listings.SingleOrDefault(l => l.Id == id);

            if (loanListing == null)
                return NotFound();

            dbContext.Loan_Listings.Remove(loanListing);
            dbContext.SaveChanges();
            return NoContent();
        }

        [HttpGet("OwnerId/{ownerId}")]
        public ActionResult<IEnumerable<Loan_Listing>> GetLoanListingByOwner(int ownerId)
        {
            if (dbContext == null)
                return NotFound();

            var loanListing = dbContext.Loan_Listings.Where(l => l.OwnerId == ownerId);

            if (loanListing == null)
                return NotFound();

            return Ok(loanListing);
        }

        [HttpPut("LoanMachine/{id}")]
        public IActionResult LoanMachine(int id, Loan_Listing loan_Listing)
        {
            if (!loan_Listing.Id.Equals(id))
                return BadRequest();

            var loanMachine = dbContext.Loan_Listings.Find(id);

            if (loanMachine == null)
                return NotFound();

            bool machineDate = dbContext.Loan_Listings
                .Any(m => m.Id == id &&
                         m.StartDate >= loan_Listing.DateListed);
            if (machineDate)
                return BadRequest("Start date needs to be after listed date");

            bool isMachineInUse = dbContext.Loan_Listings
                .Any(m => m.Id == id &&
                          m.StartDate <= loan_Listing.EndDate &&
                          m.EndDate >= loan_Listing.StartDate);

            if (isMachineInUse)
                return BadRequest("The machine is already in use during that date");


            loanMachine.Id = loanMachine.Id;
            loanMachine.Name = loanMachine.Name;
            loanMachine.Brand = loanMachine.Brand;
            loanMachine.Model = loanMachine.Model;
            loanMachine.Capacity = loanMachine.Capacity;
            loanMachine.Description = loanMachine.Description;
            loanMachine.YearManufacture = loanMachine.YearManufacture;
            loanMachine.Price = loanMachine.Price;
            loanMachine.Image = loanMachine.Image;
            loanMachine.DateListed = loanMachine.DateListed;
            loanMachine.OwnerId = loanMachine.OwnerId;

            loanMachine.StartDate = loan_Listing.StartDate;
            loanMachine.EndDate = loan_Listing.EndDate;
            loanMachine.TransportNecessary = loan_Listing.TransportNecessary;
            loanMachine.WorkerAvailable = loan_Listing.WorkerAvailable;
            loanMachine.UserId = loan_Listing.UserId;

            /*var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User is not authenticated.");

            loanMachine.UserId = int.Parse(userId);*/

            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
