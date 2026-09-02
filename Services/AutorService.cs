using BibliotecaMVC.Interfaces;
using BibliotecaMVC.Models;

namespace BibliotecaMVC.Services
{
    public class AutorService : IAutorService
    {
        private readonly List<Autor> _autores;

        public AutorService(IRepositorioAutores repositorio)
        {
            _autores = repositorio.GetAll().ToList();
        }

        public IEnumerable<Autor> GetAll() => _autores;

        public Autor? GetById(int id) => _autores.FirstOrDefault(a => a.Id == id);

        public Autor Create(Autor autor)
        {
            autor.Id = _autores.Any() ? _autores.Max(a => a.Id) + 1 : 1;
            _autores.Add(autor);
            return autor;
        }

        public bool Update(Autor autor)
        {
            var idx = _autores.FindIndex(a => a.Id == autor.Id);
            if (idx == -1) return false;
            _autores[idx] = autor;
            return true;
        }

        public bool Delete(int id)
        {
            var autor = GetById(id);
            if (autor == null) return false;
            _autores.Remove(autor);
            return true;
        }
    }
}