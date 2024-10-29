using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDS_2425.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {

        private readonly MachineHubContext dbContext;

        public ReceiptsController(MachineHubContext dbContext) => this.dbContext = dbContext;

        //GET: api/Receipts
        [HttpGet]
        public ActionResult<IEnumerable<Receipt>> GetReceipts()
        {
            if (dbContext.Receipts == null)
                return NotFound();

            return Ok(dbContext.Receipts.ToList());
        }



        //GET: api/Receipts/{id}
        [HttpGet("{id}")]
        public ActionResult<Receipt> GetReceipts(int id)
        {
            if (dbContext.Receipts == null)
                return NotFound();

            var receipt = dbContext.Receipts.SingleOrDefault(s => s.Id == id);

            if (receipt == null)
                return NotFound();

            return Ok(receipt);
        }

        //PUT: api/receipts
        [HttpPost]

        public ActionResult<Receipt> Add(Receipt receipt)
        {
            if (receipt == null)
            {
                return BadRequest("Contrato nao pode ser nulo.");
            }

            //// Validações adicionais para propriedades obrigatórias
            //if (string.IsNullOrEmpty(receipt.CompanyName) ||
            //    string.IsNullOrEmpty(receipt.CompanyEmail) ||
            //    receipt.DateReceipt == default)
            //{
            //    return BadRequest("Campos obrigatórios estão ausentes.");
            //}

            //// Validação da estrutura do recibo conforme seu contexto
            //if (!(receipt.Loan_ListingId.HasValue ^ receipt.MachineId.HasValue))
            //{
            //    return BadRequest("O recibo deve estar associado a um aluguel ou uma máquina específica, não ambos.");
            //}

            dbContext.Receipts.Add(receipt);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(Add), new { id = receipt.Id }, receipt);
        }

        //Put: api/Receipts/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Receipt receipt)
        {
            if (!receipt.Id.Equals(id))
                return BadRequest();

            dbContext.Receipts.Entry(receipt).State = EntityState.Modified;
            dbContext.SaveChanges();
            return NoContent();
        }

        //Delete: api/students/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (dbContext.Receipts == null)
                return NotFound();

            var receipt = dbContext.Receipts.SingleOrDefault(s => s.Id == id);

            if (receipt == null)
                return NotFound();

            dbContext.Receipts.Remove(receipt);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}

