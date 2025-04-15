using Microsoft.EntityFrameworkCore;
using ProyectoTiendaVideojuegos.Models.Generos;
namespace ProyectoTiendaVideojuegos.Models.Conexion
{
    public class DbAplicacion : DbContext
    {
        public DbAplicacion(DbContextOptions<DbAplicacion> options) : base(options)
        { }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<Plataforma> Plataformas { get; set; }


    }
}
