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
    public class ChicosController : ControllerBase
    {
        private readonly IChicoService _service;
        private readonly IMicroEscolarService _microService;

        public ChicosController(IChicoService service, IMicroEscolarService microService)
        {
            _service = service;
            _microService = microService;
        }

        [HttpGet]
        public async Task<IEnumerable<Chico>> GetAll() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Chico>> GetById(string id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Chico>> Create(Chico item)
        {
            // Validate foreign key micro_patente if provided
            if (!string.IsNullOrWhiteSpace(item.MicroPatente))
            {
                if (!string.IsNullOrEmpty(item.MicroPatente))
                {
                    var micro = await _microService.GetByIdAsync(item.MicroPatente!);
                    if (micro == null) return BadRequest($"Micro with patente '{item.MicroPatente}' does not exist.");
                }

            }
            else
            {
                item.MicroPatente = null;  // If empty string, set to null
            }
                
            var created = await _service.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = created.Dni }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Chico item)
        {
            if (id != item.Dni) return BadRequest();
            if (!string.IsNullOrWhiteSpace(item.MicroPatente))
            {
                var micro = await _microService.GetByIdAsync(item.MicroPatente!);
                if (micro == null) return BadRequest($"Micro with patente '{item.MicroPatente}' does not exist.");
            }
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
