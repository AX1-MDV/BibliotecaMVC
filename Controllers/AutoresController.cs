using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers
{
    public class AutoresController : Controller
    {
        public IActionResult Index()
        {
            List<Autor> autores = new List<Autor>
            {
                new Autor { Id = 1, Nombre = "Robert", Apellido = "Martin", Nacionalidad = "Estadounidense", FechaNacimiento = new DateTime(1952, 12, 5), Activo = true },
                new Autor { Id = 2, Nombre = "Miguel", Apellido = "de Cervantes", Nacionalidad = "Español", FechaNacimiento = new DateTime(1547, 9, 29), Activo = false },
                new Autor { Id = 3, Nombre = "Gabriel", Apellido = "García Márquez", Nacionalidad = "Colombiano", FechaNacimiento = new DateTime(1927, 3, 6), Activo = false },
                new Autor { Id = 4, Nombre = "Antoine", Apellido = "de Saint-Exupéry", Nacionalidad = "Francés", FechaNacimiento = new DateTime(1900, 6, 29), Activo = false },
                new Autor { Id = 5, Nombre = "George", Apellido = "Orwell", Nacionalidad = "Británico", FechaNacimiento = new DateTime(1903, 6, 25), Activo = false }
            };
            ViewBag.autores = autores;
            return View();
        }
    }
}
