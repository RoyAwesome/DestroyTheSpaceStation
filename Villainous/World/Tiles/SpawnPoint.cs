using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Villainous
{
    class SpawnPoint : Tile
    {
        
        private Type entitySpawnType;
        Vector2 spawnPoint;
        string edata;
        public SpawnPoint()
        {
            this.Collidable = false;
            this.texture = "floortile";
        }

        public void SpawnEntity()
        {
            if (entitySpawnType == null) return;
            Entity e = entitySpawnType.GetConstructor(new Type[] { typeof(Vector2) }).Invoke(new object[] { spawnPoint }) as Entity;
            e.HandleMetadata(edata);
            EntityManager.Instance.SpawnEntity(e);
            OnWalkInto(e);

        }

        public override void HandleMetadata(int x, int y, string metadata)
        {
            string type = metadata.Substring(0, metadata.IndexOf(" "));
            edata = metadata.Substring(metadata.IndexOf(" ")).Trim();
            entitySpawnType = Type.GetType(type);
            
            this.spawnPoint = new Vector2(x, y);

            SpawnEntity();
            
        }

    }
}
