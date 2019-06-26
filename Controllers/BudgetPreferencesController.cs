using System.Threading.Tasks;
using HouseBudgetApi.Models.Database;
using HouseBudgetApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HouseBudgetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetPreferencesController : ControllerBase
    {
        private IBudgetPreferencesRepository budgetPreferencesRepository;

        public BudgetPreferencesController(IBudgetPreferencesRepository budgetPreferencesRepository)
        {
            this.budgetPreferencesRepository = budgetPreferencesRepository;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<BudgetPreferences>> Get(string clientId)
        {
            return await budgetPreferencesRepository.Get(clientId);
        }

        [HttpPost]
        public async Task<ActionResult<BudgetPreferences>> Post([FromBody]BudgetPreferences budgetPreferences)
        {
            await budgetPreferencesRepository.Create(budgetPreferences);

            return budgetPreferences;
        }

        [HttpPut]
        public async Task<ActionResult<BudgetPreferences>> Put([FromBody]BudgetPreferences budgetPreferences)
        {
            var budgetPreferencesFromDb = await budgetPreferencesRepository.Get(budgetPreferences.ClientId);

            if (budgetPreferencesFromDb == null)
            {
                return NotFound();
            }

            budgetPreferences.Id = budgetPreferencesFromDb.Id;

            await budgetPreferencesRepository.Update(budgetPreferences);

            return budgetPreferences;
        }
    }
}
