using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Entities
{
    class Tiger: Animal
    {
        public override int MaxHealth => 4;

        public override EKind Kind => EKind.Tiger;
    }
}
