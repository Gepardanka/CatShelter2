using CatShelter.Repository;
using CatShelter.Models;
using Microsoft.AspNetCore.Identity;
using CatShelter.Data;
using Microsoft.EntityFrameworkCore;

namespace CatShelter.Repository
{
    public class CatRepository : IRepository<Cat>
    {
        readonly AppDbContext _context;
        public CatRepository(AppDbContext context)
        {
            _context = context;
         }
        public void AddUserRole(IdType userId, IdType roleId)
        {
            throw new NotImplementedException();
        }

        public void Delete(IdType id)
        {
            var toRemove = _context.Cats.FirstOrDefault(x => x.Id == id);
            if(toRemove != null)
            _context.Cats.Remove(toRemove);
        }

        public IQueryable<Cat> GetAll()
        {
            return _context.Cats;
        }

        public Cat? GetById(IdType id)
        {
            return _context.Cats
                .Include(x => x.Carer)
                .FirstOrDefault(x => x.Id == id);
        }

        public IdType GetRoleId(string role)
        {
            throw new NotImplementedException();
        }

        public IdentityUserRole<IdType>? GetUserRole(IdType userId, IdType roleId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Cat entity)
        {
            _context.Cats.Add(entity);
        }

        public void RemoveUserRole(IdType userId, IdType roleId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Cat entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}