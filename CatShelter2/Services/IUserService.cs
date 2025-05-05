using CatShelter.Models;
namespace CatShelter.Services
{
    public interface IUserService
    {
        IQueryable<User> GetAll();
        User? GetById(IdType id);
        void Insert(User user);
        void Update(User user);
        void Delete(IdType id);
        IEnumerable<User> GetAdmins();
        IEnumerable<User> GetEmployees();
        IEnumerable<User> GetByName(string? surname, string? name);

    }
}
