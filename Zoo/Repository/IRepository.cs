using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Entities;

namespace Zoo.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> AllEntities();

        void Add(T entity);

        void Delete(string name);

        T Get(string name);

        IEnumerable<IGrouping<EKind, T>> GroupByKind();

        IEnumerable<T> GetAnimalsByState(EState state);

        IEnumerable<T> GetSickTigers();

        IEnumerable<T> GetElephantsByName(string name);

        IEnumerable<string> GetHungryNames();

        IEnumerable<Tuple<EKind, int>> GetDeadCountByKing();

        IEnumerable<T> GetWolfsAndBearsWhereHealthBigerThen3();

        IEnumerable<T> GetMinAndMaxHealth();

        double AverageHealth();

        IEnumerable<T> GetWithMaxHealthByKind();
    }

}
