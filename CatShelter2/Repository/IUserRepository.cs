using CatShelter.Models;
using Microsoft.AspNetCore.Identity;
namespace CatShelter.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        void AddUserRole(IdType userId, IdType roleId);
        IdentityUserRole<IdType>? GetUserRole (IdType userId, IdType roleId);
        IdType GetRoleId(string role);
        void RemoveUserRole(IdType userId, IdType roleId);
    }
}