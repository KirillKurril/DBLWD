using DBLWD6.CustomORM.Entities;
using System.Linq.Expressions;

namespace DBLWD6.CustomORM.Repository
{
    public class TableWrapper<T> : ITableWrapper<T> where T : DbEntity
    {
        public TableWrapper() { }

        public Task EnsureCreated()
        {

        }
        public Task Add(T objectToAdd)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int objectToDeletId)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetCollection(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task Update(T newObject, int prevObjectId)
        {
            throw new NotImplementedException();
        }
    }
}
