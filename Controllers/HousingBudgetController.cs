using System.Threading.Tasks;
using HouseBudgetApi.BusinessLogic.Interfaces;
using HouseBudgetApi.Models.Listings;
using Microsoft.AspNetCore.Mvc;

namespace HouseBudgetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousingBudgetController : ControllerBase
    {
        private IHousingBudgetService housingBudgetService;

        public HousingBudgetController(IHousingBudgetService housingBudgetService)
        {
            this.housingBudgetService = housingBudgetService;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<HousingBudget>> Get(string clientId)
        {
            return await housingBudgetService.Run(clientId);
        }

        // add various desired goals: Saving $100 per month, $0 Additional Income
        // add ability to use sliders to adjust your budget preferences to see what other listings you might be eligible for in your area. (assuming certain things like percentage for down payment)
        // we can include multiple loan products to increase the calculation for multiple options
    }
}
