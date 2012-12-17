using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class CommunistManifesto : ItemEntity
    {
        public CommunistManifesto()
        {
            this.Name = "The Communist Manifesto";
            this.RestrictedItem = true;
            this.NeedsDirection = true;
        }

        public override void UseItem(int x, int y, Entity user)
        {
            UserInterface.Message("Hello Comrade!", Color.Red);
        }
    }
}
