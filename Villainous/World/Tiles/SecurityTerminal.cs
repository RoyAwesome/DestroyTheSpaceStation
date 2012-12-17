using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class SecurityTerminal : Tile
    {
        SecurityLevel level;
        List<Tile> commandedTiles = new List<Tile>();
        public SecurityTerminal()
        {
            this.texture = "securitytile";
            this.Collidable = true;
            level = SecurityLevel.PURPLE;
        }

        public override void Draw(SpriteBatch batch, float dt, Vector2 position)
        {
            base.Draw(batch, dt, position);
            batch.Draw(ArtManager.GetTexture("securitytile_decal"), position, level.securityColor);
        }
        public override void OnInteract(Entity e)
        {
            if (e is MovingEntity)
            {
                MovingEntity ent = e as MovingEntity;
                if (this.level <= ent.GetSecurityLevel())
                {
                    OnHack(e);
                }
                else
                {
                    if (e is PlayerEntity)
                    {
                        UserInterface.Message("You don't have clearance do that!", Color.Red);
                    }
                }
            }
        }

        public override void OnHack(Entity Hacker)
        {
            if (Hacker is PlayerEntity)
            {
                UserInterface.Message("Disabled Controlled Systems", Color.Blue);
            }
            foreach (Tile t in commandedTiles)
            {
                t.OnControlToggle(Hacker);

            }
        }

        public override void HandleMetadata(int x, int y, string metadata)
        {
            commandedTiles.Clear();
            //format {x,y;x1,y1;x2,y2}
            string[] bits = metadata.Split(' ');
            string terminals = bits[1];
            terminals = terminals.Replace("{", "").Replace("}", "");
            string[] tiles = terminals.Split(';');
            
            for (int i = 0; i < tiles.Length; i++)
            {
                string[] xy = tiles[i].Split(',');
                int tx = int.Parse(xy[0]);
                int ty = int.Parse(xy[1]);
                commandedTiles.Add(Station.Instance.GetTile(tx, ty));
            }

            level = SecurityLevel.GetSecurityLevelByName(bits[0]);
        }

    }
}
