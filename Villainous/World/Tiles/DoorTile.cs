using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class DoorTile : Tile
    {
        private bool Closed = true;
        public SecurityLevel securityLevel;
        private string securityDecal = "doortile_securityLayer";
        public DoorTile()
        {
            this.Collidable = true;
            this.texture = "doortile_closed";
            securityLevel = SecurityLevel.PURPLE;
            Closed = true;
        }

        public void OpenDoor()
        {
            this.Collidable = false;
            this.texture = "doortile_open";
            Closed = false;
            
        }

        public void CloseDoor()
        {
            this.Collidable = true;
            this.texture = "doortile_closed";
            Closed = true;

        }

        public override void OnHack(Entity Hacker)
        {
            if (Closed)
            {
                OpenDoor();
                if (Hacker is PlayerEntity) UserInterface.Message("The Door Opens");
            }
            else
            {
                if (Hacker is PlayerEntity) UserInterface.Message("Nothing Happens", Color.Red);
            }
        }

        public override void OnInteract(Entity e)
        {
            if (Closed)
            {
                MovingEntity ent = e as MovingEntity;
                if (ent.GetSecurityLevel() >= this.securityLevel)
                {
                    OpenDoor();
                    if (ent is PlayerEntity) UserInterface.Message("The Door Opens"); 
                }
                else UserInterface.Message("You must have a clearence level of " + securityLevel.name + " or higher to open this door");
            }
            else
            {
                CloseDoor();
                if (e is PlayerEntity) UserInterface.Message("The Door Closes");
            }
        }

        public override void Draw(SpriteBatch batch, float dt, Vector2 position)
        {
            base.Draw(batch, dt, position);
            if(Closed) batch.Draw(ArtManager.GetTexture(this.securityDecal), position, securityLevel.securityColor);
        }

        public override void HandleMetadata(int x, int y, string metadata)
        {
            this.securityLevel = SecurityLevel.GetSecurityLevelByName(metadata);
           
        }

        public override void OnCollideWith(Entity e)
        {
            if (Closed)
            {
                if (e is PlayerEntity) UserInterface.Message("The Door is closed (Press E to interact with it)", Color.Red);
            }
        }

        public override void OnControlToggle(Entity toggleer)
        {
            if (Closed)
            {
                OpenDoor();
                if (toggleer is PlayerEntity) UserInterface.Message("The Door Opens");
            }
        }
    }
}
