using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrderAPI.Data;
using PizzaOrderAPI.Exceptions;
using PizzaOrderAPI.Models;
using PizzaOrderAPI.Auth;
using Microsoft.AspNetCore.Authorization;

namespace PizzaOrderAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;

        public StoresController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStore()
        {
            return Ok(await _storeRepository.GetAllStores()); 
        }

        //POST: API/Stores/Search
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<IEnumerable<Store>>> SearchStore(StoreQuery query)
        {
            try
            {
                return Ok((await _storeRepository.GetStores(query)).ToList());
            }
            catch
            {
                return Problem("Something went wrong.");
            }
            
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(int id)
        {
            var store = await _storeRepository.GetStore(id);

            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // PUT: api/Stores/5
        [HttpPut("{id}")]
        [Authorize(Roles ="GeneralManager")]
        public async Task<ActionResult<Store>> PutStore(int id, StoreUpdate storeUpdate)
        {
            try
            {
                await _storeRepository.UpdateStore(id, storeUpdate);
                return NoContent();
            }
            catch(CustomException ex)
            {
                return NotFound();
            }
        }

        // POST: api/Stores
        [HttpPost]
        [Authorize(Roles = "GeneralManager")]
        public async Task<ActionResult<Store>> PostStore(Store store)
        {
            try
            {
                var newStore = await _storeRepository.AddStore(store);
                return CreatedAtAction("GetStore", new { id = newStore.Id }, newStore);
            }
            catch(InvalidException ex)
            {
                return BadRequest(ex.ModelState);
            }
        }

    }
}
