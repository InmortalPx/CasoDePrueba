using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
        {
            return await _context.Personas.ToListAsync();
        }

        // GET: api/personas/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null) return NotFound();
            return persona;
        }

        // POST: api/personas
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona)
        {
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);
        }

        // PUT: api/personas/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, Persona persona)
        {
            if (id != persona.Id) return BadRequest();

            var existente = await _context.Personas.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Nombre = persona.Nombre;
            existente.Email = persona.Email;
            existente.Telefono = persona.Telefono;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/personas/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null) return NotFound();
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}