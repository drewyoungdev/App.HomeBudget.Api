using System.Threading.Tasks;
using house_budget_api.BusinessLogic.Interfaces;
using house_budget_api.Models.Listings;
using Microsoft.AspNetCore.Mvc;

namespace house_budget_api.Controllers
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

        [HttpGet]
        public async Task<ActionResult<HousingBudget>> Get()
        {
            return await housingBudgetService.Run();
        }
    }
}
