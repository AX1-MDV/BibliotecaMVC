using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BibliotecaMVC.Controllers
{
    public class AutoresController : Controller
    {
        private static List<Autor> _autores = new List<Autor>
        {
            new Autor { Id = 1, Nombre = "Robert", Apellido = "Martin", Nacionalidad = "Estadounidense", FechaNacimiento = new DateTime(1952, 12, 5), Activo = true },
            new Autor { Id = 2, Nombre = "Miguel", Apellido = "de Cervantes", Nacionalidad = "Español", FechaNacimiento = new DateTime(1547, 9, 29), Activo = false },
            new Autor { Id = 3, Nombre = "Gabriel", Apellido = "García Márquez", Nacionalidad = "Colombiano", FechaNacimiento = new DateTime(1927, 3, 6), Activo = false },
            new Autor { Id = 4, Nombre = "Antoine", Apellido = "de Saint-Exupéry", Nacionalidad = "Francés", FechaNacimiento = new DateTime(1900, 6, 29), Activo = false },
            new Autor { Id = 5, Nombre = "George", Apellido = "Orwell", Nacionalidad = "Británico", FechaNacimiento = new DateTime(1903, 6, 25), Activo = false }
        };

        public IActionResult Index()
        {
            return View(_autores);
        }

        public IActionResult Detalles(int id)
        {
            var autor = _autores.FirstOrDefault(a => a.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Autor autor)
        {
            if (ModelState.IsValid)
            {
                autor.Id = _autores.Max(a => a.Id) + 1;
                _autores.Add(autor);
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        // GET: Autores/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var autor = _autores.FirstOrDefault(a => a.Id == id.Value);
            if (autor == null) return NotFound();

            return View(autor);
        }

        // POST: Autores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var autor = _autores.FirstOrDefault(a => a.Id == id);
            if (autor == null) return NotFound();

            _autores.Remove(autor);
            return RedirectToAction("Index");
        }
    }
}