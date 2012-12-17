using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Villainous
{
    abstract class MovingEntity : Entity
    {
        const float TurnTime = 0.1f;


        private float timeLeft = TurnTime;

       

        protected string HeadTexture
        {
            get;
            set;
        }

        protected string BodyTexture
        {
            get;
            set;
        }

        protected Color HeadColor
        {
            get;
            set;
        }

        protected Color BodyColor
        {
            get;
            set;
        }

        protected Vector2 position;

        protected SecurityLevel securityLevel;

        protected ItemEntity[] inventory = new ItemEntity[10];

        public MovingEntity(Vector2 spawnPoint)
        {
            this.position = spawnPoint;
        }

        protected virtual void MoveDirection(int x, int y)
        {
            Tile t = Station.Instance.GetTile((int)position.X + x, (int)position.Y + y);
            Tile currentTile = Station.Instance.GetTile((int)position.X, (int)position.Y);
            if (t.Collidable == false)
            {
                position += new Vector2(x, y);
                t.OnWalkInto(this);
                currentTile.OnWalkOut(this);
            }
            else
            {
                t.OnCollideWith(this);
            }
        }

        protected void InteractDirection(int x, int y)
        {
            Tile t = Station.Instance.GetTile((int)position.X + x, (int)position.Y + y);
            t.OnInteract(this);
        }

        public override sealed void Update(float dt)
        {
            if (timeLeft <= 0.0f)
            {
                if (DoTurn())
                {
                    timeLeft = TurnTime;
                }
            }
            else
            {
                timeLeft -= dt;
            }
        }

        public abstract bool DoTurn();
       

        public override void Draw(SpriteBatch batch, float dt)
        {
            batch.Draw(ArtManager.GetTexture(this.HeadTexture), Camera.ToScreenSpace(position), HeadColor);
            batch.Draw(ArtManager.GetTexture(this.BodyTexture), Camera.ToScreenSpace(position), securityLevel.securityColor);
        }

        public SecurityLevel GetSecurityLevel()
        {
            return securityLevel;
        }

        public bool PickUpItem(ItemEntity item)
        {
            for (int i = 0; i < 10; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = item;
                    return true;
                }
            }
            if (this is PlayerEntity) UserInterface.Message("Inventory is full! Drop something");
            return false;
        }

        public bool HasItem(Type itemType)
        {
            foreach (ItemEntity item in inventory)
            {
                if (item.GetType() == itemType) return true;
            }
            return false;
        }

        public ItemEntity GetItem(Type itemType)
        {
            foreach (ItemEntity item in inventory)
            {
                if (item.GetType() == itemType) return item;
            }
            return null;
        }

        public void UseItem(Type itemType)
        {
            GetItem(itemType).UseItem(this);
        }

        public void RemoveItem(int slot)
        {
            inventory[slot] = null;
        }

        public ItemEntity GetItem(int slot)
        {
            return inventory[slot];
        }

        public void UseItem(int slot)
        {
            UseItem(0, 0, slot);
        }

        public void UseItem(int x, int y, int slot)
        {
            if (inventory[slot] == null)
            {
                UserInterface.Message("There is no item in that slot", Color.Red);
                return;
            }
            inventory[slot].UseItem(x, y, this);
            RemoveItem(slot);
        }

        public override void HandleMetadata(string metadata)
        {
            Console.WriteLine("Metadata for Entity " + this + " Unknown.  { " + metadata + " } ");
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public override void OnInteract(Entity e)
        {
            if (e is PlayerEntity)
            {
                UserInterface.Message("He looks at you with an empty gaze", Color.Green);
            }
        }

        public ItemEntity[] GetInventory()
        {
            return inventory;
        }
    }
}
