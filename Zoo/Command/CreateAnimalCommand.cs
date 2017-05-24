using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Entities;
using Zoo.Repository;

namespace Zoo.Command
{
    class AddAnimalCommand : ICommand
    {
        private Animal _animal;
        private IRepository<Animal> _repository;
        public AddAnimalCommand(Animal animal, IRepository<Animal> repository)
        {
            _animal = animal;
            _repository = repository;
        }
        public void Execute()
        {
            _repository.Add(_animal);
            Console.WriteLine("{0} was created", _animal.Name);
        }
    }
}
