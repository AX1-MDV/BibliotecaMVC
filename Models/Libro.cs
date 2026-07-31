using System.ComponentModel.DataAnnotations;

namespace BibliotecaMVC.Models
{
    public class Libro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(150)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio")]
        [StringLength(100)]
        public string Autor { get; set; }

        [StringLength(50)]
        public string Categoria { get; set; }

        [Range(0, 1000, ErrorMessage = "Precio fuera de rango")]
        public decimal Precio { get; set; }

        public bool Disponible { get; set; }

        [Range(1, 9999, ErrorMessage = "Año inválido")]
        public int AnioPublicacion { get; set; }
        public string? ImagePath { get; set; }
    }
}