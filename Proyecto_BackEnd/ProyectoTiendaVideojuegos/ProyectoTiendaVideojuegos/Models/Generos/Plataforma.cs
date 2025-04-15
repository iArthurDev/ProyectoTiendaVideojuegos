using System.ComponentModel.DataAnnotations;

namespace ProyectoTiendaVideojuegos.Models.Generos
{
    public class Plataforma
    {
        [Key]
        public int IdPlataforma { get; set; }

        public string Nombre { get; set; }

        public bool Activo { get; set; }

    }
}
