using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Villainous
{
    class Raygun : ItemEntity
    {
        public Raygun()
        {
            this.Name = "RayGun";
            NeedsDirection = true;
        }

        public override void UseItem(int x, int y, Entity user)
        {
            UserInterface.Message("Pew Pew");
        }
    }
}
