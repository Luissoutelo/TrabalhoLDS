using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDS_2425.Controllers
{
    public class FavoritePageController : Controller
    {
        private readonly MachineHubContext dbContext;
        public FavoritePageController(MachineHubContext dbContext) => this.dbContext = dbContext;

        [HttpPost("{userId}")]
        public async Task<ActionResult> AddToFavoritesMachine(int userId,[FromBody] AddMachineToCartRequest request)
        {
            var favouritePage=await dbContext.FavoritesPages.FirstOrDefaultAsync(x => x.UserId == userId);
            if (favouritePage == null)
            {
                NotFound("There is no favourite page for this user");
            }
            FavoritesPageMachine favoritesPageMachine = new FavoritesPageMachine
            {
                FavoritesPageId = favouritePage.Id,
                 MachineId = request.MachineId
            };

            dbContext.FavoritesPageMachines.Add(favoritesPageMachine);
            await dbContext.SaveChangesAsync();
            return Ok(new { Message = "Machine added to favorites successfully.", FavoriteMachine = request.MachineId });
        }

      
        [HttpPost("{userId}")]
        public async Task<ActionResult> AddToFavoritesLoanListing(int userId,[FromBody] AddMachineToCartRequest request)
        {
            var favouritePageId = await dbContext.FavoritesPages.FirstOrDefaultAsync(x => x.UserId == userId);
            if (favouritePageId == null)
            {
                NotFound("There is no favourite page for this user");
            }

            FavoritesPageLoan_Listing favoritesPages = new FavoritesPageLoan_Listing
            {
                FavoritesPageId = favouritePageId.Id,
                Loan_ListingId = request.MachineId
            };

            dbContext.FavoritesPageLoan_Listings.Add(favoritesPages);
            await dbContext.SaveChangesAsync();
            return Ok(new { Message = "Machine added to favorites successfully.", FavoriteMachine = request.MachineId });
        }

        
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<FavoritesPage>>> GetFavorites(int userId)
        {
            var favoritesPage = await dbContext.FavoritesPages
                .Include(fp => fp.Machines) // Inclui máquinas
                .Include(fp => fp.LoanListings) // Inclui anúncios de aluguel
                .FirstOrDefaultAsync(fp => fp.UserId == userId);

            if (favoritesPage == null)
            {
                return NotFound("Favorites page not found!");
            }

           
            var machines = await dbContext.FavoritesPageMachines
                .Where(fpm => fpm.FavoritesPageId == favoritesPage.Id)
                .Select(fpm => fpm.MachineId)
                .ToListAsync();

            var loanListings = await dbContext.FavoritesPageLoan_Listings
                .Where(fpl => fpl.FavoritesPageId == favoritesPage.Id)
                .Select(fpl => fpl.Loan_ListingId)
                .ToListAsync();

            
            var detailedMachines = await dbContext.Machines
                .Where(m => machines.Contains(m.Id))
                .ToListAsync();

            var detailedLoanListings = await dbContext.Loan_Listings
                .Where(ll => loanListings.Contains(ll.Id))
                .ToListAsync();

          
            var response = new
            {
                Machines = detailedMachines,
                LoanListings = detailedLoanListings
            };

            return Ok(response);
        }



    }
}