using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class HackingKit : ItemEntity
    {
        public HackingKit()
        {
            Name = "Hacking Kit";
            RestrictedItem = true;
            NeedsDirection = true;
        }
        public override void UseItem(int x, int y, Entity user)
        {
            if (user is PlayerEntity)
            {
                PlayerEntity player = user as PlayerEntity;
                Vector2 position = player.GetPosition() + new Vector2(x, y);

                Tile selectedTile = Station.Instance.GetTile((int)position.X, (int)position.Y);
                selectedTile.OnHack(player);

            }
        }
    }
}
