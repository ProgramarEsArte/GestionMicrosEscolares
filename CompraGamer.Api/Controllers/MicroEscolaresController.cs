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
    public class MicroEscolaresController : ControllerBase
    {
        private readonly IMicroEscolarService _service;
        private readonly IChoferService _choferService;

        public MicroEscolaresController(IMicroEscolarService service, IChoferService choferService)
        {
            _service = service;
            _choferService = choferService;
        }

        [HttpGet]
        public async Task<IEnumerable<MicroEscolar>> GetAll() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<MicroEscolar>> GetById(string id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<MicroEscolar>> Create(MicroEscolar item)
        {
            if (!string.IsNullOrWhiteSpace(item.ChoferDni))
            {
                var chofer = await _choferService.GetByIdAsync(item.ChoferDni!);
                if (chofer == null) return BadRequest($"Chofer with dni '{item.ChoferDni}' does not exist.");
            }
            var created = await _service.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = created.Patente }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, MicroEscolar item)
        {
            if (id != item.Patente) return BadRequest();
            if (!string.IsNullOrWhiteSpace(item.ChoferDni))
            {
                var chofer = await _choferService.GetByIdAsync(item.ChoferDni!);
                if (chofer == null) return BadRequest($"Chofer with dni '{item.ChoferDni}' does not exist.");
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
