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
    }
}
