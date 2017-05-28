using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Entities
{
    class Bear : Animal
    {
        public override int MaxHealth => 6;

        public override EKind Kind => EKind.Bear;
    }
}
