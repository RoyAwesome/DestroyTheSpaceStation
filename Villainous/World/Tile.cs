using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class Tile 
    {
        public const int TileSize = 16;

        Entity entityOnTop = null;

        private bool WasCollidable
        {
            get;
            set;
        }

        public bool Collidable
        {
            get;
            set;
        }

        public string texture
        {
            get;
            set;
        }


        public virtual void OnInteract(Entity e)
        {
            Console.WriteLine(entityOnTop);
            if (entityOnTop != null)
            {
                entityOnTop.OnInteract(e);
                return;
            }
            if (e is PlayerEntity) UserInterface.Message("There is nothing there");
        }

        public virtual void OnWalkInto(Entity e)
        {
            entityOnTop = e;
            WasCollidable = Collidable;
            Collidable = true;
        }

        public virtual void OnWalkOut(Entity e)
        {
            entityOnTop = null;
            Collidable = WasCollidable;
            
        }

        public virtual void OnCollideWith(Entity e)
        {
            if (entityOnTop != null)
            {
                if (e is PlayerEntity)
                {
                    UserInterface.Message("There is something in the way (Press E to interact)", Color.Red);
                }
            }
        }

        public virtual void Draw(SpriteBatch batch, float dt, Vector2 position)
        {
            batch.Draw(ArtManager.GetTexture(this.texture), position, Color.White);
        }

        public virtual void HandleMetadata(int x, int y, String metadata)
        {
            Console.WriteLine(this + " Got Invalid metadata {" + metadata + "}");
        }

        public Tile Copy()
        {
           return (Tile)this.MemberwiseClone();
        }

        public virtual void OnHack(Entity Hacker)
        {

        }

        public virtual void OnControlToggle(Entity toggleer)
        {

        }
    }
}
