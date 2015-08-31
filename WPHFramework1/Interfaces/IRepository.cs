using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace WPHFramework1
{
    public interface IRepository<T>
    {
        void Save(T entity);
        void Delete(T entity);
        IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        T GetById(int id);

    }
}
