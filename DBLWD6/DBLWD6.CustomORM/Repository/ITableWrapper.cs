using System.Linq.Expressions;

namespace DBLWD6.CustomORM.Repository
{
    public interface ITableWrapper<T>
    {
        public Task Add(T objectToAdd);
        public Task Update(T newObject, int prevObjectId);
        public Task Delete(int objectToDeletId);
        public Task<T> GetById(int id);
        public Task<IEnumerable<T>> GetCollection(Expression<Func<T, bool>> predicate);

    }
}
