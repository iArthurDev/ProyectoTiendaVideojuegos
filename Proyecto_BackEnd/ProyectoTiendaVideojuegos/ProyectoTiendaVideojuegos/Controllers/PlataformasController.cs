using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTiendaVideojuegos.Models.Conexion;
using ProyectoTiendaVideojuegos.Models.Generos;

namespace ProyectoTiendaVideojuegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlataformasController : ControllerBase
    {
        private readonly DbAplicacion _context;

        public PlataformasController(DbAplicacion context)
        {
            _context = context;
        }

        // GET: api/Plataformas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plataforma>>> GetPlataformas()
        {
            return await _context.Plataformas.ToListAsync();
        }

        // GET: api/Plataformas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plataforma>> GetPlataforma(int id)
        {
            var plataforma = await _context.Plataformas.FindAsync(id);

            if (plataforma == null)
            {
                return NotFound();
            }

            return plataforma;
        }

        // PUT: api/Plataformas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlataforma(int id, Plataforma plataforma)
        {
            if (id != plataforma.IdPlataforma)
            {
                return BadRequest();
            }

            _context.Entry(plataforma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlataformaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Plataformas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plataforma>> PostPlataforma(Plataforma plataforma)
        {
            _context.Plataformas.Add(plataforma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlataforma", new { id = plataforma.IdPlataforma }, plataforma);
        }

        // DELETE: api/Plataformas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlataforma(int id)
        {
            var plataforma = await _context.Plataformas.FindAsync(id);
            if (plataforma == null)
            {
                return NotFound();
            }

            _context.Plataformas.Remove(plataforma);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlataformaExists(int id)
        {
            return _context.Plataformas.Any(e => e.IdPlataforma == id);
        }
    }
}
