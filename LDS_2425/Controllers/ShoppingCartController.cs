using System.Reflection.PortableExecutable;
using System.Security.Claims;
using LDS_2425.Data;
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


        //sem lgin
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetShoppingCart(int userId) { 


            var shoppingCart = await dbContext.ShoppingCarts.FirstOrDefaultAsync(c => c.userId == userId);

            if (shoppingCart == null)
            {
                return NotFound("Shopping Cart not found!"); // Retorna 404 se o carrinho não existir
            }
           var shoppingCartMachines= await dbContext.ShoppingCartMachineConnections
                .Where( scm => scm.ShoppingCartId==shoppingCart.Id)
                .ToListAsync();

            if (shoppingCartMachines == null)
            {
                return Ok(new { Message = "The shopping cart is empty." });
            }
            var machinesIds=shoppingCartMachines.Select(scm=>scm.MachineId).ToList();

            var machines = await dbContext.Machines
                .Where(m => machinesIds.Contains(m.Id))
                .ToListAsync();

            return Ok(machines);
       
    }
        [HttpPost("{userId}/add-machine")]
        public async Task<IActionResult> AddMachineToCart(int userId, [FromBody] int machineId)
        {
            var shoppingCart = await dbContext.ShoppingCarts.FirstOrDefaultAsync(c => c.userId == userId);

            if (shoppingCart == null)
            {
                return NotFound(new { message = "Shopping cart not found." });
            }
            var machineInCart = shoppingCart.Machines
            .FirstOrDefault(sm => sm.MachineId == machineId);


            ShoppingCartMachine machine = new ShoppingCartMachine
            {
                ShoppingCartId = shoppingCart.Id,
                MachineId = machineId
            };

            dbContext.ShoppingCartMachineConnections.Add(machine);
            dbContext.SaveChanges();



            return Ok(new { message = "Machine added to cart." });

        }
        /*com login
        [HttpGet]
        public async Task<IActionResult> GetShoppingCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Implementação para obter o carrinho com base no `userId` do token
        }
        */

    }
}
