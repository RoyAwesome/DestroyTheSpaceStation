using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Villainous
{
    class Knife : ItemEntity
    {
        public Knife()
        {
            this.Name = "Knife";
            NeedsDirection = true;
        }

        public override void UseItem(int x, int y, Entity user)
        {
            UserInterface.Message("You Stab");
        }
    }
}
