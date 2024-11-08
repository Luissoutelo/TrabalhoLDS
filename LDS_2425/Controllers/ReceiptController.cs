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

        //Apresentar lista dos Recibos
        //GET: api/Receipts
        [HttpGet]
        public ActionResult<IEnumerable<Receipt>> GetReceipts()
        {
            //Se lista estiver vazia
            if (dbContext.Receipts == null)
            {
                return NotFound();
            }

            return Ok(dbContext.Receipts.ToList());
        }

        //Apresenta 1 expecifico recibo
        //GET: api/Receipts/{id}
        [HttpGet("{id}")]
        public ActionResult<Receipt> GetReceipts(int id)
        {
            if (dbContext.Receipts == null)
            {
                return NotFound();
            }

            var receipt = dbContext.Receipts.SingleOrDefault(s => s.Id == id);

            if (receipt == null)
            {
               return NotFound();
            }
            return Ok(receipt);
        }

        //Adiciona um recibo
        //PUT: api/receipts
        [HttpPost]
        public ActionResult<Receipt> Add(Receipt receipt)
        {
            if (receipt == null)
            {
                return BadRequest("Contrato nao pode ser nulo.");
            }

            // Validar se ID loanListing
            if (receipt.Loan_ListingId.HasValue)
            {
                bool listingExist = dbContext.Loan_Listings.Any(l => l.Id == receipt.Loan_ListingId.Value);
                if (!listingExist)
                {
                    return BadRequest("ListingID não é valido");
                }
            }

            //Validar se ID Contrato
            if (receipt.ContractId.HasValue)
            {
                bool contractExist = dbContext.Contracts.Any(c => c.Id == receipt.ContractId.Value);
                if (!contractExist)
                {
                    return BadRequest("ContratoID não é valido");
                }
            }

            dbContext.Receipts.Add(receipt);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(Add), new { id = receipt.Id }, receipt);
        }


    }
}