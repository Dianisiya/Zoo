using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Entities;

namespace Zoo.Factory
{
    public interface IAnimalFactory
    {
        Animal CreateAnimal(EKind kind, string name);
    }
}
