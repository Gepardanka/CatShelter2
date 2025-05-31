using CatShelter.Models;

namespace CatShelter.Services
{
    public interface ICatService
    {
        IEnumerable<Cat> GetAll();
        Cat? GetById(IdType id);
        void Insert(Cat cat);
        void Update(Cat cat);
        void Delete(IdType id);
    }
}