using CatShelter.Models;
using CatShelter.Repository;

namespace CatShelter.Services
{
    public class CatService : ICatService
    {
        readonly IRepository<Cat> _repository;
        public CatService(IRepository<Cat> repository)
        {
            _repository = repository;
        }
        public void Delete(IdType id)
        {
            _repository.Delete(id);
            _repository.Save();
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
            _repository.Save();
        }

        public void Update(Cat cat)
        {
            _repository.Update(cat);
            _repository.Save();
        }
    }
}