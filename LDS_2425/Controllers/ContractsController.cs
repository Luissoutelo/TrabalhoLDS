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
				return NotFound();

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
				return NotFound();

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

            dbContext.Contracts.Add(contract);
			dbContext.SaveChanges();
			return CreatedAtAction(nameof(Add), new { id = contract.Id }, contract);
		}

		//Put: api/contracts
		[HttpPut("{id}")]
		public IActionResult Update(int id, Contract contract)
		{
			if (!contract.Id.Equals(id))
				return BadRequest();

			dbContext.Contracts.Entry(contract).State = EntityState.Modified;
			dbContext.SaveChanges();
			return NoContent();
		}

		//Delete: api/students
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			 if (dbContext.Contracts == null)
				return NotFound();

			var contract = dbContext.Contracts.SingleOrDefault(s => s.Id == id);

			if (contract == null)
				return NotFound();

			dbContext.Contracts.Remove(contract);
			dbContext.SaveChanges();
			return NoContent();
		}
	}
}
