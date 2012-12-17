using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Villainous
{
    abstract class Entity
    {
        public abstract void Update(float dt);

        public abstract void Draw(SpriteBatch batch, float dt);

        public abstract void HandleMetadata(string metadata);

        public abstract void OnInteract(Entity e);
    }
}
