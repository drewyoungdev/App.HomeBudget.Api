using System.Threading.Tasks;
using HouseBudgetApi.Models.Database;
using HouseBudgetApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HouseBudgetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedListingsController : ControllerBase
    {
        private ISavedListingsRepository savedListingsRepository;

        public SavedListingsController(ISavedListingsRepository savedListingsRepository)
        {
            this.savedListingsRepository = savedListingsRepository;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<SavedListings>> Get(string clientId)
        {
            return await savedListingsRepository.Get(clientId);
        }

        [HttpPost]
        public async Task<ActionResult<SavedListings>> Post([FromBody]SavedListings savedListings)
        {
            await savedListingsRepository.Create(savedListings);

            return savedListings;
        }

        [HttpPut]
        public async Task<ActionResult<SavedListings>> Put([FromBody]SavedListings savedListings)
        {
            var savedListingsFromDb = await savedListingsRepository.Get(savedListings.ClientId);

            if (savedListingsFromDb == null)
            {
                return NotFound();
            }

            savedListings.Id = savedListingsFromDb.Id;

            await savedListingsRepository.Update(savedListings);

            return savedListings;
        }
    }
}
