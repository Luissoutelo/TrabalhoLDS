using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDS_2425.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MachineHubContext dbContext;

        public CategoriesController(MachineHubContext dbContext) => this.dbContext = dbContext;

        // GET : api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            if (dbContext.Categories == null)
            {
                return NotFound();
            }

            var categories = await dbContext.Categories.ToListAsync();

            return Ok(categories);
        }

        // GET : api/categories{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Category>> GetCategory(int id)
        {
            if (dbContext.Categories == null)
            {
                return NotFound();
            }

            var category = dbContext.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST : api/categories
        [HttpPost]
        public ActionResult<Category> Add(Category category)
        {
            // Vê se existe categoria com nome
            var nameUsed = dbContext.Categories
                .FirstOrDefault(c => c.Name == category.Name);

            if (nameUsed != null)
            {
                return Conflict(new { message = "Name is already in use." });
            }

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(Add), new { id = category.Id }, category);
        }

        // PUT : api/categories/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Category category)
        {
            if (!category.Id.Equals(id))
            {
                return BadRequest();
            }

            // Vê se existe categoria com nome
            var nameUsed = dbContext.Categories
                .FirstOrDefault(c => c.Name == category.Name);

            if (nameUsed != null)
            {
                return Conflict(new { message = "Name is already in use." });
            }

            dbContext.Categories.Entry(category).State = EntityState.Modified;
            dbContext.SaveChanges();
            return NoContent();
        }

        // DELETE : api/categories/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (dbContext.Categories == null)
            {
                return NotFound();
            }

            var category = dbContext.Categories.SingleOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
            return NoContent();
        }

        // GET : api/categories{id}
        [HttpGet("{id}/machineCount")]
        public async Task<ActionResult<int>> GetMachineCountForCategory(int id)
        {
            if (dbContext.Categories == null || !dbContext.Categories.Any(c => c.Id == id))
            {
                return NotFound();
            }

            var machines = await dbContext.Machines.CountAsync(m => m.CategoryId == id);
            return Ok(machines);
        }

        // GET : api/categories{id}
        [HttpGet("{id}/loanListingCount")]
        public async Task<ActionResult<int>> GetLoanListingCountForCategory(int id)
        {
            if (dbContext.Categories == null || !dbContext.Categories.Any(c => c.Id == id))
            {
                return NotFound();
            }

            var listings = await dbContext.Loan_Listings.CountAsync(l => l.CategoryId == id);

            return Ok(listings);
        }
    }
}
