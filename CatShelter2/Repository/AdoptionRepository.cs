using CatShelter.Models;
using Microsoft.AspNetCore.Identity;
using CatShelter.Data;
using Microsoft.EntityFrameworkCore;
namespace CatShelter.Repository
{
    public class AdoptionRepository : IRepository<Adoption>
    {
        readonly AppDbContext _context;

        public AdoptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Delete(IdType id)
        {
            var toDelete = _context.Adoptions.FirstOrDefault(x => x.Id == id);
            if (toDelete != null) { _context.Adoptions.Remove(toDelete); }
        }

        public IQueryable<Adoption> GetAll()
        {
            return _context.Adoptions.Include(x => x.User).Include(x => x.Cat);
        }

        public Adoption? GetById(IdType id)
        {
            return _context.Adoptions
                .Include(x => x.Cat)
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Adoption entity)
        {
            _context.Adoptions.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Adoption entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}