using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Entities;
using Zoo.Repository;

namespace Zoo.Command
{
    class RemoveAnimalCommand : ICommand
    {

        private string _name;
        private IRepository<Animal> _repository;
        public RemoveAnimalCommand(string name, IRepository<Animal> repository)
        {
            _name = name;
            _repository = repository;
        }
        public void Execute()
        {
            if (_repository.Get(_name).State == EState.Dead)
            {
                _repository.Delete(_name);
                Console.WriteLine("{0} was deleted", _name);
            }
            Console.WriteLine("{0} still alive", _name);
        }
    }
}
