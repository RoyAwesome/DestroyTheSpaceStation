using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Villainous
{
    abstract class ItemEntity : Entity
    {
        public static ItemEntity CreateItem(string metadata)
        {
            string type = metadata;
           
            if(metadata.Contains('('))
            {
                type = metadata.Substring(0, metadata.IndexOf('('));
                int l = metadata.IndexOf(')') - metadata.IndexOf('(');
                metadata = metadata.Substring(metadata.IndexOf('(')+ 1, l - 1);
                
            }

            Type itemType = Type.GetType(type);

            ItemEntity entity = itemType.GetConstructor(Type.EmptyTypes).Invoke(null) as ItemEntity;
            entity.HandleMetadata(metadata);
            return entity;
        }

        public string Name
        {
            get;
            protected set;
        }

        public bool RestrictedItem
        {
            get;
            protected set;
        }

        public bool NeedsDirection
        {
            get;
            protected set;
        }

        public ItemEntity()
        {
            Name = "Unknown Item";
            RestrictedItem = false;
            NeedsDirection = false;
        }

        public override void Update(float dt)
        {
            
        }

        public override void Draw(SpriteBatch batch, float dt)
        {
           
        }

        public override void HandleMetadata(string metadata)
        {
            Console.WriteLine("Metadata for Entity " + this + " Unknown.  { " + metadata + " } ");
        }

        public abstract void UseItem(int x, int y, Entity user);

        public void UseItem(Entity user)
        {
            UseItem(0, 0, user);
        }


        public override void OnInteract(Entity e)
        {
            if (e is PlayerEntity)
            {
                PlayerEntity p = e as PlayerEntity;
                p.PickUpItem(this);
                EntityManager.Instance.KillEntity(this);
            }
        }
    }
}
