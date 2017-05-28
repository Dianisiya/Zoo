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

        private static Dictionary<string, int> states = new Dictionary<string, int>
        {
            {EState.Sated.ToString(), 1 },
            {EState.Hungry.ToString(), 0 },
            {EState.Sick.ToString(), 2 },
            {EState.Dead.ToString(), 3 },
        };

        static void Main(string[] args)
        {
            var help = "If you want to add some new animal, you need to write: Add  <Kind>  <Name>" + Environment.NewLine +
                       "If you want to feed some animal, you need to write: Feed <Name> " + Environment.NewLine +
                       "If you want to heal some animal, you need to write: Heal <Name>" + Environment.NewLine +
                       "If you want to delete some animal, you need to write: Delete <Name>" + Environment.NewLine +
                       "If you want to group by kind: GroupByKind" + Environment.NewLine +
                       "If you see some animal by state: GetAnimalsByState <State>" + Environment.NewLine+
                       "If you want to see all tigers, wicth are ill: GetSickTigers" + Environment.NewLine+
                       "If wou want see some elephant, write: GetElephantsByName <Name>" + Environment.NewLine+
                       "If you want to see name of animls, which are hungry, write: GetHungryNames" + Environment.NewLine+
                       "If you want to see the most helthy animals, wtite: GetWithMaxHealthByKind"+ Environment.NewLine+
                       "If you want to see dead animals, write: GetDeadCountByKing" + Environment.NewLine +
                       "If you want to see wolf and bear, which have helth>3, write: GetWolfsAndBearsWhereHealthBigerThen3" + Environment.NewLine+
                       "If you want to see animal with max and min helth, write: GetMinAndMaxHealth" + Environment.NewLine+
                       "If you want to see average ,write: AverageHealth";
            Console.WriteLine(help);
            IRepository<Animal> repository = new Repository<Animal>();
            IAnimalFactory factory = new AnimalFactory();

            var timer = new Timer(o =>
            {
                var random = new Random();
            
                var animals = repository.AllEntities().Where(a => a.State != EState.Dead).ToArray();
                if (animals.Length == 0)
                    return;
                if (animals.All(a => a.State == EState.Dead))
                {
                    Environment.Exit(0);
                }
                var randomAnimal = animals[random.Next() % animals.Length];
                if (randomAnimal.State == EState.Sated)
                {
                    randomAnimal.State = EState.Hungry;
                    return;
                }
                if (randomAnimal.State == EState.Hungry)
                {
                    randomAnimal.State = EState.Sick;
                    return;
                }
                if (randomAnimal.State == EState.Sick)
                {
                    randomAnimal.DecrementHealth();
                    if (randomAnimal.Health == 0)
                    {
                        randomAnimal.State = EState.Dead;
                    }
                }
            });


            timer.Change(0, 5000);

            while (true)
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
                    case "GroupByKind":
                        foreach (var group in repository.GroupByKind())
                        {
                            Console.WriteLine(group.Key);
                            group.ToList().ForEach(a => Console.WriteLine(" * " + a));
                        }
                        break;

                    case "GetAnimalsByState":
                        repository.GetAnimalsByState((EState)states[str[1]]).ToList().ForEach(Console.WriteLine);
                        break;
                    case "GetSickTigers":
                        repository.GetSickTigers().ToList().ForEach(Console.WriteLine);
                        break;
                    case "GetElephantsByName":
                        repository.GetElephantsByName(str[1]).ToList().ForEach(Console.WriteLine);
                        break;
                    case "GetHungryNames":
                        repository.GetHungryNames().ToList().ForEach(Console.WriteLine);
                        break;
                    case "GetWithMaxHealthByKind":
                        repository.GetWithMaxHealthByKind().ToList().ForEach(Console.WriteLine);
                        break;
                    case "GetDeadCountByKing":
                        repository.GetDeadCountByKing().ToList().ForEach(a => Console.WriteLine(a.Item1.ToString() + "  " + a.Item2));
                        break;
                    case "GetWolfsAndBearsWhereHealthBigerThen3":
                        repository.GetWolfsAndBearsWhereHealthBigerThen3().ToList().ForEach(Console.WriteLine);
                        break;
                    case "GetMinAndMaxHealth":
                        repository.GetMinAndMaxHealth().ToList().ForEach(Console.WriteLine);
                        break;
                    case "AverageHealth":
                        Console.WriteLine(repository.AverageHealth());
                        break;
                    case "help":
                        Console.WriteLine(help);
                        break;
                        
                }
                command?.Execute();
            }
            
        }
    }
}
