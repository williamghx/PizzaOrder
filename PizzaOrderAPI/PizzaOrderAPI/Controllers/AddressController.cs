using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderAPI.Enums;

namespace PizzaOrderAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        //GET: api/address/states

        [HttpGet]
        [Route("States")]
        public ActionResult<IEnumerable<State>> GetsStates()
        {
            return Ok(Enum.GetValues(typeof(State)));
        }

        //GET api/address/streettypes

        [HttpGet]
        [Route("StreetTypes")]
        public ActionResult<IEnumerable<StreetType>> GetStreetTypes()
        {
            return Ok(Enum.GetValues(typeof(StreetType)));
        }
    }
}
