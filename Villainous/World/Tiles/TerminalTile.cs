using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class TerminalTile : Tile
    {
        const string SecurityDecal = "terminaltile_decal";

        SecurityLevel level;

        string message;
        public TerminalTile()
        {
            this.Collidable = true;
            this.texture = "terminaltile";
            level = SecurityLevel.PURPLE;
            message = "No Message in Metadata!";
        }


        public override void Draw(SpriteBatch batch, float dt, Vector2 position)
        {
            base.Draw(batch, dt, position);
            batch.Draw(ArtManager.GetTexture(SecurityDecal), position, level.securityColor);
        }


        public override void OnCollideWith(Entity e)
        {
            if (e is PlayerEntity)
            {
                UserInterface.Message("Present Clearance Level (Press E to interact)", Color.Red);
            }
        }

        public override void OnInteract(Entity e)
        {
            if (e is PlayerEntity)
            {
                PlayerEntity p = e as PlayerEntity;
                if (p.GetSecurityLevel() >= this.level)
                {
                    UserInterface.Message(message, Color.Green);
                }
                else
                {
                    UserInterface.Message("Invalid Security Clearance.  This action has been logged", Color.Red);
                }
            }
        }

        public override void HandleMetadata(int x, int y, string metadata)
        {
            string sl = metadata.Substring(0, metadata.IndexOf(' '));
            Console.WriteLine(sl);
            level = SecurityLevel.GetSecurityLevelByName(sl);
            message = metadata.Substring(metadata.IndexOf(' '));

        }
    }
}
