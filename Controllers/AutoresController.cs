using BibliotecaMVC.Interfaces;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers
{
    public class AutoresController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IAutorService _autorService;
        public AutoresController(IWebHostEnvironment env, IAutorService autorService)
        {
            _env = env;
            _autorService = autorService;
        }
        public IActionResult Index()
        {
            return View(_autorService.GetAll());
        }
        public IActionResult Detalles(int id)
        {
            var autor = _autorService.GetById(id);
            if (autor == null) return NotFound();
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
                _autorService.Create(autor);
                return RedirectToAction("Index");
            }
            return View(autor);
        }
        public IActionResult Edit(int id)
        {
            var autor = _autorService.GetById(id);
            if (autor == null) return NotFound();
            return View(autor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Autor autor)
        {
            if (ModelState.IsValid)
            {
                var updated = _autorService.Update(autor);
                if (!updated) return NotFound();
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        // AutoresDelete
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var autor = _autorService.GetById(id.Value);
            if (autor == null) return NotFound();

            return View(autor);
        }

        // POST: AutoresDelete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleted = _autorService.Delete(id);
            if (!deleted) return NotFound();
            return RedirectToAction("Index");
        }
    }
}