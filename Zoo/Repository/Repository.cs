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

        public IEnumerable<IGrouping<EKind, T>> GroupByKind() => list.GroupBy(a => a.Kind);

        public IEnumerable<T> GetAnimalsByState(EState state) => list.Where(a => a.State == state);

        public IEnumerable<T> GetSickTigers() => list.Where(a => a.Kind == EKind.Tiger && a.State == EState.Sick);

        public IEnumerable<T> GetElephantsByName(string name)
            => list.Where(a => a.Kind == EKind.Elephant && a.Name == name);

        public IEnumerable<string> GetHungryNames() => list.Where(a => a.State == EState.Hungry).Select(a => a.Name);

        public IEnumerable<Tuple<EKind, int>> GetDeadCountByKing() => GroupByKind().Select(grouping => new Tuple<EKind, int>(grouping.Key, grouping.Count(a => a.State == EState.Dead)));

        public IEnumerable<T> GetWolfsAndBearsWhereHealthBigerThen3()
            => list.Where(a => (a.Kind == EKind.Bear || a.Kind == EKind.Wolf) && a.Health > 3);

        public IEnumerable<T> GetMinAndMaxHealth()
            => new List<T>{list.First(a => a.Health == list.Min(a1 => a1.Health)), list.First(a => a.Health == list.Max(a1 => a1.Health)) };

        public double AverageHealth() => list.Average(a => a.Health);

        public IEnumerable<T> GetWithMaxHealthByKind()
            => GroupByKind().SelectMany(a => a.Where(an => an.Health == a.Max(anm => anm.Health)));
    }
}
