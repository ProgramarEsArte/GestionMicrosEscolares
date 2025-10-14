using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompraGamer.Api.Models;
using CompraGamer.Api.Services;

namespace CompraGamer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoferesController : ControllerBase
    {
        private readonly IChoferService _service;

        public ChoferesController(IChoferService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Chofer>> GetAll() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Chofer>> GetById(string id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Chofer>> Create(Chofer item)
        {
            var created = await _service.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = created.Dni }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Chofer item)
        {
            if (id != item.Dni) return BadRequest();
            var ok = await _service.UpdateAsync(item);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
