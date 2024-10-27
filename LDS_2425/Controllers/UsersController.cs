using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDS_2425.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MachineHubContext dbContext;

        public UsersController(MachineHubContext dbContext) => this.dbContext = dbContext;

        // GET : api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            if (dbContext.Users == null)
            {
                return NotFound();
            }

            return Ok(dbContext.Users.ToList());
        }

        // GET : api/users{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<User>> GetUser(int id)
        {
            if (dbContext.Users == null)
            {
                return NotFound();
            }

            var user = dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST : api/users
        [HttpPost]
        public ActionResult<User> Add(User user)
        {
            // Vê se email existe
            var emailUsed = dbContext.Users
                .FirstOrDefault(u => u.Email == user.Email);

            if (emailUsed != null)
            {
                return Conflict(new { message = "Email is already in use." });
            }

            // Vê se número de telemóvel existe
            var phoneNumberUsed = dbContext.Users
                .FirstOrDefault(u => u.PhoneNumber == user.PhoneNumber);

            if (phoneNumberUsed != null)
            {
                return Conflict(new { message = "Phone number is already in use." });
            }

            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(Add), new {id = user.Id}, user);
        }

        // PUT : api/users/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            if (!user.Id.Equals(id))
            {
                return BadRequest();
            }

            // Vê se email existe
            var emailUsed = dbContext.Users
                .FirstOrDefault(u => u.Email == user.Email);

            if (emailUsed != null)
            {
                return Conflict(new { message = "Email is already in use." });
            }

            // Vê se número de telemóvel existe
            var phoneNumberUsed = dbContext.Users
                .FirstOrDefault(u => u.PhoneNumber == user.PhoneNumber);

            if (phoneNumberUsed != null)
            {
                return Conflict(new { message = "Phone number is already in use." });
            }

            dbContext.Users.Entry(user).State = EntityState.Modified;
            dbContext.SaveChanges();
            return NoContent();
        }

        // DELETE : api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (dbContext.Users == null)
            {
                return NotFound();
            }

            var user = dbContext.Users.SingleOrDefault(u => u.  Id == id);

            if (user == null) 
            { 
                return NotFound();
            }

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
