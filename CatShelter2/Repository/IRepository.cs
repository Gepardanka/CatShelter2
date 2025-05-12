using Microsoft.AspNetCore.Identity;

namespace CatShelter.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T? GetById(IdType id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(IdType id);
        void Save();
        void AddUserRole(IdType userId, IdType roleId);
        IdentityUserRole<IdType>? GetUserRole (IdType userId, IdType roleId);
        IdType GetRoleId(string role);
        void RemoveUserRole(IdType userId, IdType roleId);
    }
}
