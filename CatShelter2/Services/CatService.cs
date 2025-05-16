using CatShelter.Models;
using CatShelter.Repository;

namespace CatShelter.Services
{
    public class CatService : ICatService
    {
        readonly CatRepository _repository;
        public CatService(CatRepository repository)
        {
            _repository = repository;
        }
        public void Delete(IdType id)
        {
            _repository.Delete(id);
        }

        public IQueryable<Cat> GetAll()
        {
            return _repository.GetAll();
        }

        public Cat? GetById(IdType id)
        {
            return _repository.GetById(id);
        }

        public void Insert(Cat cat)
        {
            _repository.Insert(cat);
        }

        public void Update(Cat cat)
        {
            _repository.Update(cat);
        }
    }
}