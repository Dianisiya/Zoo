using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Entities
{
    class Lion : Animal
    {
        public override int MaxHealth => 5;

        public override EKind Kind => EKind.Lion;
    }
}
