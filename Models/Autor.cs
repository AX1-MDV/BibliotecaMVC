using System.ComponentModel.DataAnnotations;
namespace BibliotecaMVC.Models
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres")] public string Sex { get; set; } = string.Empty;
        public string Apellido { get; set; }
        public string Nacionalidad { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; }
        public bool Activo { get; set; }

    }
}
