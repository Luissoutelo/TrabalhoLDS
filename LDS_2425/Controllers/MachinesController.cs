using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Authorization;
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

        // GET : api/machines{id}
        [Authorize(Roles = "Administrator")]
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

        //GET: api/Machines/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public ActionResult<IEnumerable<Machine>> GetMachineByCategory(int categoryId)
        {
            if (dbContext.Machines == null)
                return NotFound();

            var machines = dbContext.Machines.Where(m => m.CategoryId == categoryId).ToList();

            if (machines == null)
                return NotFound();

            return Ok(machines);
        }

        // GET : api/machines{name}
        [HttpGet("machineName/{name}")]
        public ActionResult<IEnumerable<Machine>> GetmachineByName(string name)
        {
            if (dbContext.Machines == null)
            {
                return NotFound();
            }

            var machine = dbContext.Machines.FirstOrDefault(x => x.Name == name);

            if (machine == null)
            {
                return NotFound();
            }

            return Ok(machine);
        }

        // POST : api/machines
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Machine> Add(Machine machine)
        {
            if (machine == null)
            {
                return BadRequest();
            }

            bool categoryExists = dbContext.Categories.Any(c => c.Id == machine.CategoryId);

            if (!categoryExists)
            {
                return BadRequest($"Category with Id {machine.CategoryId} doesn't exist.");
            }

            dbContext.Machines.Add(machine);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(Add), new { id = machine.Id }, machine);
        }

        // PUT : api/machines/{id}
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
    }
}