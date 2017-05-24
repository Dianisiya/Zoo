using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Entities
{
    public abstract class Animal
    {
        public Animal()
        {
            Health = MaxHealth;
        }

        public string Name { get; set; }

        public int Health { get; protected set; }
        
        public EState State { get; set; }

        public abstract int MaxHealth { get; }

        public void IncrementHealth()
        {
            if (Health < MaxHealth)
            {
                Health++;
            }
        }
        public void DecrementHealth()
        {
            if(Health>0)
            {
                Health--;
            }
        }
        
    }
}
