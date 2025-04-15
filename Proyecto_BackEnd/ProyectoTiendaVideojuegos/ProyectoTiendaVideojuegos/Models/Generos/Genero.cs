using System.ComponentModel.DataAnnotations;

namespace ProyectoTiendaVideojuegos.Models.Generos
{
    public class Genero
    {
        [Key]
        public int IdGenero { get; set; }

        public string Nombre { get; set; }

        public bool Activo { get; set; }

    }
}
