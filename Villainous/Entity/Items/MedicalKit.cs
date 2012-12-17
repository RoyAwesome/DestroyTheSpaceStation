using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Villainous
{
    class MedicalKit : ItemEntity
    {
        public MedicalKit()
        {
            Name = "Restoration Kit";
            NeedsDirection = true;
        }

        public override void UseItem(int x, int y, Entity user)
        {
            UserInterface.Message("You feel healthy");
        }
    }
}
