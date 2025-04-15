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
            var plataforma = await _context.Plataformas
                                .FromSqlRaw("EXEC spConsultarPlataformas")
                                .ToListAsync();

            return plataforma;
        }

        // GET: api/Plataformas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plataforma>> GetPlataforma(int id)
        {
            var resultado = await _context.Plataformas.
                                    FromSqlInterpolated($"EXEC spConsultarPlataformaPorId").
                                    ToListAsync();

            var plataforma = resultado.FirstOrDefault();

            return plataforma == null? NotFound(): plataforma;
        }

        // PUT: api/Plataformas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlataforma(int id, Plataforma plataforma)
        {
           var registro = await _context.Plataformas.
                                FromSqlInterpolated($"EXEC spConsultarPlataformaPorId {id}, {plataforma.Nombre}").
                                ToListAsync();

            var resultado = registro.FirstOrDefault();

            if (resultado == null || resultado.IdPlataforma == -1)
            {
                return Conflict("El nombre de la plataforma ya existe.");
            }

            return Ok("Registro actualizado correctamente");
        }

        // POST: api/Plataformas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plataforma>> PostPlataforma(Plataforma plataforma)
        {
            var resultado = await _context.Plataformas.
                                    FromSqlInterpolated($"EXEC spAgregarPlataforma {plataforma.Nombre}").
                                    ToListAsync();

            var idGenerado = resultado.FirstOrDefault();

            return CreatedAtAction("GetPlataforma", new { id = plataforma.IdPlataforma }, plataforma);
        }

        // DELETE: api/Plataformas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlataforma(int id)
        {
            var registro = await _context.Plataformas.
                              FromSqlInterpolated($"EXEC spEliminarPlataforma {id}").
                              ToListAsync();

            var registroEliminado = registro.FirstOrDefault();

            if (registroEliminado == null)
            {
                return NotFound();
            }

            return Ok("Registro eliminado");
        }

        private bool PlataformaExists(int id)
        {
            return _context.Plataformas.Any(e => e.IdPlataforma == id);
        }
    }
}
