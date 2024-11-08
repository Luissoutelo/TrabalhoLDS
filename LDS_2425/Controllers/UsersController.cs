﻿using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDS_2425.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MachineHubContext dbContext;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly TokenGenerator tokenGenerator;

        public UsersController(MachineHubContext dbContext, IPasswordHasher<User> passwordHasher, TokenGenerator tokenGenerator)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.tokenGenerator = tokenGenerator;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var isValidUser = dbContext.ValidateUserCredentials(loginRequest);

            if (!isValidUser)
            {
                return Unauthorized("Invalid email or password.");
            }

            var user = dbContext.Users.FirstOrDefault(u => u.Email == loginRequest.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email.");
            }

            // Generate token if credentials are valid
            var token = tokenGenerator.GenerateToken(loginRequest.Email, user.Type);
            return Ok(new { Token = token });
        }

        // GET : api/users
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (dbContext.Users == null)
            {
                return NotFound();
            }

            var usersList = await dbContext.Users.ToListAsync();

            return Ok(usersList);
        }

        // GET : api/users{id}
        [Authorize(Roles = "Administrator")]
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
        public ActionResult<User> Add(User user, string password)
        {
            // Check if email already exists
            var emailUsed = dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            if (emailUsed != null)
            {
                return Conflict(new { message = "Email is already in use." });
            }

            // Check if phone number already exists
            var phoneNumberUsed = dbContext.Users.FirstOrDefault(u => u.PhoneNumber == user.PhoneNumber);
            if (phoneNumberUsed != null)
            {
                return Conflict(new { message = "Phone number is already in use." });
            }

            // Hash the password and set it on the user object
            user.PasswordHash = passwordHasher.HashPassword(user, password);

            if (string.IsNullOrEmpty(user.Type))
            {
                user.Type = "User";
            }

            // Add the user to the database
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            ShoppingCart sc = new() {userId = user.Id};
            FavoritesPage fp = new() { UserId = user.Id };
            
            dbContext.ShoppingCarts.Add(sc);
            dbContext.FavoritesPages.Add(fp);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(Add), new { id = user.Id }, user);
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
                .FirstOrDefault(u => u.Email == user.Email && u.Id != id);

            if (emailUsed != null)
            {
                return Conflict(new { message = "Email is already in use." });
            }

            // Vê se número de telemóvel existe
            var phoneNumberUsed = dbContext.Users
                .FirstOrDefault(u => u.PhoneNumber == user.PhoneNumber && u.Id != id);

            if (phoneNumberUsed != null)
            {
                return Conflict(new { message = "Phone number is already in use." });
            }

            dbContext.Users.Entry(user).State = EntityState.Modified;
            dbContext.SaveChanges();
            return NoContent();
        }

        // DELETE : api/users/{id}
        [Authorize(Roles = "Administrator")]
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

        // GET : api/users{id}
        [Authorize]
        [HttpGet("{id}/purchasesCount")]
        public ActionResult<int> GetPurchaseCountForUser(int id)
        {
            if (dbContext.Users == null)
            {
                return NotFound();
            }

            var purchases = dbContext.Receipts.Count(r => r.UserId == id);

            return Ok(purchases);
        }

        // GET : api/users{id}
        [Authorize]
        [HttpGet("{id}/loanCount")]
        public ActionResult<int> GetMachinesLoanedCountForUser(int id)
        {
            if (dbContext.Users == null)
            {
                return NotFound();
            }

            var loans = dbContext.Receipts.Count(l => l.OwnerId == id);

            return Ok(loans);
        }

        // GET : api/users{id}
        [Authorize]
        [HttpGet("{id}/purchaseReceipts")]
        public ActionResult<IEnumerable<User>> GetPurchaseReceiptsForUser(int id)
        {
            if (dbContext.Users == null)
            {
                return NotFound();
            }

            var receipts = dbContext.Receipts
                .Where(r => r.UserId == id)
                .ToList();

            if (receipts == null)
            {
                return BadRequest();
            }

            return Ok(receipts);
        }

        // GET : api/users{id}
        [Authorize]
        [HttpGet("{id}/loanReceipts")]
        public ActionResult<IEnumerable<User>> GetLoanReceiptsForUser(int id)
        {
            if (dbContext.Users == null)
            {
                return NotFound();
            }

            var receipts = dbContext.Receipts
                .Where(r => r.OwnerId == id)
                .ToList();

            if (receipts == null)
            {
                return BadRequest();
            }

            return Ok(receipts);
        }
    }
}
