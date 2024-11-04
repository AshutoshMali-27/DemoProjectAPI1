using IRepositroy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModel;
using Services;

namespace DemoProjectAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _MenuService;
        private readonly IConfiguration _configuration;

        public MenuController(IMenuService MenuService, IConfiguration configuration)
        {
            _MenuService = MenuService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<MenuItemViewModel>>> GetMenuItems()
        {
            try
            {
                var menuItems = await _MenuService.GetMenuItemsAsync();
                return Ok(menuItems);
            }
            catch (System.Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemViewModel>> GetMenuItemById(int id)
        {
            try
            {
                var menuItem = await _MenuService.GetMenuItemByIdAsync(id);

                if (menuItem == null)
                {
                    return NotFound(); // 404 if not found
                }

                return Ok(menuItem); // 200 with the menu item
            }
            catch (System.Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


    }
}
