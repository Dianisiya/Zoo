using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Entities;

namespace Zoo.Factory
{
    interface IAnimalFactory
    {
        Animal CreateAnimal(EKind kind, string name);
    }
}
