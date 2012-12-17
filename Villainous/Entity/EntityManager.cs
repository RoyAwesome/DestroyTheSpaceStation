using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Villainous
{
    class EntityManager
    {
        public static EntityManager Instance
        {
            get;
            private set;
        }

        public EntityManager()
        {
            Instance = this;
        }

        private List<Entity> entities = new List<Entity>();

        private Queue<Entity> addQueue = new Queue<Entity>();

        private Queue<Entity> removeQueue = new Queue<Entity>();


        public void SpawnEntity(Entity entity)
        {
            addQueue.Enqueue(entity);
        }

        public void KillEntity(Entity entity)
        {
            removeQueue.Enqueue(entity);
        }

        public void update(float dt)
        {
            while (removeQueue.Count != 0)
            {
                entities.Remove(removeQueue.Dequeue());
            }

            while (addQueue.Count != 0)
            {
                entities.Add(addQueue.Dequeue());
            }

            foreach (Entity e in entities)
            {
                e.Update(dt);
            }
        }


        public void Draw(SpriteBatch batch, float dt)
        {
            foreach (Entity e in entities)
            {
                e.Draw(batch, dt);
            }
        }

    }
}
