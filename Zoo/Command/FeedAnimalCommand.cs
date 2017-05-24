using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Entities;
using Zoo.Repository;

namespace Zoo.Command
{
    class FeedAnimalCommand : ICommand
    {
        private string _name;
        private IRepository<Animal> _repository;
        public FeedAnimalCommand(string name, IRepository<Animal> repository)
        {
            _name = name;
            _repository = repository;
        }
        public void Execute()
        {
            var animal = _repository.Get(_name);
            if(animal.State==EState.Hungry)
            {
                animal.State = EState.Sated;
            }
            Console.WriteLine("{0} was feaded", _name);
        }
    }
}
