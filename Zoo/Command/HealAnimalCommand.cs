using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Entities;
using Zoo.Repository;

namespace Zoo.Command
{
    class HealAnimalCommand: ICommand
    {
        private string _name;
        private IRepository<Animal> _repository;

        public HealAnimalCommand(string name, IRepository<Animal> repository)
        {
            _name = name;
            _repository = repository;
        }
        public void Execute()
        {
            var animal = _repository.Get(_name);
            animal.IncrementHealth();
            Console.WriteLine("{0} was healed", _name);
        }
    }
}
