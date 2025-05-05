using CatShelter.Models;
using CatShelter.Repository;

namespace CatShelter.Services
{
    public class UserService : IUserService
    {
        readonly UserRepository _repository;
        public UserService(UserRepository repository) 
        {
            _repository = repository;
        }
        public void Delete(IdType id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        public IEnumerable<User> GetAdmins()
        {
            return _repository.GetAll().Where(x => x.IsAdmin == true).ToList();
        }

        public IQueryable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public User? GetById(IdType id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<User> GetByName(string? surname, string? name)
        {
            var users = _repository.GetAll();
            if (surname != null)
            {
                users = users.Where(x => x.Surname == surname);
            }
            if (name != null) {
                users = users.Where(x => x.Name == name);
            }
            return users.ToList();
        }

        public IEnumerable<User> GetEmployees()
        {
            return _repository.GetAll().Where(x => x.IsEmployee == true).ToList();
        }

        public void Insert(User user)
        {
            _repository.Insert(user);
            _repository.Save();
        }


        public void Update(User user)
        {
            _repository.Update(user);
            _repository.Save();
        }
    }
}
