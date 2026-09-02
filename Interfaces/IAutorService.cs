using BibliotecaMVC.Models;

namespace BibliotecaMVC.Interfaces
{
    public interface IAutorService
    {
        IEnumerable<Autor> GetAll();
        Autor? GetById(int id);
        Autor Create(Autor autor);
        bool Update(Autor autor);
        bool Delete(int id);
    }
}