using System.Linq.Expressions;

namespace DBLWD6.CustomORM.Repository
{
    public interface ITableWrapper<T>
    {
        Task Add(T objectToAdd);
        Task Update(T newObject, int prevObjectId);
        Task Delete(int objectToDeletId);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetWithConditions(Expression<Func<T, bool>>? predicate);

    }
}
