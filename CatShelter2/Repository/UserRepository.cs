using CatShelter.Data;
using CatShelter.Models;
using Microsoft.EntityFrameworkCore;


namespace CatShelter.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Delete(IdType id)
        {
            var toDelete = _context.Users.FirstOrDefault(x => x.Id == id);
            if (toDelete != null)
            {
                _context.Users.Remove(toDelete);
            }
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public User? GetById(IdType id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public void Insert(User entity)
        {
            _context.Users.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            if (string.IsNullOrEmpty(entity.Password))
            {
                var inDb = GetById(entity.Id)!;
                entity.Password = inDb.Password;
                _context.Entry(inDb).State = EntityState.Detached;
            }
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}