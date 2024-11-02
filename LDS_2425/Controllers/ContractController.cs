using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

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
        public ActionResult<IEnumerable<Models.Contract>> GetContracts()
        {
            //Verfica se existe contratos
            if (dbContext.Contracts == null)
                return NotFound("No contracts found");

            //Retorna lista contratos
            return Ok(dbContext.Contracts.ToList());
        }


        //GET: api/contracts/{id}
        [HttpGet("{id}")]
        public ActionResult<Models.Contract> GetContracts(int id)
        {
            //Verfica se existe contratos
            if (dbContext.Contracts == null)
                return NotFound();

            //Procura o contrato
            var contract = dbContext.Contracts.SingleOrDefault(s => s.Id == id);

            //Erro se ñ houver contrato
            if (contract == null)
                return NotFound($"Contract with ID {id} not found.");

            //Retorna contrato encontrado
            return Ok(contract);
        }


        //PUT: api/contracts
        [HttpPost]
        public ActionResult<Models.Contract> Add(Models.Contract contract)
        {
            // Verifica contrato é nulo ou ID de listingID inválido
            if (contract == null || contract.ListingId <= 0)
            {
                return BadRequest("Invalid contract: Contract and ListingId are required, and ListingId must be greater than zero.");
            }


            // Validar a existência de ListingId
            bool listingExists = dbContext.Loan_Listings.Any(l => l.Id == contract.ListingId);

            if (!listingExists)
            {
                return BadRequest($"ListingId {contract.ListingId} not found.");
            }

            // Verificar se já existe um contrato associado ao mesmo ListingId
            bool contractExistsForListing = dbContext.Contracts.Any(c => c.ListingId == contract.ListingId);
            if (contractExistsForListing)
            {
                return BadRequest($"A contract already exists {contract.ListingId}.");
            }

            //Guarda na base dados
            try
            {
                dbContext.Contracts.Add(contract);
                dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Retorna erro ao guarda na base dados
                return StatusCode(500, "Erro saving DB" + ex.Message);
            }

            return CreatedAtAction(nameof(Add), new { id = contract.Id }, contract);
        }



        //Put: api/contracts/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Models.Contract contract)
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



