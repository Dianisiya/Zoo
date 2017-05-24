using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Entities;

namespace Zoo.Repository
{
    class Repository<T> : IRepository<T> where T: Animal
    {
        private List<T> list = new List<T>();
        public void Add(T entity)
        {
            list.Add(entity);
        }

        public IEnumerable<T> AllEntities()
        {
            return list;
        }

        public void Delete(string name)
        {
            var animal = list.FirstOrDefault(a => a.Name == name);
            if(animal!=null)
            {
                list.Remove(animal);
            }
        }

        public T Get(string name)
        {
            return list.FirstOrDefault(a => a.Name == name);
        }
    }
}
