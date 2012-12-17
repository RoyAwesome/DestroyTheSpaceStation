using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class DecalTile : Tile
    {
        static Vector2 half = new Vector2(8, 8);
        int rotation = 0;
        SpriteEffects effects = SpriteEffects.None;
       
        public DecalTile()
        {
            this.Collidable = false;
            this.texture = "floortile";
        }

        public override void HandleMetadata(int x, int y, string metadata)
        {
            string[] mdta = metadata.Split(' ');

            this.texture = mdta[0];

            if (mdta.Length > 1)
            {
                this.rotation = int.Parse(mdta[1]);
                if (mdta.Length > 2)
                {
                    this.Collidable = bool.Parse(mdta[2]);
                    if (mdta.Length > 3)
                    {
                        int flip = int.Parse(mdta[3]);
                        if((flip & 1) == 1)
                        {
                            effects |= SpriteEffects.FlipHorizontally;
                        }
                        if ((flip & 2) == 1)
                        {
                            effects |= SpriteEffects.FlipVertically;
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch batch, float dt, Vector2 position)
        {
            
            batch.Draw(ArtManager.GetTexture(this.texture), position + half, null, Color.White, MathHelper.ToRadians(rotation * 90), half, 1f,  effects, 0f);
            
        }
    }
}
