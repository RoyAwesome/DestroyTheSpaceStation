using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Villainous
{
    class WallTile : Tile
    {

        public WallTile()
        {
            this.Collidable = true;
            this.texture = "walltile";
        }
    }
}
