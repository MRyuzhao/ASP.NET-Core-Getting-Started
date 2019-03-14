using System.Collections.Generic;

namespace CoreDemo.Services
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Add(T t);
    }
}
