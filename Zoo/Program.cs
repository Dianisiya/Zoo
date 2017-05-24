using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zoo.Command;
using Zoo.Entities;
using Zoo.Factory;
using Zoo.Repository;

namespace Zoo
{
    class Program
    {
        private static EKind StringToKind(string kind)
        {
            switch (kind)
            {
                case "Bear": return EKind.Bear;
                case "Elephant": return EKind.Elephant;
                case "Fox": return EKind.Fox;
                case "Lion": return EKind.Lion;
                case "Tiger": return EKind.Tiger;
                case "Wolf": return EKind.Wolf;
                default: throw new ArgumentException(nameof(kind));
            }
        }
        static void Main(string[] args)
        {
            IRepository<Animal> repository = new Repository<Animal>();
            IAnimalFactory factory = new AnimalFactory();

            ThreadPool.QueueUserWorkItem(o =>
            {
                var random = new Random();
                while(true)
                {
                    Thread.Sleep(5000);
                    var animals = repository.AllEntities().ToArray();
                    if (animals.Length == 0)
                        continue;
                    if(animals.All(a => a.State==EState.Dead))
                    {
                        Environment.Exit(0);
                    }
                    var randomAnimal = animals[random.Next() % animals.Length];
                    if(randomAnimal.State == EState.Sated)
                    {
                        randomAnimal.State = EState.Hungry;
                        continue;
                    }
                    if(randomAnimal.State == EState.Hungry)
                    {
                        randomAnimal.State = EState.Sick;
                        continue;
                    }
                    if(randomAnimal.State== EState.Sick)
                    {
                        if(randomAnimal.Health != 0)
                        {
                            randomAnimal.DecrementHealth();
                            if(randomAnimal.Health==0)
                            {
                                randomAnimal.State = EState.Dead; 
                            }
                            continue;
                        }
                        randomAnimal.State = EState.Dead;
                    }
                }
      
            });

            while(true)
            {
                ICommand command=null;
                var str = Console.ReadLine().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                switch (str[0])
                {
                    case "Delete": command = new RemoveAnimalCommand(str[1], repository);
                        break;
                    case "Add": command = new AddAnimalCommand(factory.CreateAnimal(StringToKind(str[1]), str[2]), repository);
                        break;
                    case "Heal": command = new HealAnimalCommand(str[1], repository);
                        break;
                    case "Feed": command = new FeedAnimalCommand(str[1], repository);
                        break;
                    case "Show": foreach(var animal in repository.AllEntities())
                        {
                            Console.WriteLine(animal.Name + " " + animal.State + " " + animal.GetType().Name + " " + animal.Health);
                        }
                        break;
                        
                }
                command?.Execute();
            }
            
        }
    }
}
