﻿using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LDS_2425.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly MachineHubContext dbContext;
        public ShoppingCartController(MachineHubContext dbContext) => this.dbContext = dbContext;

        // Método para obter máquinas do carrinho do usuário
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetMachineFromCart(int userId)
        {
            var shoppingCart = await dbContext.ShoppingCarts.FirstOrDefaultAsync(c => c.userId == userId);

            if (shoppingCart == null)
            {
                return NotFound("Shopping Cart not found!"); 
            }

            var shoppingCartMachines = await dbContext.ShoppingCartMachineConnections
                .Where(scm => scm.ShoppingCartId == shoppingCart.Id)
                .ToListAsync();

            if (!shoppingCartMachines.Any())
            {
                return Ok(new { Message = "The shopping cart is empty." });
            }

            var machineIds = shoppingCartMachines.Select(scm => scm.MachineId).ToList();

            var machines = await dbContext.Machines
                .Where(m => machineIds.Contains(m.Id))
                .ToListAsync();

            return Ok(machines);
        }

        // Método para adicionar uma máquina ao carrinho do usuário
        [HttpPost("{userId}/add-machine")]
        public async Task<IActionResult> AddMachineToCart(int userId,  int machineId)
        {
            var shoppingCart = await dbContext.ShoppingCarts
                .Include(sc => sc.Machines)
                .FirstOrDefaultAsync(c => c.userId == userId);

            if (shoppingCart == null)
            {
                return NotFound(new { message = "Shopping cart not found." });
            }

            // Verifica se a máquina já está no carrinho
            var machineInCart = shoppingCart.Machines
                .FirstOrDefault(sm => sm.MachineId == machineId);

            if (machineInCart != null)
            {
                return BadRequest(new { message = "Machine is already in the cart." });
            }

            var shoppingCartMachine = new ShoppingCartMachine
            {
                ShoppingCartId = shoppingCart.Id,
                MachineId = machineId
            };

            dbContext.ShoppingCartMachineConnections.Add(shoppingCartMachine);
            await dbContext.SaveChangesAsync();

            return Ok(new { message = "Machine added to cart." });
        }
    }
}
