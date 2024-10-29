using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDS_2425.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class ContractsController : ControllerBase
	{

		private readonly MachineHubContext dbContext;

		public ContractsController(MachineHubContext dbContext) => this.dbContext = dbContext;


		//GET: api/Contracts
		[HttpGet]
		public ActionResult<IEnumerable<Contract>> GetContracts()
		{
			if (dbContext.Contracts == null)
				return NotFound("No contracts found");

			return Ok(dbContext.Contracts.ToList());
		}



		//GET: api/contracts/{id}
		[HttpGet("{id}")]
		public ActionResult<Contract> GetContracts(int id)
		{
            if (dbContext.Contracts == null)
                return NotFound();

			var contract = dbContext.Contracts.SingleOrDefault(s => s.Id == id);

			if (contract == null)
				return NotFound($"Contract with ID {id} not found.");

			return Ok(contract);
		}

		//PUT: api/contracts
		[HttpPost]

		public ActionResult<Contract> Add(Contract contract)
		{
			if (contract == null)
			{
				return BadRequest("Contrato nao pode ser nulo.");
            }

			////Validadte ListingId and ReceipId existence
			//if (!dbContext.Loan_Listings.Any(1 => 1.Id == contract.ListingId) ||
   //             (!dbContext.Receipts.Any(r => r.Id == contract.ReceiptId))
			//	{
			//	return BadRequest("Listing or Receipt ID invalid");
			//}

   //         // Check if receipt validation passes
   //         if (!dbContext.ValidateReceipt(contract.Receipt))
   //         {
   //             return BadRequest("Receipt does not meet the validation requirements.");
   //         }

            dbContext.Contracts.Add(contract);
			dbContext.SaveChanges();
			return CreatedAtAction(nameof(Add), new { id = contract.Id }, contract);
		}

		//Put: api/contracts/{id}
		[HttpPut("{id}")]
		public IActionResult Update(int id, Contract contract)
		{
			if (!contract.Id.Equals(id))
				return BadRequest("Contract ID mismatch or null data.");

			if (!dbContext.Contracts.Any(c => c.Id == id))
				return NotFound($"Contract with ID {id} not found.");

			dbContext.Contracts.Update(contract);
			dbContext.SaveChanges();
			return NoContent();
		}

		//Delete: api/students/{id}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			 if (dbContext.Contracts == null)
				return NotFound();

			var contract = dbContext.Contracts.SingleOrDefault(s => s.Id == id);

			if (contract == null)
				return NotFound($"Contract with ID {id} not found");

			dbContext.Contracts.Remove(contract);
			dbContext.SaveChanges();
			return NoContent();
		}
	}
}
