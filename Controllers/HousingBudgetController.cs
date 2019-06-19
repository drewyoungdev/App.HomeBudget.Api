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

        [HttpGet]
        public async Task<ActionResult<HousingBudget>> Get()
        {
            return await housingBudgetService.Run();
        }
    }
}
