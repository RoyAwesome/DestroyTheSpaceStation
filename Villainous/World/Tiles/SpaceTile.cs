using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Villainous
{
    class SpaceTile : Tile
    {
        public SpaceTile()
        {
            this.Collidable = false;
            this.texture = "spacetile";
        }


        public override void OnInteract(Entity e)
        {
            EntityManager.Instance.KillEntity(e);
        }

    }
}
