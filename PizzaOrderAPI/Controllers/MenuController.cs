using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderAPI.Data;
using PizzaOrderAPI.Exceptions;
using PizzaOrderAPI.Models;

namespace PizzaOrderAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        // GET: api/Menu/?storeId=1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetMenu([FromQuery] int storeId)
        {
            return Ok(await _menuRepository.GetMenuByStore(storeId));
        }

        // GET: api/Menu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetMenuItem(int id)
        {
            var menuItem = await _menuRepository.GetMenuItem(id);

            if(menuItem == null)
            {
                return NotFound();
            }
            return Ok(menuItem);
        }

        //GET: api/Menu/AvailablePizzas/2
        [HttpGet("AvailablePizzas/{storeId}")]
        [Authorize(Roles ="GeneralManager, Manager")]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetAvailablePizzas(int storeId)
        {
            return Ok(await _menuRepository.GetAvailablePizzas(storeId));
        }

        //GET: api/Menu/Toppings
        [HttpGet]
        [Route("Toppings")]
        public async Task<ActionResult<Topping>> GetToppings()
        {
            try
            {
                return Ok((await _menuRepository.GetToppings()).ToList());
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }

        // PUT: api/Menu/5
        [HttpPut("{id}")]
        [Authorize(Roles ="GeneralManager,Manager")]
        public async Task<ActionResult<MenuItem>> PutMenuItem(int id, MenuUpdate menuUpdate)
        {
            try
            {
                await _menuRepository.UpdateStore(id, menuUpdate);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch(CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }

        // POST API/Menu
        [HttpPost]
        [Authorize(Roles = "GeneralManager,Manager")]
        public async Task<ActionResult<MenuItem>> PostMenuItem(MenuUpdate menuUpdate)
        {
            try
            {
                var newMenuItem = await _menuRepository.AddMenuItem(menuUpdate);
                return CreatedAtAction("GetMenuItem", new { id = newMenuItem.Id }, newMenuItem);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }

    }
}
