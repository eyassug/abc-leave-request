using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livit.Common.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T t);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
