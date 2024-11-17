using IRepositroy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModel;
using Services;

namespace DemoProjectAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanctionOrderApiController : ControllerBase
    {
        private readonly ISanctionOrder _SanctionOrder;
        private readonly IConfiguration _configuration;

        public SanctionOrderApiController(ISanctionOrder sanctionOrder, IConfiguration configuration)
        {
            _SanctionOrder = sanctionOrder;
            _configuration = configuration;
        }

        [HttpGet("getFinancialYear")]
  
        public async Task<ActionResult<List<FinancialYearViewModel>>> GetFinancialYear()
        {
            try
            {
                var financialYears = await _SanctionOrder.GetFinancialYear();
                return Ok(financialYears);
            }
            catch (System.Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
