using CatShelter.Data;
using CatShelter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace CatShelter.Repository
{
    public class UserRepository : IUserRepository
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
            var user = _context.Users
                .Include(x => x.Adoptions)
                .Include(x => x.CaredForCats)
                .FirstOrDefault(x => x.Id == id);
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
            if (string.IsNullOrEmpty(entity.PasswordHash))
            {
                var inDb = GetById(entity.Id)!;
                entity.PasswordHash = inDb.PasswordHash;
                _context.Entry(inDb).State = EntityState.Detached;
            }
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void AddUserRole(IdType userId, IdType roleId)
        {
            _context.UserRoles.Add(new IdentityUserRole<IdType> { UserId = userId, RoleId = roleId });
        }
        public IdType GetRoleId(string roleName){
            return _context.Roles.First(x => x.Name == roleName).Id;
        }
        public IdentityUserRole<IdType>? GetUserRole (IdType userId, IdType roleId){
            return _context.UserRoles.FirstOrDefault(x => x.UserId == userId && x.RoleId == roleId);
        }
        public void RemoveUserRole(IdType userId, IdType roleId){
            var userRole = GetUserRole(userId, roleId);
            if(userRole == null){return;}
            _context.UserRoles.Remove(userRole);
        }
    }

}
