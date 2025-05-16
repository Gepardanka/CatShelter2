using CatShelter.Models;
namespace CatShelter.Services
{
    public class AdoptionService : IAdoptionService
    {
        readonly AdoptionRepository _repository;
        public AdoptionService(AdoptionRepository adoptionRepository)
        {
            _repository = adoptionRepository;
        }
        public void Delete(IdType id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        public IQueryable<Adoption> GetAll()
        {
            return _repository.GetAll();
        }

        public Adoption? GetById(IdType id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Adoption> GetTemporary()
        {
            return _repository.GetAll().Where(x => x.AdoptionType == AdoptionType.Temporary);
        }
        public IQueryable<Adoption> GetLongTerm()
        {
            return _repository.GetAll().Where(x => x.AdoptionType == AdoptionType.LongTerm);
        }

        public void Insert(Adoption adoption)
        {
            _repository.Insert(adoption);
            _repository.Save();
        }

        public void Update(Adoption adoption)
        {
            _repository.Update(adoption);
            _repository.Save();
        }
    }
}
