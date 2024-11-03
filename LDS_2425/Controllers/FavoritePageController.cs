using LDS_2425.Data;
using LDS_2425.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDS_2425.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritePageController : Controller
    {
        private readonly MachineHubContext dbContext;
        public FavoritePageController(MachineHubContext dbContext) => this.dbContext = dbContext;

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<FavoritesPage>>> GetFavorites(int userId)
        {
            var favoritesPage = await dbContext.FavoritesPages
                .Include(fp => fp.Machines) 
                .Include(fp => fp.LoanListings) 
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


        [HttpPost("{userId}/addSellMachine")]
        public async Task<ActionResult> AddToFavoritesMachine(int userId,[FromBody] AddMachineToCartRequest request)
        {
            var favouritePage=await dbContext.FavoritesPages.FirstOrDefaultAsync(x => x.UserId == userId);
            if (favouritePage == null)
                if (favouritePage == null)
                {
                    return NotFound("There is no favourite page for this user");
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

      
        [HttpPost("{userId}/addLoanMachine")]
        public async Task<ActionResult> AddToFavoritesLoanListing(int userId,[FromBody] AddMachineToCartRequest request)
        {
            var favouritePageId = await dbContext.FavoritesPages.FirstOrDefaultAsync(x => x.UserId == userId);
            if (favouritePageId == null)
            {
                return NotFound("There is no favourite page for this user");
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
        [HttpDelete("{userId}/removeSellMachine/{machineId}")]
        public async Task<ActionResult> RemoveFromFavoritesMachine(int userId, int machineId)
        {
            var favoritePage = await dbContext.FavoritesPages.FirstOrDefaultAsync(x => x.UserId == userId);
            if (favoritePage == null)
            {
                return NotFound("There is no favorite page for this user");
            }

            var favoriteMachine = await dbContext.FavoritesPageMachines
                .FirstOrDefaultAsync(fpm => fpm.FavoritesPageId == favoritePage.Id && fpm.MachineId == machineId);

            if (favoriteMachine == null)
            {
                return NotFound("Machine not found in user's favorites");
            }

            dbContext.FavoritesPageMachines.Remove(favoriteMachine);
            await dbContext.SaveChangesAsync();

            return Ok(new { Message = "Machine removed from favorites successfully.", RemovedMachineId = machineId });
        }
        [HttpDelete("{userId}/removeLoanMachine/{loanListingId}")]
        public async Task<ActionResult> RemoveFromFavoritesLoanListing(int userId, int loanListingId)
        {
            var favoritePage = await dbContext.FavoritesPages.FirstOrDefaultAsync(x => x.UserId == userId);
            if (favoritePage == null)
            {
                return NotFound("There is no favorite page for this user");
            }

            var favoriteLoanListing = await dbContext.FavoritesPageLoan_Listings
                .FirstOrDefaultAsync(fpl => fpl.FavoritesPageId == favoritePage.Id && fpl.Loan_ListingId == loanListingId);

            if (favoriteLoanListing == null)
            {
                return NotFound("Loan listing not found in user's favorites");
            }

            dbContext.FavoritesPageLoan_Listings.Remove(favoriteLoanListing);
            await dbContext.SaveChangesAsync();

            return Ok(new { Message = "Loan listing removed from favorites successfully.", RemovedLoanListingId = loanListingId });
        }






    }
}