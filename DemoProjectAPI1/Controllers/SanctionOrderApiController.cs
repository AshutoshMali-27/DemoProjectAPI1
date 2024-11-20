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



        [HttpGet("getsanctiondetails")]
        public async Task<ActionResult<List<SanctionOrderDetails>>> getsanctiondetails()
        {
            try
            {
                var sanctionfdetails = await _SanctionOrder.Getsanctionorderdetails();
                return Ok(sanctionfdetails);
            }
            catch (System.Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("getSchemeName")]
        public async Task<ActionResult<List<Scheme>>> getScheme()
        {
            try
            {
                var schemename = await _SanctionOrder.GetSchemeNames();
                return Ok(schemename);
            }
            catch (System.Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpGet("getComponentName/{schemeid}")]
        public async Task<ActionResult<List<Scheme>>> getComponentbyschemeid( int schemeid)
        {
            try
            {
                // Call the service method and pass schemeid as parameter
                var schemename = await _SanctionOrder.GetComponentNames(schemeid);

                // Return the result with 200 OK status
                return Ok(schemename);
            }
            catch (System.Exception ex)
            {
                // Log the exception (optional, consider using a logging framework like Serilog, NLog, etc.)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpPost("InsertSanctionOrderEntry")]
        public async Task<IActionResult> InsertSanctionOrderEntry([FromBody] SanctionOrderEntryViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid input data");
            }

            try
            {
                int newId = await _SanctionOrder.InsertSanctionOrderEntry(
                    model.FinancialYearId,
                    model.SchemeId,
                    model.ComponentId, model.utypeid, model.ulbid,model.SanctionAmount,model.ExpenditureAmount,model.BalanceAmount);

                return Ok(new { Message = "Sanction order entry created successfully", NewId = newId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
