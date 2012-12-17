using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class MessageTile : Tile
    {
        protected string message = "There is no message here";
        public MessageTile()
        {
            this.texture = "floortile";
            this.Collidable = false;
        }

        public override void HandleMetadata(int x, int y, string metadata)
        {
            this.message = metadata;
        }

        public override void OnWalkInto(Entity e)
        {
            if (e is PlayerEntity)
            {
                UserInterface.Message(message, Color.Green);
            }
            base.OnWalkInto(e);
        }
    }
}
