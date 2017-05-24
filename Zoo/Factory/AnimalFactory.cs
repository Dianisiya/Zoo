using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Entities;

namespace Zoo.Factory
{
    class AnimalFactory : IAnimalFactory
    {
        public Animal CreateAnimal(EKind kind, string name)
        {
            switch (kind)
            {
                case EKind.Bear:
                    return new Bear { Name = name, State = EState.Sated };
                case EKind.Elephant:
                    return new Elephant { Name = name, State = EState.Sated };
                case EKind.Fox:
                    return new Fox { Name = name, State = EState.Sated };
                case EKind.Lion:
                    return new Lion { Name = name, State = EState.Sated };
                case EKind.Tiger:
                    return new Tiger { Name = name, State = EState.Sated };
                case EKind.Wolf:
                    return new Wolf { Name = name, State = EState.Sated };
                default:
                    return null;

            }
                
               
        }
            


    }
}
