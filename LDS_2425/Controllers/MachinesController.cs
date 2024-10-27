using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDS_2425.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private readonly MachineHubContext dbContext;

        public MachinesController(MachineHubContext dbContext) => this.dbContext = dbContext;

        // GET : api/machines
        [HttpGet]
        public ActionResult<IEnumerable<Machine>> GetMachines()
        {
            if (dbContext.Machines == null)
            {
                return NotFound();
            }

            return Ok(dbContext.Machines.ToList());
        }

        // GET : api/machines{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Machine>> Getmachine(int id)
        {
            if (dbContext.Machines == null)
            {
                return NotFound();
            }

            var machine = dbContext.Machines.FirstOrDefault(x => x.Id == id);

            if (machine == null)
            {
                return NotFound();
            }

            return Ok(machine);
        }

        // POST : api/machines
        [HttpPost]
        public ActionResult<Machine> Add(Machine machine)
        {
            if (machine == null)
            {
                return BadRequest();
            }

            dbContext.Machines.Add(machine);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(Add), new { id = machine.Id }, machine);
        }

        // PUT : api/machines/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Machine machine)
        {
            if (!machine.Id.Equals(id))
            {
                return BadRequest();
            }

            dbContext.Machines.Entry(machine).State = EntityState.Modified;
            dbContext.SaveChanges();
            return NoContent();
        }

        // DELETE : api/machines/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (dbContext.Machines == null)
            {
                return NotFound();
            }

            var machine = dbContext.Machines.SingleOrDefault(m => m.Id == id);

            if (machine == null)
            {
                return NotFound();
            }

            dbContext.Machines.Remove(machine);
            dbContext.SaveChanges();
            return NoContent();
        }

        // GET : api/machines{id}
        [HttpGet("{id}/receiptsCount")]
        public ActionResult<int> GetPurchaseCountForMachine(int id)
        {
            if (dbContext.Receipts == null)
            {
                return NotFound();
            }

            var receipts = dbContext.Receipts.Count(r => r.Id == id);

            return Ok(receipts);
        }
    }
}
