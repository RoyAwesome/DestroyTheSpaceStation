using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Villainous
{
    class ItemScanner : Tile
    {
        static Vector2 half = new Vector2(8, 8);
        int rotation = 0;

        bool active = true;

        public ItemScanner()
        {
            this.texture = "scannertile";
        }

        public override void HandleMetadata(int x, int y, string metadata)
        {
            rotation = int.Parse(metadata);
        }

        public override void Draw(SpriteBatch batch, float dt, Vector2 position)
        {
            batch.Draw(ArtManager.GetTexture(this.texture), position + half, null, Color.White, MathHelper.ToRadians(rotation * 90), half, 1f, SpriteEffects.None, 0f);
        }

        public override void OnControlToggle(Entity toggleer)
        {
            active = !active;
        }

        public override void OnWalkInto(Entity e)
        {
            if (!active) return;
            if (e is MovingEntity)
            {
                MovingEntity p = e as MovingEntity;

                ItemEntity[] inv = p.GetInventory();

                for (int i = 0; i < inv.Length; i++)
                {
                    if (inv[i] == null) continue;
                    if (inv[i].RestrictedItem)
                    {
                        if (p is PlayerEntity) UserInterface.Message("Removed " + inv[i].Name + " from your inventory!", Color.Red);
                        p.RemoveItem(i);                        
                    }
                }


            }
        }
    }
}
