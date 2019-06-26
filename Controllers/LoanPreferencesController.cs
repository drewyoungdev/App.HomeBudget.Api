using System.Threading.Tasks;
using HouseBudgetApi.Models.Database;
using HouseBudgetApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HouseBudgetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanPreferencesController : ControllerBase
    {
        private ILoanPreferencesRepository loanPreferencesRepository;

        public LoanPreferencesController(ILoanPreferencesRepository loanPreferencesRepository)
        {
            this.loanPreferencesRepository = loanPreferencesRepository;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<LoanPreferences>> Get(string clientId)
        {
            return await loanPreferencesRepository.Get(clientId);
        }

        [HttpPost]
        public async Task<ActionResult<LoanPreferences>> Post([FromBody]LoanPreferences loanPreferences)
        {
            await loanPreferencesRepository.Create(loanPreferences);

            return loanPreferences;
        }

        [HttpPut]
        public async Task<ActionResult<LoanPreferences>> Put([FromBody]LoanPreferences loanPreferences)
        {
            var loanPreferencesFromDb = await loanPreferencesRepository.Get(loanPreferences.ClientId);

            if (loanPreferencesFromDb == null)
            {
                return NotFound();
            }

            loanPreferences.Id = loanPreferencesFromDb.Id;

            await loanPreferencesRepository.Update(loanPreferences);

            return loanPreferences;
        }
    }
}
