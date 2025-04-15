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
    [Route("api/Generos")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly DbAplicacion _context;

        public GeneroController(DbAplicacion context)
        {
            _context = context;
        }

        // GET: api/Genero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
        {
            var generos = await _context.Generos
                                .FromSqlRaw("EXEC spConsultarGeneros")
                                .ToListAsync();

            return generos;
        }

        // GET: api/Genero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            var generos = await _context.Generos
                           .FromSqlInterpolated($"EXEC spConsultarGeneroPorID {id}")
                           .ToListAsync();

            var genero = generos.FirstOrDefault();

            return genero == null ? NotFound() : genero;
        }

        // PUT: api/Genero/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenero(int id, Genero genero)
        {
            var consulta = await _context.Generos.
                                            FromSqlInterpolated($"EXEC spActualizarGenero {id}, {genero.Nombre}").
                                            ToListAsync();

            var registroActualizado = consulta.FirstOrDefault();

            if (registroActualizado == null || registroActualizado.IdGenero == -1)
            {
                return Conflict("El nombre del género ya existe.");
            }

            return Ok("Registro actualizado correctamente");
        }

        // POST: api/Genero
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Genero>> PostGenero(Genero genero)
        {
            var resultado = await _context.Generos
                                    .FromSqlInterpolated($"EXEC spAgregarGenero {genero.Nombre}")
                                    .ToListAsync();

            var idGenerado = resultado.FirstOrDefault()?.IdGenero ?? -1;

            if (idGenerado == -1)
            {
                return Conflict("El género ya existe.");
            }

            genero.IdGenero = idGenerado;
            genero.Activo = true;

            return CreatedAtAction("GetGenero", new { id = genero.IdGenero }, genero);
        }

        // DELETE: api/Genero/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            var registro = await _context.Generos.
                              FromSqlInterpolated($"EXEC spEliminarGenero {id}").
                              ToListAsync();

            var registroEliminado = registro.FirstOrDefault();

            if (registroEliminado == null)
            {
                return NotFound();
            }

            return Ok("Registro eliminado");
        }

        private bool GeneroExists(int id)
        {
            return _context.Generos.Any(e => e.IdGenero == id);
        }
    }
}
