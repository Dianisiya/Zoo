using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Repository
{
    interface IRepository<T>
    {
        IEnumerable<T> AllEntities();
        void Add(T entity);
        void Delete(string name);
        T Get(string name);
    }
}
