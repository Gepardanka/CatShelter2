using CatShelter.Models;
namespace CatShelter.Services
{
    public interface IAdoptionService
    {
        IQueryable<Adoption> GetAll();
        Adoption? GetById(IdType id);
        void Insert(Adoption adoption);
        void Update(Adoption adoption);
        void Delete(IdType id);
        IQueryable<Adoption> GetTemporary();
        IQueryable<Adoption> GetLongTerm();
    }
}